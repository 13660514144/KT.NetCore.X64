using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Model.Models
{
    /// <summary>
    /// 系统配置关键字
    /// </summary>
    public class SystemConfigEnum : BaseEnum
    {
        public SystemConfigEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        /// <summary>
        /// 当前服务Key
        /// </summary>
        public static SystemConfigEnum SERVER_KEY { get; } = new SystemConfigEnum(2, "SERVER_KEY", "当前服务Key");
    }
}
