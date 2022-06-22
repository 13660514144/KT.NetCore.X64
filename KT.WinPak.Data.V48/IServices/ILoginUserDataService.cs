using KT.WinPak.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.Data.IServices
{
    public interface ILoginUserDataService
    {
        /// <summary>
        /// 新增或修改
        /// </summary>
        /// <param name="model">登录用户信息</param>
        /// <returns></returns>
        LoginUserModel AddOrUpdate(LoginUserModel model);

        /// <summary>
        /// 获取最近一条登录用户信息
        /// </summary>
        /// <returns></returns>
        LoginUserModel GetLast();
    }
}
