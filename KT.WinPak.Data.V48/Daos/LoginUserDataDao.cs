using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.WinPak.Data.V48.Base;
using KT.WinPak.Data.V48.IDaos;
using KT.WinPak.Data.V48.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.WinPak.Data.V48.Daos
{
    public class LoginUserDataDao :  BaseDataDao<LoginUserEntity>, ILoginUserDataDao
    {
        private WinPakSqliteContext _context;
        public LoginUserDataDao(WinPakSqliteContext context) :base(context)
        {
            _context = context;
        }
          
        public async Task<LoginUserEntity> SelectByDataAsync(LoginUserEntity model)
        {
            var result =await _context.LoginUsers.Where(x =>
                       x.DBAddr == model.DBAddr
                    && x.DBName == model.DBName
                    && x.DBUser == model.DBUser
                    && x.DBPassword == model.DBPassword
                    && x.PCAddr == model.PCAddr
                    && x.PCUser == model.PCUser
                    && x.PCPassword == model.PCPassword
                    && x.ServerAddress == model.ServerAddress)
                .FirstOrDefaultAsync();

            return result;
        }

        public LoginUserEntity SelectLast()
        {
            var result = _context.LoginUsers
                .OrderByDescending(x => x.EditedTime)
                .FirstOrDefault();

            return result;
        }
    }
}
//&& x.DBAddr           == model.DBAddr            
//&& x.DBName           == model.DBName            
//&& x.DBUser           == model.DBUser            
//&& x.DBPassword       == model.DBPassword        
//&& x.PCAddr           == model.PCAddr            
//&& x.PCUser           == model.PCUser            
//&& x.PCPassword       == model.PCPassword        
//&& x.ServerAddress    == model.ServerAddress     