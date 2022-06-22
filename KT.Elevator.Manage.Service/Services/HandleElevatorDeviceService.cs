using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Devices.DeviceDistributes;
using KT.Elevator.Manage.Service.Devices.Kone;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.Handlers;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.Hubs;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Proxy.QuantaApi.Turnstile;
using KT.Quanta.Common.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class HandleElevatorDeviceService : IHandleElevatorDeviceService
    {
        private IHandleElevatorDeviceDao _cardDeviceDataDao;
        private IHandleElevatorInputDeviceDao _handleElevatorInputDeviceDao;
        private KoneHandleDeviceList _koneHandleDeviceList;
        private ILogger<HandleElevatorDeviceService> _logger;
        private RemoteDeviceList _remoteDeviceList;
        private IHandleElevatorDeviceDeviceDistributeService _handleElevatorDeviceDistributeService;
        private IFloorService _floorService;
        private IInternalApi _internalApi;
        private IPassRightDao _passRightDao;
        private IHandleElevatorDeviceDao _handleElevatorDeviceDao;
        private PushRecordHandler _pushRecordHanlder;
        private IHubContext<DistributeHub> _distributeHub;

        public HandleElevatorDeviceService(IHandleElevatorDeviceDao cardDeviceDataDao,
            IHandleElevatorInputDeviceDao handleElevatorInputDeviceDao,
            KoneHandleDeviceList koneHandleDeviceList,
            ILogger<HandleElevatorDeviceService> logger,
            RemoteDeviceList remoteDeviceList,
            IHandleElevatorDeviceDeviceDistributeService handleElevatorDeviceDistributeService,
            IFloorService floorService,
            IInternalApi internalApi,
            IPassRightDao passRightDao,
            IHandleElevatorDeviceDao handleElevatorDeviceDao,
            PushRecordHandler pushRecordHanlder,
            IHubContext<DistributeHub> distributeHub)
        {
            _cardDeviceDataDao = cardDeviceDataDao;
            _handleElevatorInputDeviceDao = handleElevatorInputDeviceDao;
            _koneHandleDeviceList = koneHandleDeviceList;
            _logger = logger;
            _remoteDeviceList = remoteDeviceList;
            _handleElevatorDeviceDistributeService = handleElevatorDeviceDistributeService;
            _floorService = floorService;
            _internalApi = internalApi;
            _passRightDao = passRightDao;
            _handleElevatorDeviceDao = handleElevatorDeviceDao;
            _pushRecordHanlder = pushRecordHanlder;
            _distributeHub = distributeHub;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _cardDeviceDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _cardDeviceDataDao.DeleteByIdAsync(id);

            //分发数据
            _handleElevatorDeviceDistributeService.DeleteAsync(id);
        }

        public async Task<HandleElevatorDeviceModel> AddOrEditAsync(HandleElevatorDeviceModel model)
        {
            //闸机服务从闸机获取ip与端口
            if (model.DeviceType == DeviceTypeEnum.ELEVATOR_GATE_DISPLAY.Value)
            {
                //从闸机服务获取ip与端口
                var turnstileProcessor = await _internalApi.GetProcessorByIdAsync(model.DeviceId);
                if (turnstileProcessor == null)
                {
                    throw CustomException.Run($"获取闸机边缘处理器数据失败：id:{model.DeviceId} ");
                }
                model.IpAddress = turnstileProcessor.IpAddress;
                model.Port = turnstileProcessor.Port;
            }

            //持久化数据
            var entity = await _cardDeviceDataDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = HandleElevatorDeviceModel.ToEntity(model);
                entity.DeviceKey = IdUtil.NewId();
                await _cardDeviceDataDao.AddAsync(entity);
            }
            else
            {
                entity = HandleElevatorDeviceModel.SetEntity(entity, model);
                await _cardDeviceDataDao.EditAsync(entity);
            }

            model = HandleElevatorDeviceModel.SetModel(model, entity);

            //更新在线设备列表
            var remoteDevice = HandleElevatorDeviceModel.ToRemoteDevice(model);
            await _remoteDeviceList.AddOrUpdateContentAsync(remoteDevice);

            //电梯列表设备更新 
            entity = await _cardDeviceDataDao.GetWithServersAndFloorsByIdAsync(model.Id);
            if (entity == null)
            {
                _logger.LogError("找不到已经初始化的派梯设备");
                return null;
            }
            //清除关联，防止死循环
            entity.ElevatorGroup.ElevatorServers.ForEach(x => x.ElevatorGroup = null);
            model = HandleElevatorDeviceModel.ToModel(entity);
            _koneHandleDeviceList.AddOrUpdate(model);

            //分发数据
            var unitEntity = await GetUnitByDeviceId(model.Id);
            _handleElevatorDeviceDistributeService.AddOrUpdateAsync(unitEntity);

            return model;
        }

        public async Task<List<HandleElevatorDeviceModel>> GetAllAsync()
        {
            var entities = await _cardDeviceDataDao.GetAllAsync();

            var models = HandleElevatorDeviceModel.ToModels(entities);

            return models;
        }

        public async Task<HandleElevatorDeviceModel> GetByIdAsync(string id)
        {
            var entity = await _cardDeviceDataDao.GetByIdAsync(id);
            var model = HandleElevatorDeviceModel.ToModel(entity);
            return model;
        }

        public async Task HandleElevator(HandleElevatorModel handleElevator)
        {
            var device = _koneHandleDeviceList.GetById(handleElevator.HandleElevatorDeviceId);
            if (device == null)
            {
                //设备未初始化从数据库中获取并初始化
                var entity = await _cardDeviceDataDao.GetWithServersAndFloorsByIdAsync(handleElevator.HandleElevatorDeviceId);
                if (entity == null)
                {
                    throw CustomException.Run("找不到已经初始化的派梯设备");
                }

                //清除关联，防止死循环
                entity.ElevatorGroup.ElevatorServers.ForEach(x => x.ElevatorGroup = null);

                var model = HandleElevatorDeviceModel.ToModel(entity);
                device = _koneHandleDeviceList.AddOrUpdate(model);
            }

            //获取终端号
            var remoteDevice = _remoteDeviceList.GetById(handleElevator.HandleElevatorDeviceId);
            var sourceFloor = await _floorService.GetByIdAsync(handleElevator.SourceFloorId);
            var destinationFloor = await _floorService.GetByIdAsync(handleElevator.DestinationFloorId);

            //派梯
            if (string.IsNullOrEmpty(sourceFloor?.RealFloorId))
            {
                throw CustomException.Run($"来源楼层真实楼层id为空");
            }
            if (string.IsNullOrEmpty(destinationFloor?.RealFloorId))
            {
                throw CustomException.Run($"目的楼层真实楼层id为空");
            }

            var terminalId = remoteDevice?.RemoteDeviceInfo.TerminalId ?? 0;

            device.Handle(sourceFloor.RealFloorId, destinationFloor.RealFloorId, terminalId);

            _logger.LogInformation($"派梯操作完成：sourceFloorId:{sourceFloor.RealFloorId} destinationFloorId:{destinationFloor.RealFloorId} terminalId:{terminalId} ");
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
            var entity = await _cardDeviceDataDao.GetWithFloorsByIdAsync(deviceId);
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
            result.ElevatorGroupId = entity.ElevatorGroup?.Id;

            //权限明细
            result.Floors = new List<UnitFloorEntity>();
            if (entity.ElevatorGroup?.RelationFloors?.FirstOrDefault() == null)
            {
                _logger.LogWarning($"派梯设备关联可去楼层为空！");
                return result;
            }

            foreach (var item in entity.ElevatorGroup.RelationFloors)
            {
                if (item?.Floor == null)
                {
                    _logger.LogWarning($"派梯设备关联可去楼层为空！");
                    continue;
                }

                var unitFloor = new UnitFloorEntity();
                unitFloor.Id = item.Id;
                unitFloor.Name = item.Floor.Name;
                unitFloor.RealFloorId = item.Floor.RealFloorId;
                unitFloor.IsPublic = item.Floor.IsPublic;
                unitFloor.EdificeId = item.Floor.Edifice.Id;
                unitFloor.EdificeName = item.Floor.Edifice.Name;
                unitFloor.EditedTime = item.Floor.EditedTime;

                result.Floors.Add(unitFloor);
            }

            return result;
        }

        public async Task<HandledElevatorSuccessModel> RightHandleElevator(RightHandleElevatorModel rightHandleElevator)
        {
            //<HandledElevatorSuccessModel>
            var device = _koneHandleDeviceList.GetById(rightHandleElevator.HandleElevatorDeviceId);
            if (device == null)
            {
                //设备未初始化从数据库中获取并初始化
                var entity = await _cardDeviceDataDao.GetWithServersAndFloorsByIdAsync(rightHandleElevator.HandleElevatorDeviceId);
                if (entity == null)
                {
                    throw CustomException.Run("找不到已经初始化的派梯设备");
                }

                //清除关联，防止死循环
                entity.ElevatorGroup.ElevatorServers.ForEach(x => x.ElevatorGroup = null);

                var model = HandleElevatorDeviceModel.ToModel(entity);
                device = _koneHandleDeviceList.AddOrUpdate(model);
            }

            //获取终端号
            var remoteDevice = _remoteDeviceList.GetById(rightHandleElevator.HandleElevatorDeviceId);
            //var handleElevatorDevice = await _handleElevatorDeviceDao.GetWithFloorsByIdAsync(rightHandleElevator.HandleElevatorDeviceId);

            if (remoteDevice == null)
            {
                throw CustomException.Run($"找不到派梯设备：id:{rightHandleElevator.HandleElevatorDeviceId} ");
            }

            //派梯
            var sourceFloorId = device.HandleElevatorDevice?.Floor?.RealFloorId;
            if (string.IsNullOrEmpty(sourceFloorId))
            {
                throw CustomException.Run($"来源楼层真实楼层id为空！");
            }
            var terminalId = remoteDevice?.RemoteDeviceInfo.TerminalId ?? 0;

            //获取权限
            var right = await _passRightDao.GetBySignAndAccessTypeAsync(rightHandleElevator.Sign, rightHandleElevator.AccessType);
            if (right == null)
            {
                throw CustomException.Run($"派梯通行权限为空！");
            }
            right?.RelationFloors?.ForEach(x => x.PassRight = null);
            _logger.LogInformation($"派梯权限：data:{JsonConvert.SerializeObject(right, JsonUtil.JsonSettings)} ");

            //是否直接派梯
            bool isDirectHandle = false;
            FloorEntity rightDestinationFloor = null;
            //闸机派梯显示屏直接派梯 
            if (right?.Floor != null && remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_GATE_DISPLAY.Value)
            {
                isDirectHandle = true;
                rightDestinationFloor = right.Floor;
            }
            else if (right?.RelationFloors?.FirstOrDefault() != null)
            {
                var rightFloorIds = right.RelationFloors.Select(x => x.Id).ToList();
                var publicFloorCount = device.HandleElevatorDevice?.ElevatorGroup?.Floors?.Count(x => x.IsPublic
                                                                                                        && x.Id != device.HandleElevatorDevice?.Floor?.Id
                                                                                                        && !rightFloorIds.Contains(x.Id));
                //直接派梯，只有一层可去楼层或不考虑公共楼层
                if (right?.RelationFloors?.Count == 1 && publicFloorCount == 0)
                {
                    isDirectHandle = true;
                    rightDestinationFloor = right.RelationFloors.FirstOrDefault().Floor;
                }
            }

            //派梯
            if (isDirectHandle)
            {
                _logger.LogInformation($"直接派梯！！");

                if (string.IsNullOrEmpty(rightDestinationFloor?.RealFloorId))
                {
                    throw CustomException.Run($"目的楼层真实楼层id为空");
                }
                if (sourceFloorId == rightDestinationFloor.RealFloorId)
                {
                    throw CustomException.Run($"来源楼层与目地层相同！");
                }

                //派梯
                device.Handle(sourceFloorId, rightDestinationFloor.RealFloorId, terminalId);

                //派梯成功，直接返回
                var result = new HandledElevatorSuccessModel();
                result.DestinationFloorId = rightDestinationFloor.Id;
                result.DestinationFloorName = rightDestinationFloor.Name;
                result.ElevatorName = 0.ToString();

                _logger.LogInformation($"派梯操作完成：sourceFloorId:{sourceFloorId} destinationFloorId:{rightDestinationFloor.RealFloorId} terminalId:{terminalId} result:{ JsonConvert.SerializeObject(result, JsonUtil.JsonSettings)} ");

                //上传事件
                var passRecord = new PassRecordModel();
                passRecord.Extra = rightHandleElevator.DeviceId;
                passRecord.DeviceType = device.HandleElevatorDevice.DeviceType;
                passRecord.AccessType = rightHandleElevator.AccessType;
                passRecord.PassRightSign = rightHandleElevator.Sign;
                passRecord.DeviceId = rightHandleElevator.HandleElevatorDeviceId;
                passRecord.PassTime = DateTimeUtil.UtcNowMillis();
                passRecord.PassLocalTime = passRecord.PassTime.ToZoneDateTime()?.ToSecondString();
                passRecord.Remark = rightDestinationFloor.Name;

                await _pushRecordHanlder.StartPushAsync(passRecord);

                return result;
            }
            //发送到派梯设备
            else
            {
                _logger.LogInformation($"发送到派梯设备派梯！！");

                if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value)
                {
                    await _distributeHub.Clients.Client(remoteDevice.RemoteDeviceInfo.ConnectionId).SendAsync("RightHandleElevator", rightHandleElevator);
                }
                else
                {
                    _logger.LogInformation($"发送到派梯设备派梯错误：派梯设备类型不正确：type:{remoteDevice.RemoteDeviceInfo.DeviceType} ");
                }
            }


            return null;
            ////获取终端号  
            //var destinationFloor = await _floorService.GetByIdAsync(rightHandleElevator.DestinationFloorId);

            //var destinationFloorId = destinationFloor?.RealFloorId ?? rightHandleElevator.DestinationFloorId;

            //device.Handle(sourceFloorId, destinationFloorId, terminalId);

            //_logger.LogInformation($"派梯操作完成：sourceFloorId:{sourceFloorId} destinationFloorId:{destinationFloorId} terminalId:{terminalId} ");
        }
    }
}
