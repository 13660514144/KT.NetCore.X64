using KT.Elevator.Unit.Entity.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Unit.ClientApp.Events
{
    public class HandledElevatorEvent: PubSubEvent<UnitHandleElevatorModel>
    {
    }
}
