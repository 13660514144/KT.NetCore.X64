using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Elevator.Manage.Entity.Entities
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Table("LOGIN_USER")]
    public class LoginUserEntity : BaseEntity
    {
        /// <summary>
        /// 用户Id,关联WinPak系统中的用户
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 服务器数据库地址
        /// </summary>
        public string DBAddr { get; set; }

        /// <summary>
        /// 服务器数据库名称
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// 服务器数据库用户名
        /// </summary>
        public string DBUser { get; set; }

        /// <summary>
        /// 服务器数据库密码
        /// </summary>
        public string DBPassword { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string PCAddr { get; set; }

        /// <summary>
        /// 服务器系统用户名
        /// </summary>
        public string PCUser { get; set; }

        /// <summary>
        /// 服务器系统密码
        /// </summary>
        public string PCPassword { get; set; }

        /// <summary>
        /// 后台服务器地址
        /// </summary>
        public string ServerAddress { get; set; }
    }
}
