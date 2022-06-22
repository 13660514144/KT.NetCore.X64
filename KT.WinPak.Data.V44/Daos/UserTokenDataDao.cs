using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.WinPak.Data.Base;
using KT.WinPak.Data.IDaos;
using KT.WinPak.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.WinPak.Data.Daos
{
    public class UserTokenDataDao : BaseDataDao<UserTokenEntity>, IUserTokenDataDao
    {
        private WinPakSqliteContext _context;
        public UserTokenDataDao(WinPakSqliteContext context):base(context)
        {
            _context = context;
        }

        public async Task AddAsync(UserTokenEntity entity)
        {
            if( entity .LoginUser != null)
            {
                entity.LoginUser = await _context.LoginUsers.FirstOrDefaultAsync(x => x.Id == entity.LoginUser .Id);
            }
             
              await base.InsertAsync(entity); 
        }

        public async Task<UserTokenEntity> GetByTokenAsync(string token)
        {
            var    result =await _context.UserTokens
                .Include(x => x.LoginUser)
                .FirstOrDefaultAsync(x => x.Token == token);

            return result;
        }

        public async Task<UserTokenEntity> GetByUserIdAsync(string id)
        {
            var result = await _context.UserTokens
                .Include(x => x.LoginUser)
                .FirstOrDefaultAsync(x => x.LoginUser.Id == id);

            return result;
        } 
    }
}
