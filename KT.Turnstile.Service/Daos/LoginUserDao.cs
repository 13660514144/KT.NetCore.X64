using KT.Common.Data.Daos;
using KT.Turnstile.Manage.Service.Base;
using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace KT.Turnstile.Manage.Service.Daos
{
    public class LoginUserDao : BaseDataDao<LoginUserEntity>, ILoginUserDao
    {
        public LoginUserDao(QuantaDbContext context) : base(context)
        {

        }
    }
}
