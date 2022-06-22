using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Models
{
    public class VisitorConfigParms
    {
        /// <summary>
        /// 访问事由
        /// </summary>
        public List<string> AccessReasons { get; set; }

        /// <summary>
        /// 授权方式
        /// </summary>
        public Dictionary<string, string> AuthTypes { get; set; }

        /// <summary>
        /// 授权方式
        /// </summary>
        public string AuthType { get; set; }

        /// <summary>
        /// 随从访客是否需要登记证件号
        /// </summary>
        public bool CheckRetinueCert { get; set; } = true;

        /// <summary>
        /// 访客是否需要人证比对
        /// </summary>
        public bool OpenVisitorCheck { get; set; }

        /// <summary>
        /// 是否打印二维码，true：打印、false：不打印
        /// </summary>
        public bool PrintQrCode { get; set; }
    }
}
