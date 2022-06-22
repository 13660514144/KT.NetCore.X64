using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.QuantaApi.Models
{
    public class FloorModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? RealFloorId { get; set; }
        public string Direction { get; set; }
    }
}
