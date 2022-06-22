using KT.Common.WebApi.HttpModel;
using KT.WinPak.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.WinPak.Service.IServices
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        Task<TokenResponse> LoginAsync(LoginUserModel config);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        bool Logout();

        /// <summary>
        /// 根据token值获取Token详细信息
        /// </summary>
        /// <param name="token">token值</param>
        /// <returns>Token详细信息</returns>
        Task<UserTokenModel> GetByTokenAsync(string token);

        /// <summary>
        /// 登录应用
        /// </summary>
        /// <param name="userToken">用户Token</param>
        /// <param name="isLoginAccwEvent">是否登录事件上传</param>
        /// <returns>是否操作成功</returns>
        bool LoginApp(LoginUserModel loginUser, bool isLoginAccwEvent = true);

        /// <summary>
        /// 登录最新一条数据
        /// </summary>
        void Login(bool isLoginAccwEvent = true);

        /// <summary>
        /// 重新创建SDK对象并登录
        /// </summary>
        void ReloadAndLoginApp();
    }
}
