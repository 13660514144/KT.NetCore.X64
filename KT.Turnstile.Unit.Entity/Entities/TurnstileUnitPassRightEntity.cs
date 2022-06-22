using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Turnstile.Unit.Entity.Entities
{
    /// <summary>
    /// 通行权限
    /// 通行权限关联通道，通道关联读卡器（多个，分进出）、边缘处理器
    /// </summary>
    [Table("PASS_RIGHT")]
    public class TurnstileUnitPassRightEntity : BaseEntity
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 起始时间，UTC毫秒
        /// </summary>
        public long TimeStart { get; set; }

        /// <summary>
        /// 过期时间，UTC毫秒
        /// </summary>
        public long TimeEnd { get; set; }

        /// <summary>
        /// 权限组Id
        /// </summary>
        public List<TurnstileUnitPassRightDetailEntity> Details { get; set; }

        public TurnstileUnitPassRightEntity()
        {
            Details = new List<TurnstileUnitPassRightDetailEntity>();
        }
    }
}
