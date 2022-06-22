using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using KT.Common.Data.Models;
using KT.Turnstile.Common.Enums;

namespace KT.Turnstile.Entity.Entities
{
    /// <summary>
    /// 通行权限
    /// 通行权限关联通道，通道关联读卡器（多个，分进出）、边缘处理器
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
        /// 关联读卡器权限组
        /// </summary> 
        public ICollection<PassRightRelationCardDeviceRightGroupEntity> RelationCardDeviceRightGroups { get; set; }
    }
}
