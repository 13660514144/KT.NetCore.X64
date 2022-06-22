using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Dispatch.Entity.Models
{
    /// <summary>
    /// 用于记录最后一次派梯信息
    /// </summary>
    public class UnitDispatchHandleElevatorModel
    {
        /// <summary>
        /// 通行时间
        /// </summary>
        public long PassTime { get; set; }

        /// <summary>
        /// 派梯数据
        /// </summary>
        public UnitDispatchSendHandleElevatorModel SendData { get; set; }

    }
}
