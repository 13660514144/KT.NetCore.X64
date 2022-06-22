using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using KT.Turnstile.Common.Enums;

namespace KT.Turnstile.Entity.Entities
{
    /// <summary>
    /// 系统配置信息
    /// 配合SystemConfigEnum使使用
    /// </summary>
    [Table("SYSTEM_CONFIG")]
    public class SystemConfigEntity : BaseEntity
    {
        /// <summary>
        /// 关键字
        /// <see cref="SystemConfigEnum"/>
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
