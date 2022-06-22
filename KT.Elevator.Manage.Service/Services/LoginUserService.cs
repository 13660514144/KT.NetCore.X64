using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpModel;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
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
            await AddOrUpdateAsync(user);

            _pushUrlHelper.PushUrl = user.ServerAddress;

            var time = DateTimeUtil.UtcNowMillis();
            var tokenResponse = new TokenResponse(IdUtil.NewId(), time, time.AddDayMillis(8000));
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
