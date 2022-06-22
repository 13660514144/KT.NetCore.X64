using KT.Common.Core.Utils;
using KT.Common.Data.Entities;
using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Quanta.Service.Entities
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Table("LOGIN_USER")]
    public class LoginUserEntity : ServerEntity
    {
        /// <summary>
        /// 后台服务器地址
        /// </summary>
        public string ServerAddress { get; set; }
    }
}
