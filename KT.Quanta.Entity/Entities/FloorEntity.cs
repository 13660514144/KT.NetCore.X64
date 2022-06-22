using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Quanta.Service.Entities
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
        /// 是否开启前门
        /// </summary>
        public bool IsFront { get; set; }

        /// <summary>
        /// 是否开启前门
        /// </summary>
        public bool IsRear { get; set; }

        /// <summary>
        /// 物理楼层
        /// </summary>
        public string PhysicsFloor { get; set; }

        /// <summary>
        /// 是否为公共楼层
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 所属大厦
        /// </summary>
        public virtual string EdificeId { get; set; }

        /// <summary>
        /// 所属大厦
        /// </summary>
        public virtual EdificeEntity Edifice { get; set; }

    }
}
