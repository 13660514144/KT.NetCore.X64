using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Entity.Entities
{
    [Table("PASS_RIGHT_ACCESSIBLE_FLOOR")]
    public class PassRightAccessibleFloorEntity : BaseEntity
    {
        public string Sign { get; set; }

        public string ElevatorGroupId { get; set; }

        public List<PassRightAccessibleFloorDetailEntity> PassRightAccessibleFloorDetails { get; set; }
    }
}
