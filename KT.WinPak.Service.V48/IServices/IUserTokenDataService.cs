using KT.WinPak.Data.V48.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.WinPak.Service.V48.IServices
{
    public interface IUserTokenDataService
    {
        /// <summary>
        /// 根据token获取数据
        /// </summary>
        /// <param name="token">Token值</param>
        /// <returns></returns>
       Task< UserTokenModel> GetByTokenAsync(string token);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="userToken"></param>
        Task<UserTokenModel> AddAsync(UserTokenModel userToken);

        /// <summary>
        /// 根据用户ID查询数据库中是否存在，如果存在，则修改，否则新增
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns></returns>
       Task<UserTokenModel> AddOrUpdateByUserAsync(UserTokenModel userToken);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns></returns>
        Task<UserTokenModel> UpdateAsync(UserTokenModel userToken);
    }
}
