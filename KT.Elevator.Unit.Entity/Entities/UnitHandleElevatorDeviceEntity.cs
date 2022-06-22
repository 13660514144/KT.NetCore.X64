using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Unit.Entity.Entities
{
    [Table("HANDLE_ELEVATOR_DEVICE")]
    public class UnitHandleElevatorDeviceEntity : BaseEntity
    {
        public string ElevatorGroupId { get; set; }

        //public UnitElevatorGroupEntity ElevatorGroup { get; set; }
    }
}
