using KT.Common.Data.Daos;
using KT.Prowatch.Service.Entities;
using KT.Prowatch.Service.Models;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.IDaos
{
    public interface IUserTokenDao : IBaseDataDao<UserTokenEntity>
    {
        /// <summary>
        /// 根据登录用户Id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserTokenEntity> GetByUserIdAsync(string id);

        /// <summary>
        /// 根据token值获取Token信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UserTokenEntity> GetByTokenAsync(string token);

        /// <summary>
        /// 新增tokenData
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns></returns>
        Task<UserTokenEntity> AddAsync(UserTokenEntity userToken);
    }
}
