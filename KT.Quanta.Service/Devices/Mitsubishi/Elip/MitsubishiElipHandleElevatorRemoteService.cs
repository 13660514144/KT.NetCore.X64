using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip
{
    public class MitsubishiElipHandleElevatorRemoteService : IHandleElevatorRemoteService
    {
        private ILogger<MitsubishiElipHandleElevatorRemoteService> _logger;
        private readonly IMitsubishiElipHandleElevatorSequenceList _mitsubishiElipHandleElevatorSequenceList;
        private readonly RemoteDeviceList _remoteDeviceList;

        public MitsubishiElipHandleElevatorRemoteService(ILogger<MitsubishiElipHandleElevatorRemoteService> logger,
             IMitsubishiElipHandleElevatorSequenceList mitsubishiElipHandleElevatorSequenceList,
             RemoteDeviceList remoteDeviceList)
        {
            _logger = logger;
            _mitsubishiElipHandleElevatorSequenceList = mitsubishiElipHandleElevatorSequenceList;
            _remoteDeviceList = remoteDeviceList;
        }

        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="elevatorGroupRemoteDevice">电梯组</param>
        /// <param name="distributeHandle">派梯参数</param> 
        public async Task DirectCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeHandleElevatorModel distributeHandle)
        {
            //派梯参数
            var model = new MitsubishiElipAutomaticAccessRequest();
            model.SwipedCardReaderNumber = ConvertUtil.ToUInt16(distributeHandle.RealDeviceId, 0);
            model.DestinationFloor = (byte)distributeHandle.DestinationFloor.Floor;

            //向派梯关系列表中增加数据，返回派梯结果时使用
            var sequence = new MitsubishiElipHandleElevatorSequenceModel();
            sequence.MessageId = distributeHandle.MessageId;
            sequence.HandleElevatorDeviceId = distributeHandle.DeviceId;
            sequence.SourceSequenceNumber = Convert.ToByte(distributeHandle.SequenceId);
            sequence.FloorName = distributeHandle.DestinationFloorName;
            sequence.HandleElevatorType = HandleElevatorTypeEnum.MitsubishiElip.Value;
            sequence.HandleElevatorMode = HandleElevatorModeEnum.SingleFloor.Value;
            _mitsubishiElipHandleElevatorSequenceList.Add(model.SequenceNumber, sequence);

            //封装派梯参数
            var header = new MitsubishiElipCommunicationHeader();
            header.Assistant.Command = (byte)MitsubishiElipCommandEnum.AutomaticAccess.Code;
            header.Assistant.Datas = model.GetBytes().ToList();

            _logger.LogInformation($"开始派梯：header:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

            await _remoteDeviceList.ExecuteAsync(
                (remoteDevice) =>
                {
                    if (remoteDevice.RemoteDeviceInfo.ParentId == elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceId)
                    {
                        if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELSGW.Value
                            || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELIP.Value)
                        {
                            return true;
                        }
                    }
                    return false;
                },
                async (remoteDevice) =>
                {
                    foreach (var item in remoteDevice.CommunicateDevices)
                    {
                        var client = item.GetLoginUserClient<IMitsubishiElipClientHost>();
                        await client.SendAsync(header);
                    }
                });

            _logger.LogInformation($"结束派梯：{header.Assistant.Datas.ToCommaPrintString()} ");
        }

        public async Task MultiFloorCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeMultiFloorHandleElevatorModel distributeHandle)
        {
            //派梯参数
            var model = new MitsubishiElipManualAccessRequest();
            model.SwipedCardReaderNumber = ConvertUtil.ToUInt16(distributeHandle.RealDeviceId, 0);
            model.AccesibleFloors = distributeHandle.DestinationFloors.Select(x => (byte)x.Floor).ToList();

            //向派梯关系列表中增加数据，返回派梯结果时使用
            var sequence = new MitsubishiElipHandleElevatorSequenceModel();
            sequence.MessageId = distributeHandle.MessageId;
            sequence.HandleElevatorDeviceId = distributeHandle.HandleElevatorDeviceId;
            sequence.SourceSequenceNumber = Convert.ToByte(distributeHandle.SequenceId);
            sequence.HandleElevatorType = HandleElevatorTypeEnum.MitsubishiElip.Value;
            sequence.HandleElevatorMode = HandleElevatorModeEnum.MultiFloor.Value;
            _mitsubishiElipHandleElevatorSequenceList.Add(model.SequenceNumber, sequence);

            //封装派梯参数
            var header = new MitsubishiElipCommunicationHeader();
            header.Assistant.Command = (byte)MitsubishiElipCommandEnum.ManualAccess.Code;
            header.Assistant.Datas = model.GetBytes().ToList();

            _logger.LogInformation($"开始派梯：header:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

            await _remoteDeviceList.ExecuteAsync(
                (remoteDevice) =>
                {
                    if (remoteDevice.RemoteDeviceInfo.ParentId == elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceId)
                    {
                        if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELSGW.Value
                            || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELIP.Value)
                        {
                            return true;
                        }
                    }
                    return false;
                },
                async (remoteDevice) =>
                {
                    foreach (var item in remoteDevice.CommunicateDevices)
                    {
                        var client = item.GetLoginUserClient<IMitsubishiElipClientHost>();
                        await client.SendAsync(header);
                    }
                });

            _logger.LogInformation($"结束派梯：{header.Assistant.Datas.ToCommaPrintString()} ");
        }

        public Task RightCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeHandleElevatorModel distributeHandle)
        {
            throw new System.NotImplementedException();
        }
    }
}
