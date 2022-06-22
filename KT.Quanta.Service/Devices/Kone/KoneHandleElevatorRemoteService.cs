using AutoMapper;
using BigMath;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Model.Kone;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Devices.Kone.Clients;
using KT.Quanta.Service.Devices.Kone.Helpers;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Devices.Kone.Requests;
using KT.Quanta.Service.IDaos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone
{
    public class KoneHandleElevatorRemoteService : IHandleElevatorRemoteService
    {
        private ILogger<KoneHandleElevatorRemoteService> _logger;
        private readonly IKoneRcgifHandleElevatorSequenceList _koneRcgifHandleElevatorSequenceList;
        private readonly IKoneEliHandleElevatorSequenceList _koneEliHandleElevatorSequenceList;
        private readonly RemoteDeviceList _remoteDeviceList;
        private readonly KoneSettings _koneSettings;
        private readonly IMapper _mapper;
        private readonly IPassRightDao _passRightDao;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public KoneHandleElevatorRemoteService(ILogger<KoneHandleElevatorRemoteService> logger,
             IKoneRcgifHandleElevatorSequenceList koneRcgifHandleElevatorSequenceList,
             IKoneEliHandleElevatorSequenceList koneEliHandleElevatorSequenceList,
             RemoteDeviceList remoteDeviceList,
             IOptions<KoneSettings> koneSettings,
             IMapper mapper,
             IPassRightDao passRightDao,
             IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _koneRcgifHandleElevatorSequenceList = koneRcgifHandleElevatorSequenceList;
            _koneEliHandleElevatorSequenceList = koneEliHandleElevatorSequenceList;
            _remoteDeviceList = remoteDeviceList;
            _koneSettings = koneSettings.Value;
            _mapper = mapper;
            _passRightDao = passRightDao;
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="elevatorGroupRemoteDevice">电梯组</param>
        /// <param name="distributeHandle">派梯参数</param> 
        public async Task DirectCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeHandleElevatorModel distributeHandle)
        {
            var model = new KoneRcgifDestinationCallMessage();
            model.DestCallType = KoneHelper.SystemConfig.AcsBypassCallType;
            model.TerminalId = ConvertUtil.ToUInt16(distributeHandle.RealDeviceId, 0);
            model.SourceFloor = (byte)distributeHandle.SourceFloor.Floor;
            try
            {
                model.PassengerId = Int128.Parse(distributeHandle.CardNumber);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"通力派梯卡号错误：{distributeHandle.CardNumber} ");
            }
            if (distributeHandle.SourceFloor.IsFront)
            {
                model.SourceSide = 0;
            }
            else
            {
                model.SourceSide = 1;
            }
            model.DestinationFloor = (byte)distributeHandle.DestinationFloor.Floor;
            if (distributeHandle.DestinationFloor.IsFront)
            {
                model.DestinationSide = 0;
            }
            else
            {
                model.DestinationSide = 1;
            }

            //设置calltype
            var destCallType = await GetRcgifCallType(distributeHandle);
            if (destCallType != null)
            {
                model.DestCallType = destCallType.Value;
            }

            //向派梯关系列表中增加数据，返回派梯结果时使用
            var sequence = new KoneRcgifSequenceModel();
            sequence.MessageId = distributeHandle.MessageId;
            sequence.HandleElevatorDeviceId = distributeHandle.DeviceId;
            _koneRcgifHandleElevatorSequenceList.Add(model.MessageId, sequence);

            _logger.LogInformation($"开始派梯：{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

            await _remoteDeviceList.AsyncExecuteAsync(
                (remoteDevice) =>
                {
                    if (remoteDevice.RemoteDeviceInfo.ParentId == elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceId)
                    {
                        if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.KONE_DCS.Value)
                        {
                            if (remoteDevice.RemoteDeviceInfo.ModuleType == KoneModuleTypeEnum.RCGIF.Value)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                },
                async (remoteDevice) =>
                {
                    foreach (var item in remoteDevice.CommunicateDevices)
                    {
                        var client = item.GetLoginUserClient<IKoneRcgifClientHost>();
                        Task.Run(async () =>
                        {
                            //var newModel = TransExp<KoneRcgifDestinationCallMessage, KoneRcgifDestinationCallMessage>.Trans(model);
                            var newModel = _mapper.Map<KoneRcgifDestinationCallMessage>(model);
                            try
                            {
                                await client.SendAsync(newModel);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "kone发送数据错误！");
                            }
                        });
                    }
                });

            _logger.LogInformation($"结束派梯！");
        }

        private async Task<ushort?> GetRcgifCallType(DistributeHandleElevatorModel distributeHandle)
        {
            // 获取派梯类型  
            using var scope = _serviceScopeFactory.CreateScope();
            var rcgifPassRightHandleElevatorDeviceCallTypeDao = scope.ServiceProvider.GetRequiredService<IRcgifPassRightHandleElevatorDeviceCallTypeDao>();
            var passRightHandleElevatorDeviceCallType = await rcgifPassRightHandleElevatorDeviceCallTypeDao.SelectFirstByLambdaAsync(x => x.HandleElevatorDeviceId == distributeHandle.DeviceId
                   && x.PassRightSign == distributeHandle.CardNumber);
            if (passRightHandleElevatorDeviceCallType != null)
            {
                return (ushort)passRightHandleElevatorDeviceCallType.CallType;
            }
            return null;
        }

        private async Task<ushort?> GetEliCallType(DistributeMultiFloorHandleElevatorModel distributeHandle)
        {
            // 获取派梯类型   
            using var scope = _serviceScopeFactory.CreateScope();
            var eliPassRightHandleElevatorDeviceCallTypeDao = scope.ServiceProvider.GetRequiredService<IEliPassRightHandleElevatorDeviceCallTypeDao>();
            var passRightHandleElevatorDeviceCallType = await eliPassRightHandleElevatorDeviceCallTypeDao.SelectFirstByLambdaAsync(x => x.HandleElevatorDeviceId == distributeHandle.HandleElevatorDeviceId
                   && x.PassRightSign == distributeHandle.CardNumber);
            if (passRightHandleElevatorDeviceCallType != null)
            {
                return (ushort)passRightHandleElevatorDeviceCallType.CallType;
            }
            return null;
        }

        private async Task<ushort?> GetEliMessageType(DistributeMultiFloorHandleElevatorModel distributeHandle)
        {
            // 获取派梯类型   
            using var scope = _serviceScopeFactory.CreateScope();
            var eliOpenAccessForDopMessageTypeDao = scope.ServiceProvider.GetRequiredService<IEliOpenAccessForDopMessageTypeDao>();
            var passRightHandleElevatorDeviceCallType = await eliOpenAccessForDopMessageTypeDao.SelectFirstByLambdaAsync(x => x.HandleElevatorDeviceId == distributeHandle.HandleElevatorDeviceId
                   && x.PassRightSign == distributeHandle.CardNumber);
            if (passRightHandleElevatorDeviceCallType != null)
            {
                return (ushort)passRightHandleElevatorDeviceCallType.MessageType;
            }
            return null;
        }

        public async Task MultiFloorCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeMultiFloorHandleElevatorModel distributeHandle)
        {
            var messageType = KoneHelper.SystemConfig.OpenAccessForDopMessageType;
            var newMessageType = await GetEliMessageType(distributeHandle);
            if (newMessageType != null)
            {
                messageType = newMessageType.Value;
            }

            if (OpenAccessForDopMessageTypeEnum.NORMAL.Code == messageType)
            {
                await OpenAccessForDopMessage(elevatorGroupRemoteDevice, distributeHandle);
            }
            else if (OpenAccessForDopMessageTypeEnum.WITH_CALL_TYPE.Code == messageType)
            {
                await OpenAccessForDopWithCallTypeMessage(elevatorGroupRemoteDevice, distributeHandle);
            }
            else
            {
                _logger.LogError($"多楼层派梯找不到派梯配置类型,执行默认类型：openAccessForDopMessageType:{messageType} default:{OpenAccessForDopMessageTypeEnum.WITH_CALL_TYPE.Code} ");

                await OpenAccessForDopMessage(elevatorGroupRemoteDevice, distributeHandle);
            }
        }

        private async Task OpenAccessForDopMessage(IRemoteDevice elevatorGroupRemoteDevice, DistributeMultiFloorHandleElevatorModel distributeHandle)
        {
            //初始化发送数据
            var model = new KoneEliOpenAccessForDopMessage();
            model.DopId = Convert.ToByte(distributeHandle.RealDeviceId);
            model.DopFloorId = (ushort)distributeHandle.SourceFloor.Floor;
            model.UserId = Int128.Parse(distributeHandle.CardNumber);
            model.OpenEvents = new List<KoneEliOpenAccessForDopMessageOpenEventData>();
            foreach (var item in distributeHandle.DestinationFloors)
            {
                var openEvent = new KoneEliOpenAccessForDopMessageOpenEventData();
                openEvent.DestinationFloor = (ushort)item.Floor;
                openEvent.IsFront = item.IsFront;
                openEvent.IsRear = item.IsRear;
                model.OpenEvents.Add(openEvent);
            }

            //向派梯关系列表中增加数据，返回派梯结果时使用
            var sequence = new KoneEliSequenceModel();
            sequence.Id = IdUtil.NewId();
            sequence.Type = KoneEliSequenceTypeEnum.OpenAccessForDop;
            sequence.Data = model;
            sequence.DopId = distributeHandle.RealDeviceId;
            sequence.MessageId = distributeHandle.MessageId;
            sequence.HandleElevatorDeviceId = distributeHandle.HandleElevatorDeviceId;
            sequence.CardDeviceId = distributeHandle.CardDeviceId;
            _koneEliHandleElevatorSequenceList.Add(model.MessageId, sequence);

            _logger.LogInformation($"开始多楼层派梯：{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

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
                    foreach (var item in remoteDevice.CommunicateDevices)
                    {
                        var client = item.GetLoginUserClient<IKoneEliClientHost>();
                        Task.Run(async () =>
                        {
                            //var newModel = TransExp<KoneEliOpenAccessForDopMessage, KoneEliOpenAccessForDopMessage>.Trans(model);
                            var newModel = _mapper.Map<KoneEliOpenAccessForDopMessage>(model);
                            try
                            {
                                await client.SendAsync(newModel);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "kone发送数据错误！");
                            }
                        });
                    }
                });

            _logger.LogInformation($"结束多楼层派梯！ ");
        }
        private async Task OpenAccessForDopWithCallTypeMessage(IRemoteDevice elevatorGroupRemoteDevice, DistributeMultiFloorHandleElevatorModel distributeHandle)
        {
            //初始化发送数据
            var model = new KoneEliOpenAccessForDopWithCallTypeMessage();
            model.DopId = Convert.ToByte(distributeHandle.RealDeviceId);
            model.DopFloorId = (ushort)distributeHandle.SourceFloor.Floor;
            model.CallTypeDatas = new List<KoneEliOpenAccessForDopCallTypeData>();
            model.UserId = Int128.Parse(distributeHandle.CardNumber);
            // Call type attribute destination
            var attributeDestinationData = new KoneEliOpenAccessForDopCallTypeAttributeDestinationData();
            foreach (var item in distributeHandle.DestinationFloors)
            {
                var openEventData = new KoneEliOpenAccessForDopMessageOpenEventData();
                openEventData.DestinationFloor = (ushort)item.Floor;
                openEventData.IsFront = item.IsFront;
                openEventData.IsRear = item.IsRear;
                attributeDestinationData.OpenEvents.Add(openEventData);
            }

            var isLittleEndianess = model.IsLittleEndianess();
            // Set call type data
            var callTypeData = new KoneEliOpenAccessForDopCallTypeData();
            callTypeData.CallType = (byte)KoneHelper.SystemConfig.StandardCallType;
            callTypeData.NumberOfAttributes = 1;
            callTypeData.AttributeDatas = attributeDestinationData.GetBytes(isLittleEndianess).ToList();

            //设置calltype
            var destCallType = await GetEliCallType(distributeHandle);
            if (destCallType != null)
            {
                callTypeData.CallType = (byte)destCallType.Value;
            }

            model.CallTypeDatas.Add(callTypeData);

            //向派梯关系列表中增加数据，返回派梯结果时使用
            var sequence = new KoneEliSequenceModel();
            sequence.Id = IdUtil.NewId();
            sequence.Type = KoneEliSequenceTypeEnum.OpenAccessForDopWithCallType;
            sequence.Data = model;
            sequence.DopId = distributeHandle.RealDeviceId;
            sequence.MessageId = distributeHandle.MessageId;
            sequence.HandleElevatorDeviceId = distributeHandle.HandleElevatorDeviceId;
            sequence.CardDeviceId = distributeHandle.CardDeviceId;
            _koneEliHandleElevatorSequenceList.Add(model.MessageId, sequence);

            _logger.LogInformation($"通力开始多楼层派梯：{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

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
                    foreach (var item in remoteDevice.CommunicateDevices)
                    {
                        var client = item.GetLoginUserClient<IKoneEliClientHost>();
                        Task.Run(async () =>
                        {
                            //var newModel = TransExp<KoneEliOpenAccessForDopWithCallTypeMessage, KoneEliOpenAccessForDopWithCallTypeMessage>.Trans(model);
                            var newModel = _mapper.Map<KoneEliOpenAccessForDopWithCallTypeMessage>(model);
                            try
                            {
                                await client.SendAsync(newModel);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "kone发送数据错误！");
                            }
                        });
                    }
                });

            _logger.LogInformation($"通力结束多楼层派梯！ ");
        }

        public Task RightCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeHandleElevatorModel distributeHandle)
        {
            throw new NotImplementedException();
        }
    }
}
