using KT.Visitor.IdReader.Common;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Events
{
    /// <summary>
    /// 阅读器阅读人员
    /// </summary>
    public class ReadedPersonEvent : PubSubEvent<Person>
    {

    }
}
