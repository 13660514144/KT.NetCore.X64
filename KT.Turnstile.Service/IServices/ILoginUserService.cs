﻿using KT.Common.WebApi.HttpModel;
using KT.Turnstile.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IServices
{
    public interface ILoginUserService
    {
        Task<TokenResponse> LoginAsync(LoginUserModel user);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        Task<bool> LogoutAsync(string token);

        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<LoginUserModel> AddOrUpdateAsync(LoginUserModel model);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 获取最新一条数据
        /// </summary>
        /// <returns></returns>
        Task<LoginUserModel> GetLastAsync();

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<LoginUserModel>> GetAllAsync();
    }
}