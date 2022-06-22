using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Data.Models
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    public static class DataStaticInfo
    {
        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        public static string CurrentUserId { get; set; }
    }
}
