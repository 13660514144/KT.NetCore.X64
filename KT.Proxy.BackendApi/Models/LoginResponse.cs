using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KangTa.Visitor.Proxy.ServiceApi.Modes
{
    public class LoginResponse
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account;

        /// <summary>
        /// 用户名称
        /// </summary> 
        public string Name;

        /// <summary>
        ///  密钥，用于接口签名。详情请看接口文档说明
        /// </summary>
        public string Secret;

        /// <summary>
        /// token令牌
        /// </summary>
        public string Token;

    }
}
