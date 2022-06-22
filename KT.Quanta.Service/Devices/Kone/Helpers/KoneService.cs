using AutoMapper;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Entity.Kone;
using KT.Quanta.IDao.Kone;
using KT.Quanta.Model.Kone;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Kone.Clients;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Devices.Kone.Requests;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone.Helpers
{
    public class KoneService : IKoneService
    {
        private RemoteDeviceList _remoteDeviceList;
        private readonly KoneSettings _koneSettings;
        private readonly IKoneEliHandleElevatorSequenceList _koneEliHandleElevatorSequenceList;
        private readonly ILogger<KoneService> _logger;
        private readonly IHandleElevatorDeviceDao _handleElevatorDeviceDao;
        private readonly IElevatorGroupDao _elevatorGroupDao;
        private readonly IDopGlobalDefaultAccessMaskDao _dopGlobalDefaultAccessMaskDao;
        private readonly IDopSpecificDefaultAccessMaskDao _dopSpecificDefaultAccessMaskDao;
        private readonly IMapper _mapper;
        private readonly IFloorDao _floorDao;
        private readonly ISystemConfigDao _systemConfigDao;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly CommunicateDeviceList _communicateDeviceList;
        private readonly IEliPassRightHandleElevatorDeviceCallTypeDao _eliPassRightHandleElevatorDeviceCallTypeDao;
        private readonly IRcgifPassRightHandleElevatorDeviceCallTypeDao _rcgifPassRightHandleElevatorDeviceCallTypeDao;
        private readonly IEliOpenAccessForDopMessageTypeDao _eliOpenAccessForDopMessageTypeDao;
        private readonly IElevatorGroupFloorDao _elevatorGroupFloorDao;

        public KoneService(RemoteDeviceList remoteDeviceList,
            IOptions<KoneSettings> koneSettins,
            IKoneEliHandleElevatorSequenceList koneEliHandleElevatorSequenceList,
            ILogger<KoneService> logger,
            IHandleElevatorDeviceDao handleElevatorDeviceDao,
            IElevatorGroupDao elevatorGroupDao,
            IDopGlobalDefaultAccessMaskDao dopGlobalDefaultAccessMaskDao,
            IDopSpecificDefaultAccessMaskDao dopSpecificDefaultAccessMaskDao,
            IMapper mapper,
            IFloorDao floorDao,
            ISystemConfigDao systemConfigDao,
            CommunicateDeviceList communicateDeviceList,
            IServiceScopeFactory serviceScopeFactory,
            IEliPassRightHandleElevatorDeviceCallTypeDao eliPassRightHandleElevatorDeviceCallTypeDao,
            IRcgifPassRightHandleElevatorDeviceCallTypeDao rcgifPassRightHandleElevatorDeviceCallTypeDao,
            IEliOpenAccessForDopMessageTypeDao eliOpenAccessForDopMessageTypeDao,
            IElevatorGroupFloorDao elevatorGroupFloorDao)
        {
            _remoteDeviceList = remoteDeviceList;
            _koneSettings = koneSettins.Value;
            _koneEliHandleElevatorSequenceList = koneEliHandleElevatorSequenceList;
            _logger = logger;
            _handleElevatorDeviceDao = handleElevatorDeviceDao;
            _elevatorGroupDao = elevatorGroupDao;
            _dopGlobalDefaultAccessMaskDao = dopGlobalDefaultAccessMaskDao;
            _dopSpecificDefaultAccessMaskDao = dopSpecificDefaultAccessMaskDao;
            _mapper = mapper;
            _floorDao = floorDao;
            _systemConfigDao = systemConfigDao;
            _communicateDeviceList = communicateDeviceList;
            _serviceScopeFactory = serviceScopeFactory;
            _eliPassRightHandleElevatorDeviceCallTypeDao = eliPassRightHandleElevatorDeviceCallTypeDao;
            _rcgifPassRightHandleElevatorDeviceCallTypeDao = rcgifPassRightHandleElevatorDeviceCallTypeDao;
            _eliOpenAccessForDopMessageTypeDao = eliOpenAccessForDopMessageTypeDao;
            _elevatorGroupFloorDao = elevatorGroupFloorDao;
        }

        public async Task DeleteDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model, string addressKey)
        {
            model.MaskFloors = new List<DopSpecificDefaultAccessFloorMaskModel>();
            await SetDopSpecificMaskAsync(model, addressKey);
        }

        public async Task SetDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model, string addressKey)
        {
            var elevatorGroupRemoteDevice = await _remoteDeviceList.GetByIdAsync(model.ElevatorGroupId);
            if (elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceType != DeviceTypeEnum.ELEVATOR_SERVER_GROUP.Value)
            {
                throw new ArgumentException(elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceType);
            }
            if (elevatorGroupRemoteDevice.RemoteDeviceInfo.BrandModel != BrandModelEnum.KONE_DCS.Value)
            {
                throw new ArgumentException(elevatorGroupRemoteDevice.RemoteDeviceInfo.BrandModel);
            }


            var messageIds = new List<string>();

            var connectMask = new KoneEliDopSpecificDefaultAccessMaskMessage();
            connectMask.SystemState = (byte)model.ConnectedState;

            //派梯设备查询DopId与所在楼层真实Id
            var handleElevatorDevice = await _handleElevatorDeviceDao.GetWithFloorByIdAsync(model.HandleElevatorDeviceId);
            if (handleElevatorDevice == null)
            {
                throw new Exception($"派梯设备为空：id:[{model.HandleElevatorDeviceId}] ");
            }
            if (handleElevatorDevice.Floor == null)
            {
                throw new Exception($"派梯设备楼层为空：id:[{model.HandleElevatorDevice.FloorId}] ");
            }
            connectMask.DopId = Convert.ToByte(handleElevatorDevice.DeviceId);

            var handleElevatorDeviceElevatorGroupFloor = await _elevatorGroupFloorDao.GetWithFloorByElevatorGroupIdAndFloorIdAsync(handleElevatorDevice.ElevatorGroupId, handleElevatorDevice.Floor.Id);
            if (handleElevatorDeviceElevatorGroupFloor == null)
            {
                throw new Exception($"派梯设备电梯组楼层为空：elevatorGroupId:{handleElevatorDevice.ElevatorGroupId} floorId:{handleElevatorDevice.Floor.Id} ");
            }
            connectMask.DopFloorId = Convert.ToByte(handleElevatorDeviceElevatorGroupFloor.RealFloorId);

            await SetSpecificMaskAndSendAsync(elevatorGroupRemoteDevice,
                messageIds,
                connectMask,
                true,
                model,
                addressKey);
        }

        private async Task SetSpecificMaskAndSendAsync(IRemoteDevice elevatorGroupRemoteDevice,
            List<string> messageIds,
            KoneEliDopSpecificDefaultAccessMaskMessage maskMessage,
            bool isAccessableFloor,
            DopSpecificDefaultAccessMaskModel maskModel,
            string addressKey)
        {
            //floorNumber为0时清空模板，不设置楼层
            if (maskModel.MaskFloors?.FirstOrDefault() != null)
            {
                // 获取真实楼层
                var floorIds = maskModel.MaskFloors.Select(x => x.FloorId).ToList();
                var floorEntities = await _elevatorGroupFloorDao.GetWithFloorsByElevatorGroupIdAndFloorIdsAsync(elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceId, floorIds);
                if (floorEntities?.FirstOrDefault() != null)
                {
                    uint defaultValue = 0;
                    if (isAccessableFloor)
                    {
                        defaultValue = 0;
                    }
                    else
                    {
                        //0b00000011=3
                        defaultValue = 3;
                    }

                    //获取最大楼层数，可能存在空楼层
                    var maxRealFloorId = Convert.ToInt32(floorEntities.OrderByDescending(x => Convert.ToInt32(x.RealFloorId)).FirstOrDefault().RealFloorId);

                    //Mask楼层前后门权限  
                    for (int i = 1; i <= maxRealFloorId; i++)
                    {
                        /// bit 0 : allowed destination front (1=yes, 0=no)
                        /// bit 1 : allowed destination rear(1=yes, 0=no)
                        /// bit 2-31 : reserved
                        var floorEntity = floorEntities.FirstOrDefault(x => x.RealFloorId == i.ToString());
                        var floorMask = maskModel.MaskFloors.FirstOrDefault(x => x.FloorId == floorEntity?.Id);
                        if (floorMask != null)
                        {
                            var strValue = new StringBuilder();
                            strValue.Append("000000");
                            strValue.Append(floorMask.IsDestinationRear ? "1" : "0");
                            strValue.Append(floorMask.IsDestinationFront ? "1" : "0");

                            var value = Convert.ToUInt32(strValue.ToString(), 2);
                            maskMessage.AccessMasks.Add(value);
                        }
                        else
                        {
                            //0b00000011=3
                            maskMessage.AccessMasks.Add(defaultValue);
                        }
                    }
                }
            }

            var messageId = IdUtil.NewId();
            messageIds.Add(messageId);

            //向派梯关系列表中增加数据，返回派梯结果时使用
            var sequence = new KoneEliSequenceModel();
            sequence.Id = IdUtil.NewId();
            sequence.Type = KoneEliSequenceTypeEnum.DopSepcificDefaultMask;
            sequence.Data = maskModel;
            sequence.MessageId = messageId;
            sequence.HandleElevatorDeviceId = elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceId;
            _koneEliHandleElevatorSequenceList.Add(maskMessage.MessageId, sequence);

            _logger.LogInformation($"开始设置DOP全局模板：{JsonConvert.SerializeObject(maskMessage, JsonUtil.JsonPrintSettings)} ");

            //保存记录
            var dopMaskRecord = new DopMaskRecordEntity();
            dopMaskRecord.SequenceId = sequence.Id;
            dopMaskRecord.Type = sequence.Type.Value;
            dopMaskRecord.Operate = KoneEliMaskRecoredOperateEnum.Send.Value;
            dopMaskRecord.SendData = JsonConvert.SerializeObject(maskModel, JsonUtil.JsonSettings);

            await SendDataAsync(elevatorGroupRemoteDevice, maskMessage, addressKey, dopMaskRecord);

            _logger.LogInformation($"结束设置DOP全局模板！");
        }

        public async Task DeleteDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model, string addressKey)
        {
            model.MaskFloors = new List<DopGlobalDefaultAccessFloorMaskModel>();
            await SetDopGlobalMaskAsync(model, addressKey);
        }

        public async Task SetDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model, string addressKey)
        {
            var elevatorGroupRemoteDevice = await _remoteDeviceList.GetByIdAsync(model.ElevatorGroupId);
            if (elevatorGroupRemoteDevice == null)
            {
                throw new Exception($"电梯组为空：id:{model.ElevatorGroupId} ");
            }
            if (elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceType != DeviceTypeEnum.ELEVATOR_SERVER_GROUP.Value)
            {
                throw new ArgumentException(elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceType);
            }
            if (elevatorGroupRemoteDevice.RemoteDeviceInfo.BrandModel != BrandModelEnum.KONE_DCS.Value)
            {
                throw new ArgumentException(elevatorGroupRemoteDevice.RemoteDeviceInfo.BrandModel);
            }

            var messageIds = new List<string>();

            var connectMask = new KoneEliDopGlobalDefaultAccessMaskMessage();
            connectMask.SystemState = (byte)model.ConnectedState;

            await SetGlobalMaskAndSendAsync(elevatorGroupRemoteDevice,
                messageIds,
                connectMask,
                true,
                model,
                addressKey);
        }

        private async Task SetGlobalMaskAndSendAsync(IRemoteDevice elevatorGroupRemoteDevice,
            List<string> messageIds,
            KoneEliDopGlobalDefaultAccessMaskMessage maskMessage,
            bool isAccessableFloor,
           DopGlobalDefaultAccessMaskModel maskModel,
            string addressKey)
        {
            //floorNumber为0时清空模板，不设置楼层
            if (maskModel.MaskFloors?.FirstOrDefault() != null)
            {
                // 获取真实楼层
                var floorIds = maskModel.MaskFloors.Select(x => x.FloorId).ToList();
                var floorEntities = await _elevatorGroupFloorDao.GetWithFloorsByElevatorGroupIdAndFloorIdsAsync(elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceId, floorIds);
                if (floorEntities?.FirstOrDefault() != null)
                {
                    uint defaultValue = 0;
                    if (isAccessableFloor)
                    {
                        defaultValue = 0;
                    }
                    else
                    {
                        //0b00011011=27 
                        defaultValue = 27;
                    }

                    //获取最大楼层数，可能存在空楼层
                    var maxRealFloorId = Convert.ToInt32(floorEntities.OrderByDescending(x => Convert.ToInt32(x.RealFloorId)).FirstOrDefault().RealFloorId);

                    //Mask楼层前后门权限  
                    for (int i = 1; i <= maxRealFloorId; i++)
                    {
                        // bit 0 : allowed destination front (1=yes, 0=no)
                        // bit 1 : allowed destination rear(1=yes, 0=no)
                        // bit 2 : reserved
                        // bit 3 : allowed source front(1=yes, 0=no)
                        // bit 4 : allowed source rear(1=yes, 0=no)
                        // bit 5-31 : reserved
                        var floorEntity = floorEntities.FirstOrDefault(x => x.RealFloorId == i.ToString());
                        var floorMask = maskModel.MaskFloors.FirstOrDefault(x => x.FloorId == floorEntity?.Id);
                        if (floorMask != null)
                        {
                            var strValue = new StringBuilder();
                            strValue.Append("000");
                            strValue.Append(floorMask.IsSourceRear ? "1" : "0");
                            strValue.Append(floorMask.IsSourceFront ? "1" : "0");
                            strValue.Append("0");
                            strValue.Append(floorMask.IsDestinationRear ? "1" : "0");
                            strValue.Append(floorMask.IsDestinationFront ? "1" : "0");

                            var value = Convert.ToUInt32(strValue.ToString(), 2);
                            maskMessage.AccessMasks.Add(value);
                        }
                        else
                        {
                            //0b00011011=27 
                            maskMessage.AccessMasks.Add(defaultValue);
                        }
                    }
                }
            }

            var messageId = IdUtil.NewId();
            messageIds.Add(messageId);

            //向派梯关系列表中增加数据，返回派梯结果时使用
            var sequence = new KoneEliSequenceModel();
            sequence.Id = IdUtil.NewId();
            sequence.Type = KoneEliSequenceTypeEnum.DopGlobalDefaultMask;
            sequence.Data = maskModel;
            sequence.MessageId = messageId;
            sequence.HandleElevatorDeviceId = elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceId;
            _koneEliHandleElevatorSequenceList.Add(maskMessage.MessageId, sequence);

            _logger.LogInformation($"开始设置DOP全局模板：{JsonConvert.SerializeObject(maskMessage, JsonUtil.JsonPrintSettings)} ");

            //保存记录
            var dopMaskRecord = new DopMaskRecordEntity();
            dopMaskRecord.SequenceId = sequence.Id;
            dopMaskRecord.Type = sequence.Type.Value;
            dopMaskRecord.Operate = KoneEliMaskRecoredOperateEnum.Send.Value;
            dopMaskRecord.SendData = JsonConvert.SerializeObject(maskModel, JsonUtil.JsonSettings);

            await SendDataAsync(elevatorGroupRemoteDevice, maskMessage, addressKey, dopMaskRecord);

            _logger.LogInformation($"结束设置DOP全局模板！");
        }

        private async Task SendDataAsync<T>(IRemoteDevice elevatorGroupRemoteDevice,
            T messageModel,
            string addressKey,
            DopMaskRecordEntity dopMaskRecorda) where T : KoneEliSubHeader1Request
        {
            await _remoteDeviceList.AsyncExecuteAsync(
                (remoteDevice) =>
                {
                    if (remoteDevice.RemoteDeviceInfo.ParentId == elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceId)
                    {
                        if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.KONE_DCS.Value)
                        {
                            if (remoteDevice.RemoteDeviceInfo.ModuleType == KoneModuleTypeEnum.ELI.Value)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                },
                async (remoteDevice) =>
                {
                    var newMessageModel = _mapper.Map<T>(messageModel);
                    var enwDopMaskRecord = _mapper.Map<DopMaskRecordEntity>(dopMaskRecorda);
                    foreach (var item in remoteDevice.CommunicateDevices)
                    {
                        try
                        {
                            var client = item.GetLoginUserClient<IKoneEliClientHost>();
                            if (string.IsNullOrEmpty(addressKey) || addressKey == $"{item.CommunicateDeviceInfo.IpAddress}:{item.CommunicateDeviceInfo.Port}")
                            {
                                _logger.LogInformation($"{addressKey} kone发送全局模板：{JsonConvert.SerializeObject(newMessageModel)} ");
                                await client.SendAsync(newMessageModel);

                                await SaveRecordAsync(item, enwDopMaskRecord, true, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "kone发送模错误！");
                            await SaveRecordAsync(item, enwDopMaskRecord, false, ex);
                        }
                    }
                });
        }

        private async Task SaveRecordAsync(ICommunicateDevice communicateDevice, DopMaskRecordEntity dopMaskRecord, bool isSuccess, Exception exception)
        {
            try
            {
                dopMaskRecord.Id = IdUtil.NewId();
                dopMaskRecord.ElevatorServer = $"{communicateDevice.CommunicateDeviceInfo.IpAddress}:{communicateDevice.CommunicateDeviceInfo.Port}";

                dopMaskRecord.IsSuccess = isSuccess;
                if (exception != null)
                {
                    dopMaskRecord.Message = exception.ToString();
                }

                using var scope = _serviceScopeFactory.CreateScope();
                var dopMaskRecordDao = scope.ServiceProvider.GetRequiredService<IDopMaskRecordDao>();
                await dopMaskRecordDao.InsertAsync(dopMaskRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"保存Mask发送错误：" +
                    $"address:[{dopMaskRecord.ElevatorServer}] " +
                    $"data:{JsonConvert.SerializeObject(dopMaskRecord, JsonUtil.JsonPrintSettings)} ");
            }
        }

        public async Task SetSystemConfigAsync(KoneSystemConfigModel model)
        {
            if (model.OpenAccessForDopMessageType != 1 && model.OpenAccessForDopMessageType != 2)
            {
                throw new ArgumentException($"Open access for dop message type value error,need 1 or 2,but the value is {model.OpenAccessForDopMessageType} !");
            }

            var modelType = model.GetType();
            var propertyInfos = modelType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            var keys = propertyInfos.Select(x => $"{modelType.Name}_{x.Name}").ToList();
            var entities = await _systemConfigDao.SelectByLambdaAsync(x => keys.Contains(x.Key));
            foreach (var item in propertyInfos)
            {
                var key = $"{modelType.Name}_{item.Name}";
                var entity = entities?.FirstOrDefault(x => x.Key == key);
                if (entity == null)
                {
                    entity = new SystemConfigEntity();
                    entity.Key = key;
                    entity.Value = item.GetValue(model)?.ToString();

                    await _systemConfigDao.InsertAsync(entity, false);
                }
                else
                {
                    entity.Value = item.GetValue(model)?.ToString();

                    await _systemConfigDao.UpdateAsync(entity, false);
                }
            }

            await _systemConfigDao.SaveChangesAsync();

            await RefreshKoneConfigHekperAsync();
        }

        public async Task<KoneSystemConfigModel> GetSystemConfigAsync()
        {
            var resullt = new KoneSystemConfigModel();
            var modelType = resullt.GetType();
            var propertyInfos = modelType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            var keys = propertyInfos.Select(x => $"{modelType.Name}_{x.Name}").ToList();
            var entities = await _systemConfigDao.SelectByLambdaAsync(x => keys.Contains(x.Key));
            foreach (var item in propertyInfos)
            {
                var key = $"{modelType.Name}_{item.Name}";
                var entity = entities?.FirstOrDefault(x => x.Key == key);
                if (entity == null)
                {
                    continue;
                }

                var value = Convert.ToUInt16(entity.Value);
                item.SetValue(resullt, value);
            }

            return resullt;
        }

        public async Task RefreshKoneConfigHekperAsync()
        {
            KoneHelper.SystemConfig = await GetSystemConfigAsync();
        }

        public async Task SetDopMaskAsync(string remoteIp, int remotePort)
        {
            var addressKey = $"{remoteIp}:{remotePort}";
            var elevatorGroups = await _elevatorGroupDao.SelectByLambdaAsync(x => x.ElevatorServers.Any(y => y.IpAddress == remoteIp && y.Port == remotePort));
            if (elevatorGroups?.FirstOrDefault() == null)
            {
                throw new Exception($"根据电梯服务找不到电梯组：remoteIp:{remoteIp} remotePort:{remotePort} ");
            }

            foreach (var elevatorGroup in elevatorGroups)
            {
                //全局Mask
                var dopGlobalMasks = await _dopGlobalDefaultAccessMaskDao.GetWithFloorsByGroupIdAsync(elevatorGroup.Id);
                if (dopGlobalMasks?.FirstOrDefault() != null)
                {
                    foreach (var dopGlobalMask in dopGlobalMasks)
                    {
                        var dopGlobalMaskModel = _mapper.Map<DopGlobalDefaultAccessMaskModel>(dopGlobalMask);
                        await SetDopGlobalMaskAsync(dopGlobalMaskModel, addressKey);
                    }
                }

                //特定Mask
                var dopSpecificMasks = await _dopSpecificDefaultAccessMaskDao.GetWithFloorsByGroupIdAsync(elevatorGroup.Id);
                if (dopSpecificMasks?.FirstOrDefault() != null)
                {
                    dopSpecificMasks = dopSpecificMasks
                        .OrderByDescending(x => x.ConnectedState)
                        .OrderBy(x => x.HandleElevatorDevice?.Name)
                        .ToList();
                    foreach (var dopSpecificMask in dopSpecificMasks)
                    {
                        var dopSpecificMaskModel = _mapper.Map<DopSpecificDefaultAccessMaskModel>(dopSpecificMask);
                        await SetDopSpecificMaskAsync(dopSpecificMaskModel, addressKey);
                    }
                }
            }
        }

        public async Task<DopSpecificDefaultAccessMaskModel> GetDopSpecificMaskByIdAsync(string id)
        {
            var entity = await _dopSpecificDefaultAccessMaskDao.GetWithFloorsByIdAsync(id);

            return _mapper.Map<DopSpecificDefaultAccessMaskModel>(entity);
        }

        public async Task<List<DopSpecificDefaultAccessMaskModel>> GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDeviceAsync()
        {
            var entities = await _dopSpecificDefaultAccessMaskDao.GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDeviceAsync();

            return _mapper.Map<List<DopSpecificDefaultAccessMaskModel>>(entities);
        }

        public async Task DeleteAndSaveDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model)
        {
            await DeleteDopSpecificMaskAsync(model, string.Empty);

            await _dopSpecificDefaultAccessMaskDao.DeleteByIdAsync(model.Id);
        }

        public async Task DeleteRoyalDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model)
        {
            await DeleteDopSpecificMaskAsync(model, string.Empty);
        }

        public async Task SetAndSaveDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model)
        {
            var entity = await _dopSpecificDefaultAccessMaskDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = _mapper.Map<DopSpecificDefaultAccessMaskEntity>(model);
                entity.Id = IdUtil.NewId();
                if (entity.MaskFloors?.FirstOrDefault() != null)
                {
                    foreach (var item in entity.MaskFloors)
                    {
                        item.Id = IdUtil.NewId();
                        item.Floor = await _floorDao.SelectByIdAsync(item.FloorId);
                    }
                }
                entity.ElevatorGroup = await _elevatorGroupDao.SelectByIdAsync(entity.ElevatorGroupId);
                entity.HandleElevatorDevice = await _handleElevatorDeviceDao.SelectByIdAsync(entity.HandleElevatorDeviceId);
                await _dopSpecificDefaultAccessMaskDao.InsertAsync(entity);
            }
            else
            {
                model.Id = entity.Id;
                entity = _mapper.Map(model, entity);

                entity.MaskFloors = await _dopSpecificDefaultAccessMaskDao.SelectRelevanceObjectsAsync<DopSpecificDefaultAccessFloorMaskEntity>(x => x.AccessMaskId == entity.Id);

                if (model.MaskFloors?.FirstOrDefault() != null)
                {
                    if (entity.MaskFloors?.FirstOrDefault() == null)
                    {
                        entity.MaskFloors = new List<DopSpecificDefaultAccessFloorMaskEntity>();
                    }

                    foreach (var item in model.MaskFloors)
                    {
                        var maskFloor = entity.MaskFloors.FirstOrDefault(x => x.FloorId == item.FloorId);
                        if (maskFloor == null)
                        {
                            maskFloor = _mapper.Map<DopSpecificDefaultAccessFloorMaskEntity>(item);
                            entity.MaskFloors.Add(maskFloor);
                        }
                        else
                        {
                            item.Id = maskFloor.Id;
                            maskFloor = _mapper.Map(item, maskFloor);
                        }
                        maskFloor.Floor = await _floorDao.SelectByIdAsync(item.FloorId);
                    }

                    entity.MaskFloors.RemoveAll(x => !model.MaskFloors.Any(y => y.FloorId == x.FloorId));
                }
                else
                {
                    entity.MaskFloors = new List<DopSpecificDefaultAccessFloorMaskEntity>();
                }
                entity.ElevatorGroup = await _elevatorGroupDao.SelectByIdAsync(entity.ElevatorGroupId);
                entity.HandleElevatorDevice = await _handleElevatorDeviceDao.SelectByIdAsync(entity.HandleElevatorDeviceId);
                await _dopSpecificDefaultAccessMaskDao.UpdateAsync(entity);
            }

            await SetDopSpecificMaskAsync(model, string.Empty);
        }

        public async Task<DopGlobalDefaultAccessMaskModel> GetDopGlobalMaskByIdAsync(string id)
        {
            var entity = await _dopGlobalDefaultAccessMaskDao.GetWithFloorsByIdAsync(id);

            return _mapper.Map<DopGlobalDefaultAccessMaskModel>(entity);
        }

        public async Task<List<DopGlobalDefaultAccessMaskModel>> GetAllDopGlobalMasksWithElevatorGroupAsync()
        {
            var entities = await _dopGlobalDefaultAccessMaskDao.GetAllDopGlobalMasksWithElevatorGroupAsync();

            return _mapper.Map<List<DopGlobalDefaultAccessMaskModel>>(entities);
        }

        public async Task DeleteAndSaveDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model)
        {
            await DeleteDopGlobalMaskAsync(model, string.Empty);

            await _dopGlobalDefaultAccessMaskDao.DeleteByIdAsync(model.Id);
        }

        public async Task DeleteRoyalDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model)
        {
            await DeleteDopGlobalMaskAsync(model, string.Empty);
        }

        public async Task SetAndSaveDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model)
        {
            var entity = await _dopGlobalDefaultAccessMaskDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = _mapper.Map<DopGlobalDefaultAccessMaskEntity>(model);
                if (entity.MaskFloors?.FirstOrDefault() != null)
                {
                    foreach (var item in entity.MaskFloors)
                    {
                        item.Id = IdUtil.NewId();
                        item.Floor = await _floorDao.SelectByIdAsync(item.FloorId);
                    }
                }
                entity.ElevatorGroup = await _floorDao.SelectRelevanceObjectAsync<ElevatorGroupEntity>(entity.ElevatorGroupId);
                await _dopGlobalDefaultAccessMaskDao.InsertAsync(entity);
            }
            else
            {
                model.Id = entity.Id;
                entity = _mapper.Map(model, entity);

                entity.MaskFloors = await _dopGlobalDefaultAccessMaskDao.SelectRelevanceObjectsAsync<DopGlobalDefaultAccessFloorMaskEntity>(x => x.AccessMaskId == entity.Id);

                if (model.MaskFloors?.FirstOrDefault() != null)
                {
                    if (entity.MaskFloors?.FirstOrDefault() == null)
                    {
                        entity.MaskFloors = new List<DopGlobalDefaultAccessFloorMaskEntity>();
                    }

                    foreach (var item in model.MaskFloors)
                    {
                        var maskFloor = entity.MaskFloors.FirstOrDefault(x => x.FloorId == item.FloorId);
                        if (maskFloor == null)
                        {
                            maskFloor = _mapper.Map<DopGlobalDefaultAccessFloorMaskEntity>(item);
                            entity.MaskFloors.Add(maskFloor);
                        }
                        else
                        {
                            item.Id = maskFloor.Id;
                            maskFloor = _mapper.Map(item, maskFloor);
                        }
                        maskFloor.Floor = await _floorDao.SelectByIdAsync(item.FloorId);
                    }

                    entity.MaskFloors.RemoveAll(x => !model.MaskFloors.Any(y => y.FloorId == x.FloorId));
                }
                else
                {
                    entity.MaskFloors = new List<DopGlobalDefaultAccessFloorMaskEntity>();
                }
                entity.ElevatorGroup = await _floorDao.SelectRelevanceObjectAsync<ElevatorGroupEntity>(entity.ElevatorGroupId);
                await _dopGlobalDefaultAccessMaskDao.UpdateAsync(entity);
            }

            await SetDopGlobalMaskAsync(model, string.Empty);
        }

        public async Task SafelyColseHostAsync()
        {
            //关闭通力电梯
            await _communicateDeviceList.AsyncExecuteAsync(
                (communicateDevice) =>
                {
                    if (communicateDevice != null)
                    {
                        if (communicateDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_ELI.Value
                            || communicateDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_RCGIF.Value)
                        {
                            return true;
                        }
                    }
                    return false;
                },
                async (communicateDevice) =>
                {
                    await communicateDevice.CloseAsync();
                });
        }

        public async Task<List<EliPassRightHandleElevatorDeviceCallTypeModel>> GetEliPassRightHandleElevatorDeviceCallTypesByPassRightSign(string passRightSign)
        {
            var entities = await _eliPassRightHandleElevatorDeviceCallTypeDao.SelectByLambdaAsync(x => x.PassRightSign == passRightSign);

            return _mapper.Map<List<EliPassRightHandleElevatorDeviceCallTypeModel>>(entities);
        }

        public async Task AddOrEditEliPassRightHandleElevatorDeviceCallTypes(List<EliPassRightHandleElevatorDeviceCallTypeModel> models)
        {
            foreach (var model in models)
            {
                var entity = await _eliPassRightHandleElevatorDeviceCallTypeDao.SelectFirstByLambdaAsync(x => x.PassRightSign == model.PassRightSign
                    && x.HandleElevatorDeviceId == model.HandleElevatorDeviceId);
                if (entity != null)
                {
                    //更新
                    model.Id = entity.Id;
                    entity = _mapper.Map(model, entity);
                    await _eliPassRightHandleElevatorDeviceCallTypeDao.UpdateAsync(entity);
                }
                else
                {
                    //新增
                    entity = _mapper.Map<EliPassRightHandleElevatorDeviceCallTypeEntity>(model);
                    await _eliPassRightHandleElevatorDeviceCallTypeDao.InsertAsync(entity);
                }
            }
        }

        public async Task DeleteEliPassRightHandleElevatorDeviceCallTypes(List<EliPassRightHandleElevatorDeviceCallTypeModel> models)
        {
            foreach (var model in models)
            {
                await _eliPassRightHandleElevatorDeviceCallTypeDao.DeleteByIdAsync(model.Id, false);
            }
            await _eliPassRightHandleElevatorDeviceCallTypeDao.SaveChangesAsync();
        }

        public async Task<List<RcgifPassRightHandleElevatorDeviceCallTypeModel>> GetRcgifPassRightHandleElevatorDeviceCallTypesByPassRightSign(string passRightSign)
        {
            var entities = await _rcgifPassRightHandleElevatorDeviceCallTypeDao.SelectByLambdaAsync(x => x.PassRightSign == passRightSign);

            return _mapper.Map<List<RcgifPassRightHandleElevatorDeviceCallTypeModel>>(entities);
        }

        public async Task AddOrEditRcgifPassRightHandleElevatorDeviceCallTypes(List<RcgifPassRightHandleElevatorDeviceCallTypeModel> models)
        {
            foreach (var model in models)
            {
                var entity = await _rcgifPassRightHandleElevatorDeviceCallTypeDao.SelectFirstByLambdaAsync(x => x.PassRightSign == model.PassRightSign
                    && x.HandleElevatorDeviceId == model.HandleElevatorDeviceId);
                if (entity != null)
                {
                    //更新
                    model.Id = entity.Id;
                    entity = _mapper.Map(model, entity);
                    await _rcgifPassRightHandleElevatorDeviceCallTypeDao.UpdateAsync(entity);
                }
                else
                {
                    //新增
                    entity = _mapper.Map<RcgifPassRightHandleElevatorDeviceCallTypeEntity>(model);
                    await _rcgifPassRightHandleElevatorDeviceCallTypeDao.InsertAsync(entity);
                }
            }
        }
        public async Task DeleteRcgifPassRightHandleElevatorDeviceCallTypes(List<RcgifPassRightHandleElevatorDeviceCallTypeModel> models)
        {

            foreach (var model in models)
            {
                await _rcgifPassRightHandleElevatorDeviceCallTypeDao.DeleteByIdAsync(model.Id, false);
            }
            await _rcgifPassRightHandleElevatorDeviceCallTypeDao.SaveChangesAsync();
        }


        public async Task<List<EliOpenAccessForDopMessageTypeModel>> GetEliOpenAccessForDopMessageTypesByPassRightSign(string passRightSign)
        {
            var entities = await _eliOpenAccessForDopMessageTypeDao.SelectByLambdaAsync(x => x.PassRightSign == passRightSign);

            return _mapper.Map<List<EliOpenAccessForDopMessageTypeModel>>(entities);
        }

        public async Task AddOrEditEliOpenAccessForDopMessageTypes(List<EliOpenAccessForDopMessageTypeModel> models)
        {
            foreach (var model in models)
            {
                var entity = await _eliOpenAccessForDopMessageTypeDao.SelectFirstByLambdaAsync(x => x.PassRightSign == model.PassRightSign
                    && x.HandleElevatorDeviceId == model.HandleElevatorDeviceId);
                if (entity != null)
                {
                    //更新
                    model.Id = entity.Id;
                    entity = _mapper.Map(model, entity);
                    await _eliOpenAccessForDopMessageTypeDao.UpdateAsync(entity);
                }
                else
                {
                    //新增
                    entity = _mapper.Map<EliOpenAccessForDopMessageTypeEntity>(model);
                    await _eliOpenAccessForDopMessageTypeDao.InsertAsync(entity);
                }
            }
        }
        public async Task DeleteEliOpenAccessForDopMessageTypes(List<EliOpenAccessForDopMessageTypeModel> models)
        {

            foreach (var model in models)
            {
                await _eliOpenAccessForDopMessageTypeDao.DeleteByIdAsync(model.Id, false);
            }
            await _eliOpenAccessForDopMessageTypeDao.SaveChangesAsync();
        }

    }
}
