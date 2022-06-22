using KT.Common.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.WebApi.HttpModel
{
    public class TokenModelBase
    {
        /// <summary>
        /// Token值
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public long TimeNow { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public long TimeOut { get; set; }

        public TokenModelBase()
        {
            Token = IdUtil.NewId();
            TimeNow = DateTimeUtil.UtcNowMillis();
            TimeOut = TimeNow.AddDayMillis(365000);
        }
    }
}
