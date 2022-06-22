using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Events
{
    /// <summary>
    /// 异常事件
    /// </summary>
    public class ExceptionEvent : PubSubEvent<Exception>
    {
    }
}
