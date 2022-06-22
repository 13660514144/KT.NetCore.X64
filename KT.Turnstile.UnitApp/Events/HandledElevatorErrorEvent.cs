﻿using KT.Quanta.Common.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Unit.ClientApp.Events
{
    /// <summary>
    /// 派梯结束
    /// </summary>
    public class HandledElevatorErrorEvent : PubSubEvent<HandleElevatorErrorModel>
    {
    }
}
