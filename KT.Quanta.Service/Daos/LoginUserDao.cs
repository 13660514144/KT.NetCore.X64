using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;

namespace KT.Quanta.Service.Daos
{
    public class LoginUserDao : BaseDataDao<LoginUserEntity>, ILoginUserDao
    {
        public LoginUserDao(QuantaDbContext context) : base(context)
        {
        }
    }
}
