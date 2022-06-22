using KT.Prowatch.Service.IDaos;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.Services
{
    public class UserTokenService : IUserTokenService
    {
        private IUserTokenDao _dao;
        public UserTokenService(IUserTokenDao dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserTokenModel> GetByTokenAsync(string token)
        {
            //从内存列表中查询Token信息
            var tokenData = LoginHelper.Instance.TokenDatas.FirstOrDefault(x => x.Key == token).Value;

            //列表中不存在从数据库中查找
            if (tokenData == null)
            {
                var entity = await _dao.GetByTokenAsync(token);
                if (entity != null)
                {
                    tokenData = UserTokenModel.ToModel(entity);

                    //TokenData加入内存列表
                    LoginHelper.Instance.TokenDatas.AddOrUpdate(tokenData.Token, tokenData, UpdateValueFactory);
                }
            }

            return tokenData;
        }

        private UserTokenModel UpdateValueFactory(string arg1, UserTokenModel arg2)
        {
            return arg2;
        }

        public async Task<UserTokenModel> InsertAsync(UserTokenModel tokenData)
        {
            var tokenDataEntity = UserTokenModel.ToEntity(tokenData);
            await _dao.InsertAsync(tokenDataEntity);
            return tokenData;
        }
    }
}
