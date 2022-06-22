using KT.Quanta.Service.Devices.Schindler;
using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    public abstract class RemoteDevice : IRemoteDevice
    {
        public RemoteDeviceModel RemoteDeviceInfo { get; set; }
        public List<ICommunicateDevice> CommunicateDevices { get; set; }
        public IElevatorDataRemoteService ElevatorDataRemoteService { get; set; }
        public ITurnstileDataRemoteService TurnstileDataRemoteService { get; set; }
        public IHandleElevatorRemoteService HandleElevatorRemoteService { get; set; }
        public IElevatorDisplayRemoteService ElevatorDisplayRemoteService { get; set; }
        public IElevatorSelectorRemoteService ElevatorSelectorRemoteService { get; set; }
        public ITurnstileDisplayRemoteService TurnstileDisplayRemoteService { get; set; }
        public ITurnstileOperateRemoteService TurnstileOperateRemoteService { get; set; }
        public SchindlerElevatorRecordRemoteService ElevatorRecordRemoteService { get; set; }

        public abstract Task InitAsync(RemoteDeviceModel remoteDeviceInfo);
    }
}
