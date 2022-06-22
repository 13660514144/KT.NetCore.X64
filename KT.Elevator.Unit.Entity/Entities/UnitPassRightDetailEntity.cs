using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Elevator.Unit.Entity.Entities
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
        public string RealFloorId { get; set; }

        /// <summary>
        /// 是否公共楼层
        /// </summary>
        public bool IsPublic { get; set; }
    }
}
