using KT.Common.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.WebApi.HttpModel
{
    public class TokenResponse
    {
        /// <summary>
        /// Token值
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 当前系统UTC时间，单位毫秒
        /// </summary>
        public long TimeNow { get; set; }

        /// <summary>
        /// 当前系统UTC时间，单位毫秒
        /// </summary>
        public long TimeOut { get; set; }
         
        public TokenResponse(string token,long timeNow,long timeOut)
        { 
            this.Token = token;
            this.TimeNow = timeNow;
            this.TimeOut = timeOut;
        }
    }
}
