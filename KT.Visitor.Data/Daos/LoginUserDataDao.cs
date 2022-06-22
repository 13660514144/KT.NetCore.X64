using KT.Common.Data.Daos;
using KT.Visitor.Data.Base;
using KT.Visitor.Data.Entity;
using KT.Visitor.Data.Enums;
using KT.Visitor.Data.IDaos;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Daos
{
    public class LoginUserDataDao : BaseDataDao<LoginUserEntity>, ILoginUserDataDao
    {
        private SqliteContext _context;
        public LoginUserDataDao(SqliteContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsExistsSystemUserAsync(string password)
        {
            string userType = UserTypeEnum.USER_SYSTEM_INLAY.Value;
            var result = await base.HasInstanceAsync(x => x.Password == password && x.UserType == userType);
            return result;
        }

        public async Task<LoginUserEntity> GetByAccountAsync(string account)
        {
            var user = await SelectFirstByLambdaAsync(x => x.Account == account);
            return user;
        }
    }
}
