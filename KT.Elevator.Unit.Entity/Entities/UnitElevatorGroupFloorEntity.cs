using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Unit.Entity.Entities
{
    [Table("ELEVATOR_GROUP_FLOOR")]
    public class UnitElevatorGroupFloorEntity : BaseEntity
    {
        public string ElevatorGroupId { get; set; }
        public UnitElevatorGroupEntity ElevatorGroup { get; set; }

        public string FloorId { get; set; }

        //public UnitFloorEntity Floor { get; set; }
    }
}
