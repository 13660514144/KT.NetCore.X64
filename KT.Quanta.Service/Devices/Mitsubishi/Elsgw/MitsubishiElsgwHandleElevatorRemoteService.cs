using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Clients;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw
{
    public class MitsubishiElsgwHandleElevatorRemoteService : IHandleElevatorRemoteService
    {
        private ILogger<MitsubishiElsgwHandleElevatorRemoteService> _logger;
        private readonly IMitsubishiElipHandleElevatorSequenceList _mitsubishiElipHandleElevatorSequenceList;
        private readonly RemoteDeviceList _remoteDeviceList;

        public MitsubishiElsgwHandleElevatorRemoteService(ILogger<MitsubishiElsgwHandleElevatorRemoteService> logger,
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
        public async Task DirectCallAsync(IRemoteDevice elevatorGroupRemoteDevice,
            DistributeHandleElevatorModel distributeHandle)
        {
            var model = new MitsubishiElsgwSingleFloorCallDataModel();
            //根据派梯设备确定设备赋值
            model.DeviceNumber = ConvertUtil.ToUInt16(distributeHandle.RealDeviceId, 0);
            model.BoardingFloor = (byte)distributeHandle.SourceFloor.Floor;
            model.DestinationFloor = (byte)distributeHandle.DestinationFloor.Floor;

            var header = new MitsubishiElsgwTransmissionHeader();
            //不同派梯类型不同值
            header.Assistant.CommandNumber = (byte)MitsubishiElsgwCommandEnum.SingleFloorCall.Code;
            header.Assistant.Datas = model.GetBytes().ToList();

            //向派梯关系列表中增加数据，返回派梯结果时使用
            var sequence = new MitsubishiElipHandleElevatorSequenceModel();
            sequence.FloorName = distributeHandle.DestinationFloorName;
            sequence.HandleElevatorDeviceId = distributeHandle.DeviceId;
            sequence.MessageId = distributeHandle.MessageId;
            sequence.SourceSequenceNumber = model.SequenceNumber;
            _mitsubishiElipHandleElevatorSequenceList.Add(model.SequenceNumber, sequence);            
            _logger.LogInformation($" 开始三菱派梯header={JsonConvert.SerializeObject(header)}");
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
                   // _logger.LogInformation($"remoteDevice={JsonConvert.SerializeObject(remoteDevice)}");
                   // _logger.LogInformation($"remoteDevice.CommunicateDevices={JsonConvert.SerializeObject(remoteDevice.CommunicateDevices)}");
                    foreach (var item in remoteDevice.CommunicateDevices)
                    {
                        var client = item.GetLoginUserClient<IMitsubishiElsgwClientHost>();
                        await client.SendAsync(header);
                    }
                });

            _logger.LogInformation($"结束三菱派梯：{header.Assistant.Datas.ToCommaPrintString()} ");
        }

        public async Task MultiFloorCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeMultiFloorHandleElevatorModel distributeHandle)
        {
            var model = new MitsubishiElsgwMultiFloorCallDataModel();
            //根据派梯设备确定设备赋值
            model.DeviceNumber = ConvertUtil.ToUInt16(distributeHandle.RealDeviceId, 0);
            model.BoardingFloor = (byte)distributeHandle.SourceFloor.Floor;
            //model.DestinationFloor = (byte)distributeHandle.DestinationFloor;
            model.FrontDestinationFloors = distributeHandle.DestinationFloors.Select(x => (byte)x.Floor).ToList();
            model.RearDestinationFloors = model.FrontDestinationFloors;

            var header = new MitsubishiElsgwTransmissionHeader();
            //不同派梯类型不同值
            header.Assistant.CommandNumber = (byte)MitsubishiElsgwCommandEnum.MultiFloorCall.Code;
            header.Assistant.Datas = model.GetBytes().ToList();

            //向派梯关系列表中增加数据，返回派梯结果时使用
            var sequence = new MitsubishiElipHandleElevatorSequenceModel();
            sequence.HandleElevatorDeviceId = distributeHandle.HandleElevatorDeviceId;
            sequence.MessageId = distributeHandle.MessageId;
            sequence.SourceSequenceNumber = model.SequenceNumber;
            _mitsubishiElipHandleElevatorSequenceList.Add(model.SequenceNumber, sequence);

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
                        var client = item.GetLoginUserClient<IMitsubishiElsgwClientHost>();
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
