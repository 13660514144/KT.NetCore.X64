using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Core.Exceptions
{
    public class ExceptionLevelEnum : BaseEnum
    {
        private ExceptionLevelEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        /// <summary>
        /// 业务错误，业务错误一般只记录text文件日记
        /// </summary>
        public static ExceptionLevelEnum BUSINESS => new ExceptionLevelEnum(1, "BUSINESS", "业务错误");

        /// <summary>
        /// 系统错误，当系统错误时拦截器会额外给管理员发邮件等操作
        /// </summary>
        public static ExceptionLevelEnum SYSTEM => new ExceptionLevelEnum(2, "SYSTEM", "系统错误");
    }
}
