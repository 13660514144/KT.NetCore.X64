using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Entity
{
    public class LoginUserEntity : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }

        public void SetUpdateValue(LoginUserEntity entity)
        {
            Name = entity.Name;
            Account = entity.Account;
            Password = entity.Password;
            UserType = entity.UserType;
            Editor = entity.Editor;
            EditedTime = entity.EditedTime;
        }
    }
}
