using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.QuantaApi.Models
{
    public class HandleElevatorDeviceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public FloorModel Floor { get; set; }
    }
}
