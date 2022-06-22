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
    public class LoginUserDao : BaseDataDao<LoginUserEntity>, ILoginUserDao
    {
        private ProwatchSqliteContext _context;
        public LoginUserDao(ProwatchSqliteContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LoginUserEntity> GetByDataAsync(LoginUserEntity model)
        {
            var result = await _context.LoginUsers.FirstOrDefaultAsync(x =>
                      x.DBAddr == model.DBAddr
                   && x.DBName == model.DBName
                   && x.DBUser == model.DBUser
                   && x.DBPassword == model.DBPassword
                   && x.PCAddr == model.PCAddr
                   && x.PCUser == model.PCUser
                   && x.PCPassword == model.PCPassword
                   && x.ServerAddress == model.ServerAddress);

            return result;
        }

        public async Task<LoginUserEntity> GetLastAsync()
        {
            var result = await _context.LoginUsers
                .OrderByDescending(x => x.EditedTime)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<LoginUserEntity> GetLastOnTrackAsync()
        {
            var result = await _context.LoginUsers
           .AsNoTracking()
           .OrderByDescending(x => x.EditedTime)
           .FirstOrDefaultAsync();

            return result;
        }

        public async Task<LoginUserEntity> UpdateEditTimeAsync(string id)
        {
            var entity = await _context.LoginUsers.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                entity.EditedTime = DateTimeUtil.UtcNowMillis();
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
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