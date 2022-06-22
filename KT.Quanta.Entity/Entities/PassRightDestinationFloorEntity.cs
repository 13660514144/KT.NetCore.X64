using KT.Common.Data.Models;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Entity.Entities
{
    [Table("PASS_RIGHT_DESTINATION_FLOOR_FLOOR")]
    public class PassRightDestinationFloorEntity : BaseEntity
    {
        public string Sign { get; set; }

        public string ElevatorGroupId { get; set; }

        public string FloorId { get; set; }

        public FloorEntity Floor { get; set; }

        /// <summary>
        /// 是否开启前门
        /// </summary>
        public bool IsFront { get; set; }

        /// <summary>
        /// 是否开启前门
        /// </summary>
        public bool IsRear { get; set; }
    }
}
