using KT.Elevator.Unit.Dispatch.Entity.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Events
{
    /// <summary>
    /// 派梯发送成功，未返回结果
    /// </summary>
    public class HandledElevatorEvent : PubSubEvent<UnitDispatchHandleElevatorModel>
    {
    }
}
