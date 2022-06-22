using KT.Common.Data.Models;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Entity.Entities
{
    [Table("PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL")]
    public class PassRightAccessibleFloorDetailEntity : BaseEntity
    {
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

        public string PassRightAccessibleFloorId { get; set; }

        public PassRightAccessibleFloorEntity PassRightAccessibleFloor { get; set; }
    }
}
