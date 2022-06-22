using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KangTa.Visitor.Proxy.ServiceApi.Modes
{
    /// <summary>
    /// 访客统计
    /// </summary>
    public class VisitorStatisticModel
    {
        /// <summary>
        /// 在访人数
        /// </summary>
        public int VisitorNum { get; set; }

        /// <summary>
        /// 到访总人数
        /// </summary>
        public int TotalVisitorNum { get; set; }
    }
}
