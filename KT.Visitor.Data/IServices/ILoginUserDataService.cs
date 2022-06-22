using KT.Visitor.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.IServices
{
    public interface ILoginUserDataService
    {
        Task AddOrUpdateAsync(LoginUserEntity loginUser);
        Task<LoginUserEntity> GetByAccountAsync(string account);
        Task<LoginUserEntity> GetLastAsync();
        Task<bool> IsExistsSystemUserAsync(string password);
    }
}
