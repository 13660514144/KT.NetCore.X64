using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Prowatch.Service.Entities
{
    /// <summary>
    /// 用户录token数据原型
    /// </summary>
    [Table("USER_TOKEN")]
    public class UserTokenEntity : BaseEntity
    {
        /// <summary>
        /// Token值
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public long TimeNow { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public long TimeOut { get; set; }

        /// <summary>
        /// 关联用户
        /// </summary>
        public LoginUserEntity LoginUser { get; set; }
    }
}
