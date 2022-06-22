using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Manage.Entity.Entities
{
    /// <summary>
    /// 通行权限
    /// </summary>
    [Table("PASS_RIGHT")]
    public class PassRightEntity : BaseEntity
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 当前时间，UTC毫秒
        /// </summary>
        public long TimeNow { get; set; }

        /// <summary>
        /// 过期时间，UTC毫秒
        /// </summary>
        public long TimeOut { get; set; }

        /// <summary>
        /// 目标楼层
        /// </summary>
        public FloorEntity Floor { get; set; }

        /// <summary>
        /// 关联读卡器权限组
        /// </summary> 
        public ICollection<PassRightRelationFloorEntity> RelationFloors { get; set; }
    }
}
