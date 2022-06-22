using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Manage.Entity.Entities
{
    /// <summary>
    /// 楼层
    /// </summary>
    [Table("FLOOR")]
    public class FloorEntity : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 梯控楼层Id
        /// </summary>
        public string RealFloorId { get; set; }

        /// <summary>
        /// 所属大厦
        /// </summary>
        public EdificeEntity Edifice { get; set; }

    }
}
