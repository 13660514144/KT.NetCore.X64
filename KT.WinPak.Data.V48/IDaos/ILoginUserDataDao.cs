using KT.Common.Data.Daos;
using KT.WinPak.Data.V48.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.WinPak.Data.V48.IDaos
{
    public interface ILoginUserDataDao : IBaseDataDao<LoginUserEntity>
    {
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="model">除ID以外的其它参数</param>
        /// <returns></returns>
        Task<LoginUserEntity> SelectByDataAsync(LoginUserEntity model);

        /// <summary>
        /// 获取最近一条登录信息
        /// </summary>
        /// <returns></returns>
        LoginUserEntity SelectLast();
    }
}
