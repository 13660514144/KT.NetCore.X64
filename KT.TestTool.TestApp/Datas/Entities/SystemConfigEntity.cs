using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.TestTool.TestApp.Entities
{
    /// <summary>
    /// 系统配置信息
    /// 配合SystemConfigEnum使使用
    /// </summary>
    [Table("SystemConfig")]
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
