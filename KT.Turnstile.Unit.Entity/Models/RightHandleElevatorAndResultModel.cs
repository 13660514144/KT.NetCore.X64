using KT.Elevator.Unit.Entity.Models;
using KT.Quanta.Common.Models;

namespace KT.Turnstile.Unit.Entity.Models
{
    /// <summary>
    /// 派梯参数与结果
    /// </summary>
    public class RightHandleElevatorAndResultModel
    {
        /// <summary>
        /// 派梯参数
        /// </summary>
        public UintRightHandleElevatorRequestModel RightHandleElevator { get; set; }

        /// <summary>
        /// 派梯结果
        /// </summary>
        public HandleElevatorDisplayModel HandledElevatorSuccess { get; set; }
         
        /// <summary>
        /// 通行时间
        /// </summary>
        public long PassTime { get; set; }
    }
}
