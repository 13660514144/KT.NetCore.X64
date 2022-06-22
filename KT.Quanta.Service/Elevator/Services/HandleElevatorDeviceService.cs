using AutoMapper;
using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Quanta.Common.Enums;
using KT.Quanta.Common.Models;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.Devices.Quanta.Models;
using KT.Quanta.Service.Devices.Schindler.Models;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Handlers;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class HandleElevatorDeviceService : IHandleElevatorDeviceService
    {
        private IHandleElevatorInputDeviceDao _handleElevatorInputDeviceDao;
        private ILogger<HandleElevatorDeviceService> _logger;
        private RemoteDeviceList _remoteDeviceList;
        private IHandleElevatorDeviceDeviceDistributeService _handleElevatorDeviceDeviceDistributeService;
        private IFloorService _floorService;
        private IFloorDao _floorDao;
        private IPassRightDao _passRightDao;
        private IHandleElevatorDeviceDao _handleElevatorDeviceDao;
        private PushRecordHandler _pushRecordHanlder;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private IProcessorDao _processorDao;
        private IHandleElevatorDeviceDistributeService _handleElevatorDeviceDistributeService;
        private SchindlerSettings _schindlerSettings;
        private IElevatorGroupDao _elevatorGroupDao;
        private readonly IPassRightAccessibleFloorDao _passRightAccessibleFloorDao;
        private readonly IPassRightAccessibleFloorDetailDao _passRightAccessibleFloorDetailDao;
        private readonly IPassRightDestinationFloorDao _passRightDestinationFloorDao;
        private readonly IHandleElevatorDeviceAuxiliaryDao _handleElevatorDeviceAuxiliaryDao;
        private readonly IQuantaDisplayDistributeDataService _quantaDisplayDistributeDataService;
        private readonly IElevatorGroupFloorDao _elevatorGroupFloorDao;
        private readonly IMapper _mapper;

        public HandleElevatorDeviceService(IHandleElevatorInputDeviceDao handleElevatorInputDeviceDao,
            ILogger<HandleElevatorDeviceService> logger,
            RemoteDeviceList remoteDeviceList,
            IHandleElevatorDeviceDeviceDistributeService handleElevatorDeviceDeviceDistributeService,
            IHandleElevatorDeviceDistributeService handleElevatorDeviceDistributeService,
            IFloorService floorService,
            IPassRightDao passRightDao,
            IHandleElevatorDeviceDao handleElevatorDeviceDao,
            PushRecordHandler pushRecordHanlder,
            IHubContext<QuantaDistributeHub> distributeHub,
            IProcessorDao processorDao,
            IFloorDao floorDao,
            IOptions<SchindlerSettings> schindlerSettings,
            IElevatorGroupDao elevatorGroupDao,
            IPassRightAccessibleFloorDao passRightAccessibleFloorDao,
            IPassRightAccessibleFloorDetailDao passRightAccessibleFloorDetailDao,
            IHandleElevatorDeviceAuxiliaryDao handleElevatorDeviceAuxiliaryDao,
            IPassRightDestinationFloorDao passRightDestinationFloorDao,
             IQuantaDisplayDistributeDataService quantaDisplayDistributeDataService,
             IElevatorGroupFloorDao elevatorGroupFloorDao,
             IMapper mapper)
        {
            _handleElevatorInputDeviceDao = handleElevatorInputDeviceDao;
            _logger = logger;
            _remoteDeviceList = remoteDeviceList;
            _handleElevatorDeviceDeviceDistributeService = handleElevatorDeviceDeviceDistributeService;
            _handleElevatorDeviceDistributeService = handleElevatorDeviceDistributeService;
            _floorService = floorService;
            _passRightDao = passRightDao;
            _handleElevatorDeviceDao = handleElevatorDeviceDao;
            _pushRecordHanlder = pushRecordHanlder;
            _distributeHub = distributeHub;
            _processorDao = processorDao;
            _floorDao = floorDao;
            _schindlerSettings = schindlerSettings.Value;
            _elevatorGroupDao = elevatorGroupDao;
            _passRightAccessibleFloorDao = passRightAccessibleFloorDao;
            _passRightAccessibleFloorDetailDao = passRightAccessibleFloorDetailDao;
            _handleElevatorDeviceAuxiliaryDao = handleElevatorDeviceAuxiliaryDao;
            _passRightDestinationFloorDao = passRightDestinationFloorDao;
            _quantaDisplayDistributeDataService = quantaDisplayDistributeDataService;
            _elevatorGroupFloorDao = elevatorGroupFloorDao ?? throw new ArgumentNullException(typeof(IElevatorGroupFloorDao).Name);
            _mapper = mapper;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _handleElevatorDeviceDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _handleElevatorDeviceDao.DeleteByIdAsync(id);

            //分发数据
            _handleElevatorDeviceDeviceDistributeService.DeleteAsync(id);
        }

        public async Task<HandleElevatorDeviceModel> AddOrEditAsync(HandleElevatorDeviceModel model)
        {
            ProcessorEntity turnstileProcessor = null;
            //闸机服务从闸机获取ip与端口
            if (model.DeviceType == DeviceTypeEnum.ELEVATOR_GATE_DISPLAY.Value)
            {
                //从闸机服务获取ip与端口
                if (!string.IsNullOrEmpty(model.ProcessorId))
                {
                    turnstileProcessor = await _processorDao.GetByIdAsync(model.ProcessorId);
                    if (turnstileProcessor == null)
                    {
                        throw CustomException.Run($"获取闸机边缘处理器数据失败：id:{model.ProcessorId} ");
                    }
                    model.IpAddress = turnstileProcessor.IpAddress;
                    model.Port = turnstileProcessor.Port;
                }
            }

            //持久化数据
            var entity = await _handleElevatorDeviceDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = HandleElevatorDeviceModel.ToEntity(model);
                entity.Processor = turnstileProcessor;
                await _handleElevatorDeviceDao.AddAsync(entity);
            }
            else
            {
                entity = HandleElevatorDeviceModel.SetEntity(entity, model);
                entity.Processor = turnstileProcessor;
                await _handleElevatorDeviceDao.EditAsync(entity);
            }

            model = _mapper.Map(entity, model);

            //获取电梯组
            var groupEntity = await _elevatorGroupDao.GetByIdAsync(model.ElevatorGroupId);
            if (groupEntity == null)
            {
                throw new Exception($"派梯设备关联电梯组不能为空：id:{model.Id} name:{model.Name} elevatorGroupId:{model.ElevatorGroupId} ");
            }

            var groupModel = _mapper.Map<ElevatorGroupModel>(groupEntity);
            model.ElevatorGroup = groupModel;
            model.ElevatorGroupId = groupModel.Id;

            //更新在线设备列表
            var remoteDevice = HandleElevatorDeviceModel.ToRemoteDevice(model);
            await _remoteDeviceList.AddOrUpdateContentAsync(remoteDevice);

            ////电梯列表设备更新 
            //entity = await _handleElevatorDeviceDao.GetWithServersAndFloorsByIdAsync(model.Id);
            //if (entity == null)
            //{
            //    _logger.LogError("找不到已经初始化的派梯设备");
            //    return null;
            //}
            ////清除关联，防止死循环
            //entity.ElevatorGroup.ElevatorServers.ForEach(x => x.ElevatorGroup = null);
            //model = HandleElevatorDeviceModel.ToModel(entity);

            //_koneHandleDeviceList.AddOrUpdate(model);

            //分发数据
            var unitEntity = await GetUnitByDeviceId(model.Id);
            _handleElevatorDeviceDeviceDistributeService.AddOrUpdateAsync(unitEntity);

            return model;
        }

        public async Task<List<HandleElevatorDeviceModel>> GetAllAsync()
        {
            var entities = await _handleElevatorDeviceDao.GetAllAsync();

            var models = _mapper.Map<List<HandleElevatorDeviceModel>>(entities);

            return models;
        }

        public async Task<List<HandleElevatorDeviceModel>> GetByElevatorGroupIdAsync(string elevatorGroupId)
        {
            var entities = await _handleElevatorDeviceDao.SelectByLambdaAsync(x => x.ElevatorGroupId == elevatorGroupId);

            var models = _mapper.Map<List<HandleElevatorDeviceModel>>(entities);

            return models;
        }

        public async Task<HandleElevatorDeviceModel> GetByIdAsync(string id)
        {
            var entity = await _handleElevatorDeviceDao.GetByIdAsync(id);
            var model = _mapper.Map<HandleElevatorDeviceModel>(entity);
            return model;
        }

        public async Task HandleElevator(UnitManualHandleElevatorRequestModel handleElevator)
        {
            _logger.LogInformation($"test test==>{JsonConvert.SerializeObject(handleElevator)}");
            //获取派梯设备
            var remoteDevice = await _remoteDeviceList.GetByIdAsync(handleElevator.HandleElevatorDeviceId);
            if (remoteDevice == null)
            {
                throw CustomException.Run($"派梯设备不存在或已删除1：id:{remoteDevice.RemoteDeviceInfo.DeviceId} ");
            }
            var handleElevatorDeviceEntity = await _handleElevatorDeviceDao.SelectByIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
            if (handleElevatorDeviceEntity == null)
            {
                throw CustomException.Run($"派梯设备不存在或已删除2：id:{remoteDevice.RemoteDeviceInfo.DeviceId} ");
            }

            //派梯楼层
            var sourceFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, handleElevatorDeviceEntity.FloorId);
            var destinationFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, handleElevator.DestinationFloorId);

            //派梯
            if (string.IsNullOrEmpty(sourceFloor?.RealFloorId))
            {
                throw CustomException.Run($"来源楼层真实楼层id不能为空{JsonConvert.SerializeObject(handleElevator)}");
            }
            if (string.IsNullOrEmpty(destinationFloor?.RealFloorId))
            {
                throw CustomException.Run($"目的楼层真实楼层id不能为空{JsonConvert.SerializeObject(handleElevator)}");
            }
            if (sourceFloor.RealFloorId == destinationFloor.RealFloorId)
            {
                throw CustomException.Run($"来源楼层与目地层相同：realFloorId:{sourceFloor.RealFloorId} sourceElevatorGroupFloorId:{sourceFloor.Id} destinationElevatorGroupFloorId:{destinationFloor.Id}");
            }

            var distributeHandle = new DistributeHandleElevatorModel();
            distributeHandle.MessageId = Guid.NewGuid().ToString();
            distributeHandle.DeviceId = remoteDevice?.RemoteDeviceInfo.DeviceId;

            //来源楼层
            distributeHandle.SourceFloor = ElevatorFloorModel.Set(long.Parse(sourceFloor.RealFloorId), sourceFloor.Floor.IsFront, sourceFloor.Floor.IsRear);
            var andleElevatorDeviceAuxiliary = await _handleElevatorDeviceAuxiliaryDao.GetByHandleElevatorDeviceIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
            ElevatorFloorModel.Set(distributeHandle.SourceFloor, andleElevatorDeviceAuxiliary?.IsFront, andleElevatorDeviceAuxiliary?.IsRear);

            // 通力电梯直接派梯一卡可能有多个目的楼层，从辅助权限中查找，存在则派辅助权限中的楼层x= >x. handleElevator.Sign, remoteDevice.RemoteDeviceInfo.DeviceId,true
            var passRightAccessibleFloorDetailEntity = await _passRightDestinationFloorDao.GetWithFloorAsync(
                handleElevator.Sign,
                remoteDevice.RemoteDeviceInfo.ParentId);

            if (passRightAccessibleFloorDetailEntity?.Floor != null)
            {
                var passRightElevaotrGrouopDestinationFloor = await _elevatorGroupFloorDao.GetByElevatorGroupIdAndFloorIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, passRightAccessibleFloorDetailEntity.FloorId);
                if (passRightElevaotrGrouopDestinationFloor == null)
                {
                    throw new Exception($"找不到电梯组楼层：floorId:{passRightAccessibleFloorDetailEntity.FloorId} ");
                }

                //目的楼层
                distributeHandle.DestinationFloor = ElevatorFloorModel.Set(
                    long.Parse(passRightElevaotrGrouopDestinationFloor.RealFloorId),
                    passRightAccessibleFloorDetailEntity.Floor.IsFront,
                    passRightAccessibleFloorDetailEntity.Floor.IsRear,
                    passRightAccessibleFloorDetailEntity.IsFront,
                    passRightAccessibleFloorDetailEntity.IsRear);
            }
            else
            {
                //目的楼层
                distributeHandle.DestinationFloor = ElevatorFloorModel.Set(long.Parse(destinationFloor.RealFloorId), destinationFloor.Floor.IsFront, destinationFloor.Floor.IsRear);

                var passRightAccessibleFloor = await _passRightAccessibleFloorDetailDao.GetAsync(handleElevator.Sign, remoteDevice.RemoteDeviceInfo.ParentId, destinationFloor.Id);
                distributeHandle.DestinationFloor = ElevatorFloorModel.Set(distributeHandle.DestinationFloor, passRightAccessibleFloor?.IsFront, passRightAccessibleFloor?.IsRear);
            }

            distributeHandle.DestinationFloorName = destinationFloor.Floor.Name;
            distributeHandle.RealDeviceId = remoteDevice?.RemoteDeviceInfo?.RealId;
            distributeHandle.CardNumber = handleElevator.Sign;

            await _handleElevatorDeviceDistributeService.HandleAsync(remoteDevice.RemoteDeviceInfo.ParentId, distributeHandle);

            _logger.LogInformation($"派梯操作完成：sourceFloorId:{sourceFloor.RealFloorId} {sourceFloor.Floor.IsFront} {sourceFloor.Floor.IsRear} " +
                $"destinationFloorId:{destinationFloor.RealFloorId} {destinationFloor.Floor.IsFront} {destinationFloor.Floor.IsRear} " +
                $"distributeHandle:{JsonConvert.SerializeObject(distributeHandle, JsonUtil.JsonPrintSettings)} ");
        }

        public async Task SecondaryHandleElevator(
            UnitManualHandleElevatorRequestModel handleElevator,
            DistributeHandleElevatorModel DhModel
            )
        {
            _logger.LogInformation($"API请求到达==>>{JsonConvert.SerializeObject(handleElevator)}");            
            //获取派梯设备
            
            var remoteDevice = await _remoteDeviceList.GetByIdAsync(handleElevator.HandleElevatorDeviceId);
            if (remoteDevice == null)
            {
                throw CustomException.Run($"派梯设备不存在或已删除1：id:{remoteDevice.RemoteDeviceInfo.DeviceId} ");
            }
            /*
            // 二次派梯 取传入
            var handleElevatorDeviceEntity = await _handleElevatorDeviceDao.SelectByIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
            if (handleElevatorDeviceEntity == null)
            {
                throw CustomException.Run($"派梯设备不存在或已删除2：id:{remoteDevice.RemoteDeviceInfo.DeviceId} ");
            }
            if (string.IsNullOrEmpty(handleElevator.SourceFloorId))
            {
                throw CustomException.Run($"派梯设备不存在或已删除2：id:{remoteDevice.RemoteDeviceInfo.DeviceId} ");
            }
            //派梯楼层   用handleElevator.DestinationFloorId 替换 handleElevatorDeviceEntity.FloorId

            var sourceFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, handleElevator.SourceFloorId);            
            var destinationFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, handleElevator.DestinationFloorId);

            //派梯
            if (string.IsNullOrEmpty(sourceFloor?.RealFloorId))
            {
                throw CustomException.Run($"来源楼层真实楼层id不能为空！");
            }
            if (string.IsNullOrEmpty(destinationFloor?.RealFloorId))
            {
                throw CustomException.Run($"目的楼层真实楼层id不能为空！");
            }
            if (sourceFloor.RealFloorId == destinationFloor.RealFloorId)
            {
                throw CustomException.Run($"来源楼层与目地层相同：realFloorId:{sourceFloor.RealFloorId} sourceElevatorGroupFloorId:{sourceFloor.Id} destinationElevatorGroupFloorId:{destinationFloor.Id}");
            }
            
            var distributeHandle = new DistributeHandleElevatorModel();
            distributeHandle.MessageId = Guid.NewGuid().ToString();
            distributeHandle.DeviceId = remoteDevice?.RemoteDeviceInfo.DeviceId;

            _logger.LogInformation($"可以开始API派梯 来源楼层");
            //来源楼层
            distributeHandle.SourceFloor = ElevatorFloorModel.Set(long.Parse(sourceFloor.RealFloorId), sourceFloor.Floor.IsFront, sourceFloor.Floor.IsRear);
            var andleElevatorDeviceAuxiliary = await _handleElevatorDeviceAuxiliaryDao.GetByHandleElevatorDeviceIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
            ElevatorFloorModel.Set(distributeHandle.SourceFloor, andleElevatorDeviceAuxiliary?.IsFront, andleElevatorDeviceAuxiliary?.IsRear);
            _logger.LogInformation($"可以开始API派梯 目的楼层");
            //目的楼层
            distributeHandle.DestinationFloor = ElevatorFloorModel.Set(long.Parse(destinationFloor.RealFloorId), destinationFloor.Floor.IsFront, destinationFloor.Floor.IsRear);
            var passRightAccessibleFloor = await _passRightAccessibleFloorDetailDao.GetAsync(handleElevator.Sign, remoteDevice.RemoteDeviceInfo.ParentId, destinationFloor.Id);
            distributeHandle.DestinationFloor = ElevatorFloorModel.Set(distributeHandle.DestinationFloor, passRightAccessibleFloor?.IsFront, passRightAccessibleFloor?.IsRear);
            _logger.LogInformation($"可以开始API派梯 权限楼层");
            distributeHandle.DestinationFloorName = destinationFloor.Floor.Name;
            distributeHandle.RealDeviceId = remoteDevice?.RemoteDeviceInfo?.RealId;
            distributeHandle.CardNumber = handleElevator.Sign;
            */
            _logger.LogInformation($"可以开始API派梯");
            await _handleElevatorDeviceDistributeService.HandleAsync(remoteDevice.RemoteDeviceInfo.ParentId,DhModel );

            //_logger.LogInformation($"派梯操作完成：sourceFloorId:{sourceFloor.RealFloorId} {sourceFloor.Floor.IsFront} {sourceFloor.Floor.IsRear} " +
             //   $"destinationFloorId:{destinationFloor.RealFloorId} {destinationFloor.Floor.IsFront} {destinationFloor.Floor.IsRear} " +
              //  $"distributeHandle:{JsonConvert.SerializeObject(distributeHandle, JsonUtil.JsonPrintSettings)} ");
        }

        public async Task InitLoadAsync()
        {
            //初始化所有本地边缘处理器
            var models = await GetAllAsync();

            var remoteDevices = HandleElevatorDeviceModel.ToRemoteDevices(models);
            await _remoteDeviceList.AddsAsync(remoteDevices);
        }

        public async Task<UnitHandleElevatorDeviceModel> GetUnitByDeviceId(string deviceId)
        {
            var entity = await _handleElevatorDeviceDao.GetWithFloorsByIdAsync(deviceId);
            if (entity == null)
            {
                throw CustomException.Run($"获取的派梯设备不存在：deviceId:{deviceId} ");
            }

            //权限内容
            var result = new UnitHandleElevatorDeviceModel();
            result.Id = entity.Id;
            result.DeviceFloorId = entity.Floor.Id;
            result.FaceAppId = entity.FaceAppId;
            result.FaceSdkKey = entity.FaceSdkKey;
            result.FaceActivateCode = entity.FaceActivateCode;
            result.ElevatorGroupId = entity.ElevatorGroupId;

            //权限明细
            result.Floors = new List<UnitFloorEntity>();
            if (entity.ElevatorGroup?.ElevatorGroupFloors?.FirstOrDefault() == null)
            {
                _logger.LogWarning($"派梯设备关联可去楼层为空！");
                return result;
            }

            foreach (var item in entity.ElevatorGroup.ElevatorGroupFloors)
            {
                if (item?.Floor == null)
                {
                    _logger.LogWarning($"派梯设备关联可去楼层为空！");
                    continue;
                }

                var unitFloor = new UnitFloorEntity();
                unitFloor.Id = item.Floor.Id;
                unitFloor.Name = item.Floor.Name;
                unitFloor.RealFloorId = item.RealFloorId;
                unitFloor.IsPublic = item.Floor.IsPublic;
                unitFloor.EdificeId = item.Floor.Edifice?.Id;
                unitFloor.EdificeName = item.Floor.Edifice?.Name;
                unitFloor.EditedTime = item.Floor.EditedTime;

                result.Floors.Add(unitFloor);
            }

            return result;
        }

        /// <summary>
        /// 根据权限派梯
        /// </summary>
        /// <param name="rightHandleElevator"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<HandleElevatorDisplayModel> RightHandleElevator(
            UintRightHandleElevatorRequestModel rightHandleElevator, 
            string messageId,string PersonId="")
        {
            //获取派梯设备
            var handleElevatorRemoteDevice = await _remoteDeviceList.GetByIdAsync(rightHandleElevator.HandleElevatorDeviceId);

            if (handleElevatorRemoteDevice == null)
            {
                throw CustomException.Run($"找不到派梯设备：id:{rightHandleElevator.HandleElevatorDeviceId} ");
            }

            //派梯
            var handleElevatorDevice = await _handleElevatorDeviceDao.GetWithFloorByIdAsync(rightHandleElevator.HandleElevatorDeviceId);
            if (handleElevatorDevice?.Floor == null)
            {
                throw CustomException.Run($"派梯设备所在楼层为空！");
            }
            var elevatorGroupDestinationFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId, handleElevatorDevice.Floor.Id);
            if (string.IsNullOrEmpty(elevatorGroupDestinationFloor?.RealFloorId))
            {
                throw CustomException.Run($"来源楼层真实楼层id不能为空");
            }
            _logger.LogInformation($"派梯楼层检查 rightHandleElevator flooid==>{rightHandleElevator.DestinationFloorId}");
            //派梯楼层
            CheckPassRightFloorModel rightCallResult;
            if (!string.IsNullOrEmpty(rightHandleElevator.DestinationFloorId) && rightHandleElevator.DestinationFloorId != "0")
            {
                //直接派梯 
                rightCallResult = new CheckPassRightFloorModel();
                rightCallResult.DestinationFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId, rightHandleElevator.DestinationFloorId);
                rightCallResult.IsDirectHandle = true;
            }
            else
            {
                //根据权限派梯
                rightCallResult = await GetDestinationFloorAsycn(rightHandleElevator, handleElevatorRemoteDevice, handleElevatorDevice);
            }
            //_logger.LogInformation($"派梯楼层检查 rightCallResult=={JsonConvert.SerializeObject(rightCallResult)}");
            //派梯
            if (rightCallResult.IsDirectHandle)
            {
                return await DirectHandle(rightHandleElevator, messageId, handleElevatorRemoteDevice, handleElevatorDevice, elevatorGroupDestinationFloor, rightCallResult,PersonId);
            }
            //发送到派梯设备
            else
            {
                await MultiFloorHandle(rightHandleElevator, messageId, handleElevatorRemoteDevice, handleElevatorDevice, elevatorGroupDestinationFloor, rightCallResult);
            }

            return null;
        }

        public async Task<HandleElevatorDisplayModel> ManualHandleElevator(
            UnitManualHandleElevatorRequestModel handleElevator,
            string PersonId="")
        {
            //获取派梯设备
            var remoteDevice = await _remoteDeviceList.GetByIdAsync(handleElevator.HandleElevatorDeviceId);
            if (remoteDevice == null)
            {
                throw CustomException.Run($"派梯设备不存在或已删除1：id:{remoteDevice.RemoteDeviceInfo.DeviceId} ");
            }
            var handleElevatorDeviceEntity = await _handleElevatorDeviceDao.SelectByIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
            if (handleElevatorDeviceEntity == null)
            {
                throw CustomException.Run($"派梯设备不存在或已删除2：id:{remoteDevice.RemoteDeviceInfo.DeviceId} ");
            }
             
            //派梯
            var handleElevatorDevice = await _handleElevatorDeviceDao.GetWithFloorAndRelevanceFloorsByIdAsync(handleElevator.HandleElevatorDeviceId);
            if (handleElevatorDevice?.Floor == null)
            {
                throw CustomException.Run($"派梯设备所在楼层为空！");
            }
            var elevatorGroupDestinationFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, handleElevatorDevice.Floor.Id);
            if (string.IsNullOrEmpty(elevatorGroupDestinationFloor?.RealFloorId))
            {
                throw CustomException.Run($"来源楼层真实楼层id不能为空");
            }

            var rightCallResult = new CheckPassRightFloorModel();
            rightCallResult.DestinationFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, handleElevator.DestinationFloorId);
            rightCallResult.RightFloors = await _elevatorGroupFloorDao.GetWithEdificeFloorsByElevatorGroupIdAndFloorIdsAsync(remoteDevice.RemoteDeviceInfo.ParentId, handleElevator.DestinationFloorIds);
            var sourceFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(remoteDevice.RemoteDeviceInfo.ParentId, handleElevatorDeviceEntity.FloorId);
            var messageKey = IdUtil.NewId();
            await MultiFloorHandle(handleElevator, messageKey, remoteDevice, handleElevatorDevice, sourceFloor, rightCallResult);

            return null;
        }

        private async Task<HandleElevatorDisplayModel> DirectHandle(UintRightHandleElevatorRequestModel rightHandleElevator,
            string messageId,
            IRemoteDevice handleElevatorRemoteDevice,
            Entities.HandleElevatorDeviceEntity handleElevatorDevice,
            ElevatorGroupFloorEntity sourceFloor,
            CheckPassRightFloorModel rightCallResult,string PersonId="")
        {
            _logger.LogInformation($"直接派梯！！");

            //海康面板机加三菱电梯显示屏不用直接派梯
            if (handleElevatorRemoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SELECTOR.Value)
            {
                if (handleElevatorRemoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELIP_SELECTOR.Value)
                {
                    throw CustomException.Run($"海康面板机加三菱电梯显示屏不能直接派梯,海康面板机已直接派梯！");
                }
            }

            if (string.IsNullOrEmpty(rightCallResult.DestinationFloor?.RealFloorId))
            {
                throw CustomException.Run($"目的楼层真实楼层id不能为空");
            }
            if (sourceFloor.RealFloorId == rightCallResult.DestinationFloor.RealFloorId)
            {
                throw CustomException.Run($"来源楼层与目地层相同：realFloorId:{sourceFloor.RealFloorId} sourceElevatorGroupFloorId:{sourceFloor.Id} destinationElevatorGroupFloorId:{rightCallResult.DestinationFloor.Id}");
            }

            var distributeHandle = new DistributeHandleElevatorModel();
            distributeHandle.MessageId = messageId;
            distributeHandle.DeviceId = handleElevatorRemoteDevice?.RemoteDeviceInfo?.DeviceId;
 
            //来源楼层
            distributeHandle.SourceFloor = ElevatorFloorModel.Set(long.Parse(sourceFloor.RealFloorId), sourceFloor.Floor.IsFront, sourceFloor.Floor.IsRear);
            var handleElevatorDeviceAuxiliary = await _handleElevatorDeviceAuxiliaryDao.GetByHandleElevatorDeviceIdAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.DeviceId);
            ElevatorFloorModel.Set(distributeHandle.SourceFloor, handleElevatorDeviceAuxiliary?.IsFront, handleElevatorDeviceAuxiliary?.IsRear);

            // 通力电梯直接派梯一卡可能有多个目的楼层，从辅助权限中查找，存在则派辅助权限中的楼层
            var passRightAccessibleFloorDetailEntity = await _passRightDestinationFloorDao.GetWithFloorAsync(rightHandleElevator.Sign,
                handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId);

            if (passRightAccessibleFloorDetailEntity?.Floor != null)
            {
                var passRightElevaotrGrouopDestinationFloor = await _elevatorGroupFloorDao.GetByElevatorGroupIdAndFloorIdAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId, passRightAccessibleFloorDetailEntity.FloorId);
                if (passRightElevaotrGrouopDestinationFloor == null)
                {
                    throw new Exception($"找不到电梯组楼层：floorId:{passRightAccessibleFloorDetailEntity.FloorId} ");
                }

                //目的楼层
                distributeHandle.DestinationFloor = ElevatorFloorModel.Set(long.Parse(passRightElevaotrGrouopDestinationFloor.RealFloorId),
                    passRightAccessibleFloorDetailEntity.Floor.IsFront,
                    passRightAccessibleFloorDetailEntity.Floor.IsRear,
                    passRightAccessibleFloorDetailEntity.IsFront,
                    passRightAccessibleFloorDetailEntity.IsRear);
            }
            else
            {
                //目的楼层
                distributeHandle.DestinationFloor = ElevatorFloorModel.Set(long.Parse(rightCallResult.DestinationFloor.RealFloorId)
                    , rightCallResult.DestinationFloor.Floor.IsFront, rightCallResult.DestinationFloor.Floor.IsRear);

                var passRightAccessibleFloor = await _passRightAccessibleFloorDetailDao.GetAsync(rightHandleElevator.Sign,
                    handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId,
                    rightCallResult.DestinationFloor.Id);
                distributeHandle.DestinationFloor = ElevatorFloorModel.Set(distributeHandle.DestinationFloor, passRightAccessibleFloor?.IsFront, passRightAccessibleFloor?.IsRear);
            }

            distributeHandle.DestinationFloorName = rightCallResult.DestinationFloor.Floor.Name;
            distributeHandle.RealDeviceId = handleElevatorRemoteDevice?.RemoteDeviceInfo?.RealId;
            distributeHandle.CardNumber = rightHandleElevator.Sign;

            if (!string.IsNullOrEmpty(rightHandleElevator.Sign) && rightHandleElevator.Sign != "0")
            {
                var signRight = await _passRightDao.GetWithPersonBySignAsync(rightHandleElevator.Sign);
                if (signRight == null)
                {
                    _logger.LogWarning($"找不到权限数据：sign:{rightHandleElevator.Sign} ");
                }
                else
                {
                    if (signRight.Person == null)
                    {
                        _logger.LogWarning($"迅达多楼层派梯人员为空！");
                        distributeHandle.PersonId = long.Parse(signRight.Sign);
                    }
                    else
                    {
                        distributeHandle.PersonId = long.Parse(signRight.Person.Id);
                    }
                }
            }

            //派梯
            await _handleElevatorDeviceDistributeService.HandleAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId, distributeHandle);

            //派梯成功，直接返回
            var result = new HandleElevatorDisplayModel();
            result.DestinationFloorName = rightCallResult.DestinationFloor.Floor.Name;
            result.ElevatorName = string.Empty;

            _logger.LogInformation($"派梯操作完成：sourceFloorId:{sourceFloor.RealFloorId} {sourceFloor.Floor.IsFront} {sourceFloor.Floor.IsRear} " +
                $"destinationFloorId:{rightCallResult.DestinationFloor.RealFloorId} " +
                $"distributeHandle:{JsonConvert.SerializeObject(distributeHandle, JsonUtil.JsonPrintSettings)} " +
                $"result:{ JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");

            //异步上传记录
            if (rightHandleElevator.AccessType.ToString().Trim() == "FACE")
            {
                rightHandleElevator.Sign = PersonId;
            }
            PushRecordAsync(rightHandleElevator, handleElevatorDevice, rightCallResult.DestinationFloor);

            return result;
        }

        private async Task MultiFloorHandle(UintRightHandleElevatorRequestModel rightHandleElevator,
            string messageId,
            IRemoteDevice handleElevatorRemoteDevice,
            HandleElevatorDeviceEntity handleElevatorDevice,
            ElevatorGroupFloorEntity sourceFloor,
            CheckPassRightFloorModel rightCallResult,string PersonId="")
        {
            _logger.LogInformation($"发送到派梯设备派梯！！");

            //二次派梯一体机
            if (handleElevatorRemoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value
                || handleElevatorRemoteDevice.RemoteDeviceInfo.DeviceType==DeviceTypeEnum.ELEVATOR_CLIENT.Value)
            {
                foreach (var item in handleElevatorRemoteDevice.CommunicateDevices)
                {
                    await _distributeHub.Clients.Client(item.CommunicateDeviceInfo.ConnectionId).SendAsync("RightHandleElevator", rightHandleElevator);
                }
            }
            //电梯厅选层器
            else if (handleElevatorRemoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SELECTOR.Value)
            {
                //迅达电梯厅选层器
                if (handleElevatorRemoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.SCHINDLER_PORT_SELECTOR.Value)
                {
                    await SchindlerMultiFloorHandleAsync(rightHandleElevator, messageId, handleElevatorRemoteDevice, sourceFloor);
                }
                //三菱ELSGW电梯厅选层器 
                else if (handleElevatorRemoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELSGW_SELECTOR.Value)
                {
                    await MitsubishiMultiFloorHandleAsync(rightHandleElevator, messageId, handleElevatorRemoteDevice, handleElevatorDevice, sourceFloor, rightCallResult);
                }
                //三菱ELIP电梯厅选层器 
                else if (handleElevatorRemoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELIP_SELECTOR.Value)
                {
                    await MitsubishiMultiFloorHandleAsync(rightHandleElevator, messageId, handleElevatorRemoteDevice, handleElevatorDevice, sourceFloor, rightCallResult);
                }
                //日立电梯厅选层器 
                else if (handleElevatorRemoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HITACHI_DFRS_SELECTOR.Value)
                {
                    await MitsubishiMultiFloorHandleAsync(rightHandleElevator, messageId, handleElevatorRemoteDevice, handleElevatorDevice, sourceFloor, rightCallResult);
                }
                //通力电梯厅选层器 
                else if (handleElevatorRemoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.KONE_DCS_SELECTOR.Value)
                {
                    await KoneMultiFloorHandleAsync(rightHandleElevator, messageId, handleElevatorRemoteDevice, handleElevatorDevice, sourceFloor, rightCallResult);
                }
            }
        }

        /// <summary>
        /// 通力
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="handleElevatorRemoteDevice"></param>
        /// <param name="handleElevatorDevice"></param>
        /// <param name="sourceFloorId"></param>
        /// <param name="rightCallResult"></param>
        /// <returns></returns>
        private async Task KoneMultiFloorHandleAsync(UintRightHandleElevatorRequestModel rightHandleElevator,
            string messageId,
            IRemoteDevice handleElevatorRemoteDevice,
            HandleElevatorDeviceEntity handleElevatorDevice,
            ElevatorGroupFloorEntity sourceFloor,
            CheckPassRightFloorModel rightCallResult)
        {
            var distributeHandle = new DistributeMultiFloorHandleElevatorModel();
            distributeHandle.MessageId = messageId;
            distributeHandle.HandleElevatorDeviceId = handleElevatorRemoteDevice?.RemoteDeviceInfo?.DeviceId;
            distributeHandle.CardNumber = rightHandleElevator.Sign;

            ////来源楼层
            //distributeHandle.SourceFloor = ElevatorFloorModel.Set(long.Parse(sourceFloor.RealFloorId), sourceFloor.IsFront,sourceFloor.IsRear);
            //var floorDirection = await _passRightAccessibleFloorDao.GetBySignAndFloorIdAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.DeviceId, sourceFloor.Id);
            //distributeHandle.SourceFloor = ElevatorFloorModel.Set(distributeHandle.SourceFloor, floorDirection?.IsFront, floorDirection?.IsRear);

            //来源楼层
            distributeHandle.SourceFloor = ElevatorFloorModel.Set(long.Parse(sourceFloor.RealFloorId), sourceFloor.Floor.IsFront, sourceFloor.Floor.IsRear);
            var handleElevatorDeviceAuxiliary = await _handleElevatorDeviceAuxiliaryDao.GetByHandleElevatorDeviceIdAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.DeviceId);
            ElevatorFloorModel.Set(distributeHandle.SourceFloor, handleElevatorDeviceAuxiliary?.IsFront, handleElevatorDeviceAuxiliary?.IsRear);

            distributeHandle.RealDeviceId = handleElevatorRemoteDevice?.RemoteDeviceInfo?.RealId;
            distributeHandle.CardDeviceId = rightHandleElevator.DeviceId;

            //目的楼层
            distributeHandle.DestinationFloors = new List<ElevatorFloorModel>();
            var floors = rightCallResult.RightFloors
                 .Where(x => handleElevatorDevice.ElevatorGroup
                     .ElevatorGroupFloors.Any(y => y.FloorId == x.Id))
                 .ToList();
            foreach (var item in floors)
            {
                //var destinationFloor = ElevatorFloorModel.Set(long.Parse(item.RealFloorId), item.IsFront, item.IsRear);
                //floorDirection = await _passRightAccessibleFloorDao.GetBySignAndFloorIdAsync(rightHandleElevator.Sign, item.Id);
                //destinationFloor = ElevatorFloorModel.Set(destinationFloor, floorDirection?.IsFront, floorDirection?.IsRear);

                //目的楼层
                var destinationFloor = ElevatorFloorModel.Set(long.Parse(item.RealFloorId), item.Floor.IsFront, item.Floor.IsRear);

                var passRightAccessibleFloor = await _passRightAccessibleFloorDetailDao.GetAsync(rightHandleElevator.Sign,
                    handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId,
                    item.Id);
                destinationFloor = ElevatorFloorModel.Set(destinationFloor, passRightAccessibleFloor?.IsFront, passRightAccessibleFloor?.IsRear);

                if (destinationFloor.IsFront || destinationFloor.IsRear)
                {
                    distributeHandle.DestinationFloors.Add(destinationFloor);
                }
            }

            //默认楼层
            if (!string.IsNullOrEmpty(rightCallResult.DestinationFloor?.Id)
                && !distributeHandle.DestinationFloors.Any(x => x.ToString() == rightCallResult.DestinationFloor.Id)
                && handleElevatorDevice.ElevatorGroup.ElevatorGroupFloors.Any(y => y.Floor.Id == rightCallResult.DestinationFloor.Id))
            {
                var realFloorId = Convert.ToInt64(rightCallResult.DestinationFloor.RealFloorId);
                if (!distributeHandle.DestinationFloors.Any(x => x.Floor == realFloorId))
                {
                    //var destinationFloor = ElevatorFloorModel.Set(long.Parse(rightCallResult.DestinationFloor.RealFloorId), rightCallResult.DestinationFloor.IsFront, rightCallResult.DestinationFloor.IsRear);
                    //floorDirection = await _passRightAccessibleFloorDao.GetBySignAndFloorIdAsync(rightHandleElevator.Sign, rightCallResult.DestinationFloor.Id);
                    //destinationFloor = ElevatorFloorModel.Set(destinationFloor, floorDirection?.IsFront, floorDirection?.IsRear);

                    //目的楼层
                    var destinationFloor = ElevatorFloorModel.Set(long.Parse(rightCallResult.DestinationFloor.RealFloorId),
                                 rightCallResult.DestinationFloor.Floor.IsFront, rightCallResult.DestinationFloor.Floor.IsRear);

                    var passRightAccessibleFloor = await _passRightAccessibleFloorDetailDao.GetAsync(rightHandleElevator.Sign,
                        handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId,
                        rightCallResult.DestinationFloor.Id);
                    destinationFloor = ElevatorFloorModel.Set(destinationFloor, passRightAccessibleFloor?.IsFront, passRightAccessibleFloor?.IsRear);

                    if (destinationFloor.IsFront || destinationFloor.IsRear)
                    {
                        distributeHandle.DestinationFloors.Add(destinationFloor);
                    }
                }
            }

            //派梯
            await _handleElevatorDeviceDistributeService.MultiFloorHandleAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId, distributeHandle);

            // 无权限返回错误
            if (distributeHandle.DestinationFloors?.FirstOrDefault() == null
                || !distributeHandle.DestinationFloors.All(x => x.IsFront || x.IsRear))
            {
                var elevatorDisplay = new ElevatorDisplayModel();
                elevatorDisplay.HandleElevatorDeviceId = distributeHandle.HandleElevatorDeviceId;
                elevatorDisplay.CardDeviceId = rightHandleElevator.DeviceId;
                await _quantaDisplayDistributeDataService.HandleElevatorError(elevatorDisplay, "抱歉，您无可去楼层权限！");

            }
        }

        /// <summary>
        /// 三菱多楼层派梯
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="handleElevatorRemoteDevice"></param>
        /// <param name="handleElevatorDevice"></param>
        /// <param name="sourceFloorId"></param>
        /// <param name="rightCallResult"></param>
        /// <returns></returns>
        private async Task MitsubishiMultiFloorHandleAsync(UintRightHandleElevatorRequestModel rightHandleElevator,
            string messageId,
            IRemoteDevice handleElevatorRemoteDevice,
            HandleElevatorDeviceEntity handleElevatorDevice,
            ElevatorGroupFloorEntity sourceFloor,
            CheckPassRightFloorModel rightCallResult)
        {
            var distributeHandle = new DistributeMultiFloorHandleElevatorModel();
            distributeHandle.MessageId = messageId;
            distributeHandle.HandleElevatorDeviceId = handleElevatorRemoteDevice?.RemoteDeviceInfo?.DeviceId;
            distributeHandle.CardNumber = rightHandleElevator.Sign;

            ////来源楼层
            //distributeHandle.SourceFloor = ElevatorFloorModel.Set(long.Parse(sourceFloor.RealFloorId), sourceFloor.IsFront,sourceFloor.IsRear);
            //var floorDirection = await _passRightAccessibleFloorDao.GetBySignAndFloorIdAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.DeviceId, sourceFloor.Id);
            //distributeHandle.SourceFloor = ElevatorFloorModel.Set(distributeHandle.SourceFloor, floorDirection?.IsFront, floorDirection?.IsRear);

            //来源楼层
            distributeHandle.SourceFloor = ElevatorFloorModel.Set(long.Parse(sourceFloor.RealFloorId), sourceFloor.Floor.IsFront, sourceFloor.Floor.IsRear);
            var handleElevatorDeviceAuxiliary = await _handleElevatorDeviceAuxiliaryDao.GetByHandleElevatorDeviceIdAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.DeviceId);
            ElevatorFloorModel.Set(distributeHandle.SourceFloor, handleElevatorDeviceAuxiliary?.IsFront, handleElevatorDeviceAuxiliary?.IsRear);

            distributeHandle.RealDeviceId = handleElevatorRemoteDevice?.RemoteDeviceInfo?.RealId;
            distributeHandle.CardDeviceId = rightHandleElevator.DeviceId;

            //目的楼层
            distributeHandle.DestinationFloors = new List<ElevatorFloorModel>();
            var floors = rightCallResult.RightFloors
                 .Where(x => handleElevatorDevice.ElevatorGroup
                     .ElevatorGroupFloors.Any(y => y.FloorId == x.Id))
                 .ToList();
            foreach (var item in floors)
            {
                //var destinationFloor = ElevatorFloorModel.Set(long.Parse(item.RealFloorId), item.IsFront, item.IsRear);
                //floorDirection = await _passRightAccessibleFloorDao.GetBySignAndFloorIdAsync(rightHandleElevator.Sign, item.Id);
                //destinationFloor = ElevatorFloorModel.Set(destinationFloor, floorDirection?.IsFront, floorDirection?.IsRear);

                //目的楼层
                var destinationFloor = ElevatorFloorModel.Set(long.Parse(item.RealFloorId), item.Floor.IsFront, item.Floor.IsRear);

                var passRightAccessibleFloor = await _passRightAccessibleFloorDetailDao.GetAsync(rightHandleElevator.Sign,
                    handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId,
                    item.Id);
                destinationFloor = ElevatorFloorModel.Set(destinationFloor, passRightAccessibleFloor?.IsFront, passRightAccessibleFloor?.IsRear);

                if (destinationFloor.IsFront || destinationFloor.IsRear)
                {
                    distributeHandle.DestinationFloors.Add(destinationFloor);
                }
            }

            //默认楼层
            if (!string.IsNullOrEmpty(rightCallResult.DestinationFloor?.Id)
                && !distributeHandle.DestinationFloors.Any(x => x.ToString() == rightCallResult.DestinationFloor.Id)
                && handleElevatorDevice.ElevatorGroup.ElevatorGroupFloors.Any(y => y.Floor.Id == rightCallResult.DestinationFloor.Id))
            {
                var realFloorId = Convert.ToInt64(rightCallResult.DestinationFloor.RealFloorId);
                if (!distributeHandle.DestinationFloors.Any(x => x.Floor == realFloorId))
                {
                    //var destinationFloor = ElevatorFloorModel.Set(long.Parse(rightCallResult.DestinationFloor.RealFloorId), rightCallResult.DestinationFloor.IsFront, rightCallResult.DestinationFloor.IsRear);
                    //floorDirection = await _passRightAccessibleFloorDao.GetBySignAndFloorIdAsync(rightHandleElevator.Sign, rightCallResult.DestinationFloor.Id);
                    //destinationFloor = ElevatorFloorModel.Set(destinationFloor, floorDirection?.IsFront, floorDirection?.IsRear);

                    //目的楼层
                    var destinationFloor = ElevatorFloorModel.Set(long.Parse(rightCallResult.DestinationFloor.RealFloorId),
                        rightCallResult.DestinationFloor.Floor.IsFront, rightCallResult.DestinationFloor.Floor.IsRear);

                    var passRightAccessibleFloor = await _passRightAccessibleFloorDetailDao.GetAsync(rightHandleElevator.Sign,
                        handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId,
                        rightCallResult.DestinationFloor.Id);
                    destinationFloor = ElevatorFloorModel.Set(destinationFloor, passRightAccessibleFloor?.IsFront, passRightAccessibleFloor?.IsRear);

                    if (destinationFloor.IsFront || destinationFloor.IsRear)
                    {
                        distributeHandle.DestinationFloors.Add(destinationFloor);
                    }
                }
            }

            if (distributeHandle.DestinationFloors.IsListNull())
            {
                throw CustomException.Run($"多楼层派梯：目地楼层为空！");
            }

            //派梯
            await _handleElevatorDeviceDistributeService.MultiFloorHandleAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId, distributeHandle);
        }

        /// <summary>
        /// 迅达多楼层派梯
        /// </summary>
        /// <param name="rightHandleElevator"></param>
        /// <param name="messageId"></param>
        /// <param name="handleElevatorRemoteDevice"></param>
        /// <param name="sourceFloor"></param>
        /// <returns></returns>
        private async Task SchindlerMultiFloorHandleAsync(UintRightHandleElevatorRequestModel rightHandleElevator,
            string messageId,
            IRemoteDevice handleElevatorRemoteDevice,
            ElevatorGroupFloorEntity sourceFloor)
        {
            var distributeHandle = new DistributeHandleElevatorModel();
            distributeHandle.MessageId = messageId;
            distributeHandle.DeviceId = handleElevatorRemoteDevice?.RemoteDeviceInfo?.DeviceId;
            distributeHandle.CardNumber = rightHandleElevator.Sign;

            ////来源楼层
            //distributeHandle.SourceFloor = ElevatorFloorModel.Set(long.Parse(sourceFloor.RealFloorId), sourceFloor.IsFront,sourceFloor.IsRear);
            //var floorDirection = await _passRightAccessibleFloorDao.GetBySignAndFloorIdAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.DeviceId, sourceFloor.Id);
            //distributeHandle.SourceFloor = ElevatorFloorModel.Set(distributeHandle.SourceFloor, floorDirection?.IsFront, floorDirection?.IsRear);

            //来源楼层
            distributeHandle.SourceFloor = ElevatorFloorModel.Set(long.Parse(sourceFloor.RealFloorId), sourceFloor.Floor.IsFront, sourceFloor.Floor.IsRear);
            var handleElevatorDeviceAuxiliary = await _handleElevatorDeviceAuxiliaryDao.GetByHandleElevatorDeviceIdAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.DeviceId);
            ElevatorFloorModel.Set(distributeHandle.SourceFloor, handleElevatorDeviceAuxiliary?.IsFront, handleElevatorDeviceAuxiliary?.IsRear);

            distributeHandle.RealDeviceId = handleElevatorRemoteDevice?.RemoteDeviceInfo?.RealId;

            var signRight = await _passRightDao.GetWithPersonBySignAsync(rightHandleElevator.Sign);
            if (signRight == null)
            {
                _logger.LogWarning($"找不到权限数据：sign:{rightHandleElevator.Sign} ");
            }
            else
            {
                if (signRight.Person == null)
                {
                    _logger.LogWarning($"迅达多楼层派梯人员为空！");
                    distributeHandle.PersonId = long.Parse(signRight.Sign);
                }
                else
                {
                    distributeHandle.PersonId = long.Parse(signRight.Person.Id);
                }
            }

            //通力用户派梯
            distributeHandle.IsCallByPerson = true;

            //派梯
            await _handleElevatorDeviceDistributeService.HandleAsync(handleElevatorRemoteDevice.RemoteDeviceInfo.ParentId, distributeHandle);
        }

        /// <summary>
        /// 上传数据，异步
        /// </summary>
        /// <param name="rightHandleElevator"></param>
        /// <param name="handleElevatorDevice"></param>
        /// <param name="rightDestinationFloor"></param>
        private async void PushRecordAsync(UintRightHandleElevatorRequestModel rightHandleElevator,
            Entities.HandleElevatorDeviceEntity handleElevatorDevice,
            ElevatorGroupFloorEntity rightDestinationFloor)
        {
            //上传事件
            var passRecord = new PassRecordModel();
            passRecord.Extra = rightHandleElevator.DeviceId;
            passRecord.DeviceType = handleElevatorDevice.DeviceType;
            passRecord.AccessType = rightHandleElevator.AccessType;
            passRecord.PassRightSign = rightHandleElevator.Sign;
            passRecord.DeviceId = rightHandleElevator.HandleElevatorDeviceId;
            passRecord.PassTime = DateTimeUtil.UtcNowMillis();
            passRecord.PassLocalTime = passRecord.PassTime.ToZoneDateTimeByMillis()?.ToSecondString();
            passRecord.Remark = rightDestinationFloor.Floor.Name;

            await _pushRecordHanlder.StartPushAsync(passRecord);
        }

        private async Task<CheckPassRightFloorModel> GetDestinationFloorAsycn(UintRightHandleElevatorRequestModel rightHandleElevator,
            IRemoteDevice remoteDevice,
            Entities.HandleElevatorDeviceEntity handleElevatorDevice)
        {
            //获取权限
            var right = await _passRightDao.GetWithFloorAndFloorsBySignAndAccessTypeAsync(rightHandleElevator.Sign, rightHandleElevator.AccessType, RightTypeEnum.ELEVATOR.Value);
            if (right == null)
            {
                throw CustomException.Run($"派梯通行权限不能为空！");
            }
            right?.RelationFloors?.ForEach(x => x.PassRight = null);
            _logger.LogInformation($"派梯权限：data:{JsonConvert.SerializeObject(right, JsonUtil.JsonPrintSettings)} ");

            //是否直接派梯
            var result = new CheckPassRightFloorModel();

            //闸机派梯显示屏直接派梯、本体屏直接派梯 
            if (right?.Floor != null &&
                (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_GATE_DISPLAY.Value
                || remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SELF_DISPLAY.Value))
            {
                _logger.LogInformation($"派梯设备类型过虑直接派梯：deviceType:{remoteDevice.RemoteDeviceInfo.DeviceType} ");
                result.IsDirectHandle = true;
                result.DestinationFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(handleElevatorDevice.ElevatorGroupId, right.Floor.Id);
            }
            else if (right?.RelationFloors?.FirstOrDefault() != null)
            {
                var rightFloorIds = right.RelationFloors.Select(x => x.Id).ToList();

                handleElevatorDevice = await _handleElevatorDeviceDao.GetWithServersAndFloorsByIdAsync(rightHandleElevator.HandleElevatorDeviceId);

                var publicFloorCount = handleElevatorDevice?.ElevatorGroup?.ElevatorGroupFloors?.Count(x => x.Floor.IsPublic
                                                                                                        && x.Id != handleElevatorDevice?.Floor?.Id
                                                                                                        && !rightFloorIds.Contains(x.Id));
                //直接派梯，只有一层可去楼层或不考虑公共楼层
                if (right?.RelationFloors?.Count == 1 && publicFloorCount == 0)
                {
                    result.IsDirectHandle = true;
                    result.DestinationFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(handleElevatorDevice.ElevatorGroupId, right.RelationFloors.FirstOrDefault().Floor.Id);
                }
                //可选派梯，返回可去楼层
                else
                {
                    result.IsDirectHandle = false;
                    result.DestinationFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(handleElevatorDevice.ElevatorGroupId, right.Floor.Id);
                    result.RightFloors = await _elevatorGroupFloorDao.GetWithFloorsByElevatorGroupIdAndFloorIdsAsync(handleElevatorDevice.ElevatorGroupId, right.RelationFloors.Select(x => x.Floor.Id).ToList());
                    result.HandleElevatorDeviceFloors = await _elevatorGroupFloorDao.GetWithFloorsByElevatorGroupIdAndFloorIdsAsync(handleElevatorDevice.ElevatorGroupId, handleElevatorDevice.ElevatorGroup.ElevatorGroupFloors.Select(x => x.Floor.Id).ToList());
                }
            }

            return result;
        }


    }

    public class CheckPassRightFloorModel
    {
        public bool IsDirectHandle { get; set; } = false;
        public ElevatorGroupFloorEntity DestinationFloor { get; set; }
        public List<ElevatorGroupFloorEntity> RightFloors { get; set; }
        public List<ElevatorGroupFloorEntity> HandleElevatorDeviceFloors { get; set; }
    }
}
