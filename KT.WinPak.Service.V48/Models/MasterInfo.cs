using KT.WinPak.Data.V48.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.Service.V48.Models
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    public static class MasterInfo
    {

        /// <summary>
        /// 退出登录，销毁Api时使用
        /// </summary>
        public static Action Logout;

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static LoginUserModel LoginUser { get; set; }

        /// <summary>
        /// 所有登录用户
        /// </summary>

        public static ConcurrentDictionary<string, LoginUserModel> LoginUsers { get; } = new ConcurrentDictionary<string, LoginUserModel>();

        /// <summary>
        /// 所有登录用户
        /// </summary>

        public static ConcurrentDictionary<string, UserTokenModel> TokenDatas { get; } = new ConcurrentDictionary<string, UserTokenModel>();
    }
}
