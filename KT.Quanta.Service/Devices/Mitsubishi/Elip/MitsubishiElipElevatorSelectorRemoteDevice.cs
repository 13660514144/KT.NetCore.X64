using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip
{
    public class MitsubishiElipElevatorSelectorRemoteDevice : RemoteDevice, IRemoteDevice
    {
        public override Task InitAsync(RemoteDeviceModel remoteDeviceInfo)
        {
            base.RemoteDeviceInfo = remoteDeviceInfo;

            return Task.CompletedTask;
        }
    }
}
