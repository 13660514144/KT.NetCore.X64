using KT.Quanta.Service.Devices.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    public class SchindlerDatabaseQueueModel
    {
        public string DistributeType { get; set; }

        public IRemoteDevice RemoteDevice { get; set; }

        public byte[] Datas { get; set; }
    }
}
