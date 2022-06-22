using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Manage.Entity.Entities
{
    /// <summary>
    /// 电梯组关联楼层
    /// </summary>
    [Table("ELEVATOR_GROUP_RELATION_FLOOR")]
    public class ElevatorGroupRelationFloorEntity : BaseEntity
    {
        /// <summary>
        /// 电梯组
        /// </summary>
        public ElevatorGroupEntity ElevatorGroup { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public FloorEntity Floor { get; set; }
    }
}
