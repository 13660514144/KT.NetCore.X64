using KT.WinPak.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.Data.IServices
{
    public interface IUserTokenDataService
    {
        /// <summary>
        /// 根据token获取数据
        /// </summary>
        /// <param name="token">Token值</param>
        /// <returns></returns>
        UserTokenModel GetByToken(string token);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="userToken"></param>
        UserTokenModel Insert(UserTokenModel userToken);

        /// <summary>
        /// 根据用户ID查询数据库中是否存在，如果存在，则修改，否则新增
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns></returns>
        UserTokenModel AddOrUpdateByUser(UserTokenModel userToken);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns></returns>
        UserTokenModel Update(UserTokenModel userToken);
    }
}
