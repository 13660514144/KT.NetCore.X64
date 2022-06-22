using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Quanta.Service.Entities
{
    /// <summary>
    /// 电梯组关联楼层
    /// </summary>
    [Table("ELEVATOR_GROUP_FLOOR")]
    public class ElevatorGroupFloorEntity : BaseEntity
    {
        /// <summary>
        /// 电梯组
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 电梯组
        /// </summary>
        public ElevatorGroupEntity ElevatorGroup { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string FloorId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public FloorEntity Floor { get; set; }

        /// <summary>
        /// 梯控楼层Id
        /// </summary>
        public string RealFloorId { get; set; }
    }
}
