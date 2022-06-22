using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KangTa.Visitor.Proxy.ServiceApi.Modes
{
    /// <summary>
    /// 状态流
    /// </summary>
    public class StatusFlowModel
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 访问信息
        /// </summary>
        public string StatusInfo { get; set; }

        /// <summary>
        /// 访问状态
        /// </summary>
        public string VisitorStatus { get; set; }
    }
}
