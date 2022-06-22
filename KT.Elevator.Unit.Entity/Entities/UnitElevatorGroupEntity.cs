using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Unit.Entity.Entities
{
    [Table("ELEVATOR_GROUP")]
    public class UnitElevatorGroupEntity : BaseEntity
    {
        public List<UnitElevatorGroupFloorEntity> ElevatorGroupFloors { get; set; }
    }
}
