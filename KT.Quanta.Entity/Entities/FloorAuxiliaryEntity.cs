using KT.Common.Data.Models;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Entity.Entities
{
    [Table("FLOOR_AUXILIARY")]
    public class FloorAuxiliaryEntity : BaseEntity
    {
        /// <summary>
        /// 楼层Id
        /// </summary>
        public string FloorId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
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
