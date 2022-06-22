using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using KT.Common.WebApi.HttpModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.WinPak.Data.Models
{
    /// <summary>
    /// 用户登录token
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
        /// 当前时间
        /// </summary>
        public long TimeOut { get; set; }
 
        /// <summary>
        /// 数据库连接数据
        /// </summary>
        //[ForeignKey("LoginUserId")]
        public LoginUserEntity LoginUser { get; set; }
    }
}
