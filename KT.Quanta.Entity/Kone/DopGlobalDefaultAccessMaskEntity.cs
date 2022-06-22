using KT.Common.Data.Models;
using KT.Quanta.Service.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Quanta.Entity.Kone
{
    [Table("DOP_GLOBAL_DEFAULT_ACCESS_MASK")]
    public class DopGlobalDefaultAccessMaskEntity : BaseEntity
    {
        /// <summary>
        /// 0:断开连接
        /// 1:连接
        /// </summary>
        public int ConnectedState { get; set; } = -1;

        /// <summary>
        /// 电梯组id
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 电梯组
        /// </summary>
        public ElevatorGroupEntity ElevatorGroup { get; set; }

        /// <summary>
        /// Dop Specific Mask可去楼层
        /// </summary>
        public List<DopGlobalDefaultAccessFloorMaskEntity> MaskFloors { get; set; }
    }
}
