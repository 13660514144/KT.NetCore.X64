using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Manage.Service.Entities
{
    /// <summary>
    /// 通行权限关联楼层
    /// </summary>
    [Table("PASS_RIGHT_RELATION_FLOOR")]
    public class PassRightRelationFloorEntity : BaseEntity
    {
        /// <summary>
        /// 通行权限
        /// </summary>
        public PassRightEntity PassRight { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public FloorEntity Floor { get; set; }
    }
}
