using KT.Visitor.Data.Entity;
using KT.Visitor.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Base
{
    public class EntitySeed
    {
        public static async Task ForLoginUserAsync(SqliteContext context)
        {
            //管理员账户
            var password = "82257640";
            string userType = UserTypeEnum.USER_SYSTEM_INLAY.Value;
            var user = await context.LoginUsers.FirstOrDefaultAsync(x => x.Password == password && x.UserType == userType);

            if (user == null)
            {
                var entity = new LoginUserEntity();
                entity.UserType = userType;
                entity.Password = password;
                await context.LoginUsers.AddAsync(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
