using KT.Elevator.Unit.Dispatch.ClientApp.Device.Hitachi;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Events
{
    public class HandleElevatorReceiveEvent : PubSubEvent<HitachiReceiveModel>
    {
    }
}
