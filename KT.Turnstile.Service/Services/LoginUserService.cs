using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Manage.Service.IServices;
using KT.Turnstile.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KT.Common.WebApi.HttpModel;
using KT.Common.Core.Utils;
using KT.Turnstile.Manage.Service.Helpers;

namespace KT.Turnstile.Manage.Service.Services
{
    public class LoginUserService : ILoginUserService
    {
        private ILoginUserDao _loginUserDataDao;
        private PushUrlHelper _pushUrlHelper;

        public LoginUserService(ILoginUserDao loginUserDataDao,
             PushUrlHelper pushUrlHelper)
        {
            _loginUserDataDao = loginUserDataDao;
            _pushUrlHelper = pushUrlHelper;
        }

        public async Task<TokenResponse> LoginAsync(LoginUserModel user)
        {
            //分发数据存在就不增加了
            await AddOrUpdateAsync(user);

            _pushUrlHelper.PushUrl = user.ServerAddress;

            var tokenResponse = new TokenResponse(IdUtil.NewId(), DateTimeUtil.UtcNowMillis(), DateTimeUtil.UtcNowMillis().AddDayMillis(8000));
            return tokenResponse;
        }

        public async Task<bool> LogoutAsync(string token)
        {
            return await Task.Run(() =>
            {
                return true;
            });
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _loginUserDataDao.HasInstanceByIdAsync(id);
        }

        public async Task<LoginUserModel> AddOrUpdateAsync(LoginUserModel model)
        {
            var entity = await _loginUserDataDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = LoginUserModel.ToEntity(model);
                await _loginUserDataDao.InsertAsync(entity);
            }
            else
            {
                entity = LoginUserModel.SetEntity(entity, model);
                await _loginUserDataDao.AttachUpdateAsync(entity);
            }
            return LoginUserModel.SetModel(model, entity);
        }

        public async Task<LoginUserModel> GetLastAsync()
        {
            var entity = await _loginUserDataDao.SelectLastAsync();
            if (entity == null)
            {
                return null;
            }
            return LoginUserModel.ToModel(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _loginUserDataDao.DeleteByIdAsync(id);
        }

        public async Task<List<LoginUserModel>> GetAllAsync()
        {
            var entities = await _loginUserDataDao.SelectAllAsync() ;

            var models = LoginUserModel.ToModels(entities);

            return models;
        }
    }
}
