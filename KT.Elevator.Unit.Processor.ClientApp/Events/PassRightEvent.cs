using KT.Elevator.Unit.Entity.Entities;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Processor.ClientApp.Events
{
    /// <summary>
    /// 修改权限
    /// </summary>
    public class AddOrEditPassRightsEvent : PubSubEvent<List<UnitPassRightEntity>>
    {
    }

    /// <summary>
    /// 修改权限
    /// </summary>
    public class DeletePassRightEvent : PubSubEvent<string>
    {
    }
}
