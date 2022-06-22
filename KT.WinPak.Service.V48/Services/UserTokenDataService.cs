using KT.WinPak.Data.V48.IDaos;
using KT.WinPak.Data.V48.Models;
using KT.WinPak.Service.V48.IServices;
using System.Threading.Tasks;

namespace KT.WinPak.Service.V48.Services
{
    public class UserTokenDataService : IUserTokenDataService
    {
        private IUserTokenDataDao dao;

        public UserTokenDataService(IUserTokenDataDao dao)
        {
            this.dao = dao;
        }

        public async Task<UserTokenModel> AddOrUpdateByUserAsync(UserTokenModel userToken)
        {
            var result =await dao.GetByUserIdAsync(userToken.LoginUser?.Id);
            if (result != null)
            {
                return await UpdateAsync(userToken);
            }
            return await AddAsync(userToken);

        }

        public async Task<UserTokenModel> UpdateAsync(UserTokenModel model)
        {
            var entity = UserTokenModel.ToEntity(model);
            await dao.AttachAsync(entity);

            model.Id = entity.Id;
            model.EditedTime = entity.EditedTime;
            return model;
        }


        /// <summary>
        /// 获取登录用户Token数据
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserTokenModel> GetByTokenAsync(string token)
        {
            var entity = await dao.GetByTokenAsync(token);
            var model = UserTokenModel.ToModel(entity);
            return model;
        }

        public async Task<UserTokenModel> AddAsync(UserTokenModel model)
        {
            var entity = UserTokenModel.ToEntity(model);
             
            await dao.AddAsync(entity);

            model.Id = entity.Id;
            model.EditedTime = entity.EditedTime;
            return model;
        }
    }
}
