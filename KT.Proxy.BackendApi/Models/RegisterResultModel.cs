using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 访问登记结果
    /// </summary>
    public class RegisterResultModel
    {
        /// <summary>
        /// 大厦名称
        /// </summary>
        public string EdificeName { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string DateTime { get; set; }

        /// <summary>
        /// 访客姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 验证类型
        /// </summary>
        public string AuthType { get; set; }

        /// <summary>
        /// 验证类型
        /// </summary>
        public List<string> AuthTypes { get; set; }

        /// <summary>
        /// 二维码内容
        /// </summary>
        public string Qr { get; set; }
    }
}
