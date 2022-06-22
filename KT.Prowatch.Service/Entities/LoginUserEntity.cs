using KT.Common.Data.Models;
using KT.Prowatch.Service.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Prowatch.Service.Entities
{
    /// <summary>
    /// 登录用户实体模型
    /// </summary>
    [Table("LOGIN_USER")]
    public class LoginUserEntity : BaseEntity
    {
        /// <summary>
        /// 数据连接地址
        /// </summary>
        public string DBAddr { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// 数据库用户
        /// </summary>
        public string DBUser { get; set; }

        /// <summary>
        /// 数据库密码
        /// </summary>
        public string DBPassword { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string PCAddr { get; set; }

        /// <summary>
        /// 服务器用户名
        /// </summary>
        public string PCUser { get; set; }

        /// <summary>
        /// 服务器密码
        /// </summary>
        public string PCPassword { get; set; }

        /// <summary>
        /// 后台服务器地址
        /// </summary>
        public string ServerAddress { get; set; }

        public LoginUserEntity()
        {
        }
    }
}
