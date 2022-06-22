using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler
{
    public class SchindlerElevatorServerRemoteDevice : RemoteDevice, IRemoteDevice
    {
        private CommunicateDeviceList _communicateDeviceList;
        public SchindlerElevatorServerRemoteDevice(CommunicateDeviceList communicateDeviceList)
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
