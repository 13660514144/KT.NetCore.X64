using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 访客记录查询
    /// </summary>
    public class VisitorQuery : PageQuery
    {
        /// <summary>
        /// 开始时间，格式为：yyyy-MM-dd HH:mm:ss【最大支持跨7天时间范围】
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// 结束时间，格式为：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// 访客姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// IC卡
        /// </summary>
        public string IcCard { get; set; }

        /// <summary>
        /// 被访员工姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 被访公司Id列表
        /// </summary>
        public List<long> CompanyIds { get; set; }

        /// <summary>
        /// 访客来源
        /// </summary>
        public string VisitorFrom { get; set; }

        /// <summary>
        /// 访客来访状态
        /// </summary>
        public string VisitorStatus { get; set; }
    }
}
