using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Quanta.Service.Entities
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
        public string PassRightId { get; set; }

        /// <summary>
        /// 通行权限
        /// </summary>
        public PassRightEntity PassRight { get; set; }

        /// <summary>
        /// 楼层
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
