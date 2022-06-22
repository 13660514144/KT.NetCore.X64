using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Prowatch.Service.IDaos;
using KT.Prowatch.Service.Base;
using KT.Prowatch.Service.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.Daos
{
    public class UserTokenDao : BaseDataDao<UserTokenEntity>, IUserTokenDao
    {
        private ProwatchSqliteContext _context;
        public UserTokenDao(ProwatchSqliteContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserTokenEntity> GetByUserIdAsync(string id)
        {
            var result = await _context.UserTokens
                .Include(x => x.LoginUser)
                .FirstOrDefaultAsync(x => x.LoginUser.Id == id);

            return result;
        }

        public async Task<UserTokenEntity> GetByTokenAsync(string token)
        {
            var result = await _context.UserTokens
                .Include(x => x.LoginUser)
                .FirstOrDefaultAsync(x => x.Token == token);

            return result;
        }

        public async Task<UserTokenEntity> AddAsync(UserTokenEntity userToken)
        {
            userToken.Id = IdUtil.NewId();
            if (userToken.LoginUser != null)
            {
                userToken.LoginUser = await _context.LoginUsers.FirstOrDefaultAsync(x => x.Id == userToken.LoginUser.Id);
            }
            await _context.UserTokens.AddAsync(userToken);
            await _context.SaveChangesAsync();
            return userToken;
        }
    }
}
