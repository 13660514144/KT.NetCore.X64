using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Unit.Secodary.Entity.Entities
{
    /// <summary>
    /// 通行权限详情，权限关联可去楼层
    /// </summary>
    [Table("PASS_RIGHT_DETAIL")]
    public class UnitPassRightDetailEntity : BaseEntity
    {
        /// <summary>
        /// 通行权限Id
        /// </summary>
        public string PassRightId { get; set; }

        /// <summary>
        /// 通行权限
        /// </summary>
        public UnitPassRightEntity PassRight { get; set; }

        /// <summary>
        /// 楼层Id，可能重复，不能作为主键
        /// </summary>
        public string FloorId { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 真实楼层Id
        /// </summary>
        public string RealFloorId { get; set; }

        /// <summary>
        /// 是否公共楼层
        /// </summary>
        public bool IsPublic { get; set; }
    }
}
