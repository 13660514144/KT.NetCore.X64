using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Secodary.Entity.Models
{
    /// <summary>
    /// 派梯参数
    /// </summary>
    public class UnitManualHandleElevatorRealRequestModel
    {
        /// <summary>
        /// 卡号，迅达电梯使用
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 来源楼层
        /// </summary>
        public string SourceRealFloorId { get; set; }

        /// <summary>
        /// 派梯楼层
        /// </summary>
        public List<string> DestinationRealFloorIds { get; set; }

        /// <summary>
        /// 派梯设备
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }
    }
}
