using KT.WinPak.Data.V48.IDaos;
using KT.WinPak.Data.V48.Models;
using KT.WinPak.Service.V48.IServices;
using System.Threading.Tasks;

namespace KT.WinPak.Service.V48.Services
{
    public class LoginUserDataService : ILoginUserDataService
    {
        private ILoginUserDataDao dao;

        public LoginUserDataService(ILoginUserDataDao dao)
        {
            this.dao = dao;
        }

        public async Task<LoginUserModel> AddOrUpdateAsync(LoginUserModel model)
        {
            var entity = LoginUserModel.ToEntity(model);
            var oldEntity = await dao.SelectByDataAsync(entity);

            if (oldEntity != null)
            {
                return LoginUserModel.ToModel(oldEntity);
            }
            else
            {
                await dao.InsertAsync(entity);

                model.Id = entity.Id;
                model.EditedTime = entity.EditedTime;
                return model;
            }
        }

        /// <summary>
        /// 获取最近一条记录
        /// </summary>
        /// <returns></returns>
        public LoginUserModel GetLast()
        {
            var entity = dao.SelectLast();

            return LoginUserModel.ToModel(entity);
        }
    }
}
