using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw
{
    /// <summary>
    /// 派梯设备
    /// </summary>
    public class MitsubishiElsgwElevatorServerRemoteDevice : RemoteDevice, IRemoteDevice
    {
        private CommunicateDeviceList _communicateDeviceList;
        public MitsubishiElsgwElevatorServerRemoteDevice(CommunicateDeviceList communicateDeviceList)
        {
            _communicateDeviceList = communicateDeviceList;
        }

        public override async Task InitAsync(RemoteDeviceModel remoteDeviceInfo)
        {
            RemoteDeviceInfo = remoteDeviceInfo;

            CommunicateDevices = new List<ICommunicateDevice>();
            for (int i = 0; i < RemoteDeviceInfo.CommunicateDeviceInfos.Count; i++)
            {
                var communicateDevice = await _communicateDeviceList.AddOrEditByAddressAndUserAsync(RemoteDeviceInfo.CommunicateDeviceInfos[i], remoteDeviceInfo);
                CommunicateDevices.Add(communicateDevice);

                RemoteDeviceInfo.CommunicateDeviceInfos[i] = communicateDevice.CommunicateDeviceInfo;
            }
        }
    }
}
