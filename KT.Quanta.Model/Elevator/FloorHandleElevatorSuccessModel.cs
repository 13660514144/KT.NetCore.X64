using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Elevator.Dtos
{
    /// <summary>
    /// 派梯返回结果
    /// </summary>
    public class FloorHandleElevatorSuccessModel
    {
        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 电梯名称
        /// </summary>
        public string ElevatorName { get; set; }
    }
}
