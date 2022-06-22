using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Secodary.Entity.Models
{
    /// <summary>
    /// 派梯参数
    /// </summary>
    public class UnitManualHandleElevatorRequestModel
    {
        /// <summary>
        /// 来源楼层
        /// </summary>
        public string SourceFloorId { get; set; }

        /// <summary>
        /// 派梯楼层
        /// </summary>
        public List<string> DestinationFloorIds { get; set; }

        /// <summary>
        /// 派梯设备
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }
    }
}
