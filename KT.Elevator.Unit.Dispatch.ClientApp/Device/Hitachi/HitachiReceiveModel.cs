using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Device.Hitachi
{
    public class HitachiReceiveModel
    {
        public string PortName { get; set; }
        public byte Command { get; set; }
        public byte DistinationFloorId { get; set; }
        public string ElevatorName { get; set; }
    }
}
