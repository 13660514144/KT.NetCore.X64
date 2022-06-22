using KT.Common.Data.Daos;
using KT.Prowatch.Service.Entities;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.IDaos
{
    public interface ILoginUserDao : IBaseDataDao<LoginUserEntity>
    {
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="model">除ID以外的其它参数</param>
        /// <returns></returns>
        Task<LoginUserEntity> GetByDataAsync(LoginUserEntity model);

        /// <summary>
        /// 只更新修改时间
        /// </summary>
        /// <param name="oldConnect"></param>
        /// <returns></returns>
        Task<LoginUserEntity> UpdateEditTimeAsync(string id);

        /// <summary>
        /// 获取最新一条数据
        /// </summary>
        /// <returns></returns>
        Task<LoginUserEntity> GetLastAsync();

        /// <summary>
        /// 获取最新一条数据
        /// </summary>
        /// <returns></returns>
        Task<LoginUserEntity> GetLastOnTrackAsync();
    }
}
