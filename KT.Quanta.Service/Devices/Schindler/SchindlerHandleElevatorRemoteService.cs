using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Devices.Schindler.Clients;
using KT.Quanta.Service.Devices.Schindler.Helpers;
using KT.Quanta.Service.Devices.Schindler.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler
{
    public class SchindlerHandleElevatorRemoteService : IHandleElevatorRemoteService
    {
        private ILogger<SchindlerHandleElevatorRemoteService> _logger;
        private readonly ISchindlerHandleElevatorSequenceList _schindlerHandleElevatorSequenceList;
        private readonly RemoteDeviceList _remoteDeviceList;

        public SchindlerHandleElevatorRemoteService(ILogger<SchindlerHandleElevatorRemoteService> logger,
            ISchindlerHandleElevatorSequenceList schindlerHandleElevatorSequenceList,
            RemoteDeviceList remoteDeviceList)
        {
            _logger = logger;
            _schindlerHandleElevatorSequenceList = schindlerHandleElevatorSequenceList;
            _remoteDeviceList = remoteDeviceList;
        }

        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="elevatorGroupRemoteDevice">电梯组</param>
        /// <param name="distributeHandle">派梯参数</param> 
        public async Task DirectCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeHandleElevatorModel distributeHandle)
        {
            byte[] bytes;
            if (distributeHandle.IsCallByPerson)
            {
                var model = new SchindlerDispatchCallByPersonRequest();
                model.TerminalId = ushort.Parse(distributeHandle.RealDeviceId);
                model.PersonId = distributeHandle.PersonId;

                _logger.LogInformation($"开始派梯：{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

                var buffer = model.WriteToLocalBuffer();
                var value = model.GetWriteValue();
                _logger.LogInformation($"迅达电梯发送数据：{value}");

                bytes = model.GetBytes(value);

                //等结果返回再显示
                var handleResult = new SchindlerHandleElevatorSequenceModel();
                handleResult.MessageId = distributeHandle.MessageId;
                _schindlerHandleElevatorSequenceList.Add(model.MessageId, handleResult);
            }
            else
            {
                var model = new SchindlerDispatchDirectCallRequest();
                model.TerminalId = ushort.Parse(distributeHandle.RealDeviceId);
                model.DestinationFloor = (byte)distributeHandle.DestinationFloor.Floor;
                model.PersonId = distributeHandle.PersonId;

                _logger.LogInformation($"开始派梯：{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

                var buffer = model.WriteToLocalBuffer();
                var value = model.GetWriteValue();
                _logger.LogInformation($"迅达电梯发送数据：{value}");

                bytes = model.GetBytes(value);

                //等结果返回再显示
                var handleResult = new SchindlerHandleElevatorSequenceModel();
                handleResult.MessageId = distributeHandle.MessageId;
                _schindlerHandleElevatorSequenceList.Add(model.MessageId, handleResult);
            }

            await _remoteDeviceList.ExecuteAsync(
                (remoteDevice) =>
                {
                    if (remoteDevice.RemoteDeviceInfo.ParentId == elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceId)
                    {
                        if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.SCHINDLER_PORT.Value)
                        {
                            if (remoteDevice.RemoteDeviceInfo.ModuleType == SchindlerModuleTypeEnum.DISPATCH.Value)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                },
                async (remoteDevice) =>
                {
                    if (remoteDevice?.CommunicateDevices?.FirstOrDefault() == null)
                    {
                        _logger.LogError($"连接设备不能为空：device:{JsonConvert.SerializeObject(remoteDevice.RemoteDeviceInfo, JsonUtil.JsonPrintSettings)} ");
                        return;
                    }
                    foreach (var item in remoteDevice.CommunicateDevices)
                    {
                        var client = item.GetLoginUserClient<ISchindlerDispatchClientHost>();
                        await client.SendAsync(bytes);
                    }
                });

            _logger.LogInformation($"结束派梯：{bytes.ToCommaPrintString()} ");
        }

        public Task MultiFloorCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeMultiFloorHandleElevatorModel distributeHandle)
        {
            throw new NotImplementedException();
        }

        public Task RightCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeHandleElevatorModel distributeHandle)
        {
            throw new NotImplementedException();
        }
    }
}
