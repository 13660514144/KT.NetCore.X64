using KT.Quanta.Service.Devices.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Hikvision.Models
{
    public class HikvisionDeviceExecuteQueueModel
    {
        public string DistributeType { get; set; }

        public IRemoteDevice RemoteDevice { get; set; }

        public object Data { get; set; }
    }
}
