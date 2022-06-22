using KT.Visitor.Data.Entity;
using KT.Visitor.Data.IDaos;
using KT.Visitor.Data.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Services
{
    public class LoginUserDataService : ILoginUserDataService
    {
        private ILoginUserDataDao _loginUserDao;

        public LoginUserDataService(ILoginUserDataDao loginUserDao)
        {
            _loginUserDao = loginUserDao;
        }

        public async Task<bool> IsExistsSystemUserAsync(string password)
        {
            return await _loginUserDao.IsExistsSystemUserAsync(password);
        }

        public async Task AddOrUpdateAsync(LoginUserEntity loginUser)
        {
            var user = await _loginUserDao.GetByAccountAsync(loginUser.Account);
            if (user != null)
            {
                user.SetUpdateValue(loginUser);
                await _loginUserDao.AttachAsync(user);
            }
            else
            {
                await _loginUserDao.InsertAsync(loginUser);
            }
        }

        public async Task<LoginUserEntity> GetLastAsync()
        {
            return await _loginUserDao.SelectLastAsync();
        }

        public async Task<LoginUserEntity> GetByAccountAsync(string account)
        {
            return await _loginUserDao.GetByAccountAsync(account);
        }
    }
}
