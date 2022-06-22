using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.QuantaApi.Models
{
    public class ElevatorGroupModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EdificeId { get; set; }
        public List<FloorModel> Floors { get; set; }
    }
}
