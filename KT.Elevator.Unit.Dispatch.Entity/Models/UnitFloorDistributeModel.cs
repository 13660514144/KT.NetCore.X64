using KT.Elevator.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Entity.Models
{
    /// <summary>
    /// 楼层映射
    /// </summary>
    public class UnitFloorDistributeModel
    {
        /// <summary>
        /// 电梯组Id
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 电梯组可去楼层
        /// </summary>
        public List<UnitFloorEntity> Floors { get; set; }
    }
}
