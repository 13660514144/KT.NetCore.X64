using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Dispatch.Entity.Models
{
    /// <summary>
    /// 调用派梯返回数据
    /// </summary>
    public class UnitDispatchReceiveHandleElevatorModel
    {
        /// <summary>
        /// 每次发送携带的主键id
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public uint CardNumber { get; set; }

        /// <summary>
        /// 目的楼层
        /// </summary>
        public string DistinationFloorId { get; set; }

        /// <summary>
        /// 电梯
        /// </summary>
        public string ElevatorName { get; set; }
    }
}
