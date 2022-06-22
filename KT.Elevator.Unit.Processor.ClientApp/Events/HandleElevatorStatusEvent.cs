﻿using KT.Quanta.Common.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Processor.ClientApp.Events
{
    /// <summary>
    /// 派梯结束
    /// </summary>
    public class HandleElevatorStatusEvent : PubSubEvent<HandleElevatorDisplayModel>
    {
    }
}