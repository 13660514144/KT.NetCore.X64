﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Processor.ClientApp.Events
{
    /// <summary>
    /// 弹窗提示
    /// </summary>
    public class WarnTipEvent : PubSubEvent<string>
    {
    }
}
