using KT.Common.Data.Daos;
using KT.WinPak.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.WinPak.Data.IDaos
{
    public interface IUserTokenDataDao:IBaseDataDao<UserTokenEntity>
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
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(UserTokenEntity entity);
    }
}
