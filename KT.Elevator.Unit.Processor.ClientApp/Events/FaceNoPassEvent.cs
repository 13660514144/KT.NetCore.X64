using KT.Elevator.Unit.Entity.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Events
{
    /// <summary>
    /// 人脸授权成功
    /// </summary>
    public class FaceNoPassEvent : PubSubEvent <UnitHandleElevatorModel>
    {

    }
}
