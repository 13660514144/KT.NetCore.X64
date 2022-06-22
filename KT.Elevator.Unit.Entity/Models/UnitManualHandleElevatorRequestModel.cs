using System.Collections.Generic;

namespace KT.Elevator.Unit.Entity.Models
{
    /// <summary>
    /// 派梯参数
    /// </summary>
    public class UnitManualHandleElevatorRequestModel : UintRightHandleElevatorRequestModel
    {
        ///// <summary>
        ///// 来源楼层
        ///// </summary>
        public string SourceFloorId { get; set; }

        /// <summary>
        /// 派梯楼层
        /// </summary>
        public List<string> DestinationFloorIds { get; set; }
        public string ElevatorGroupId { get; set; }
    }
}
