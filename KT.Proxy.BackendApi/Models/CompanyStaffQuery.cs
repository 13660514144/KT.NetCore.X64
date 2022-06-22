using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 根据关键词搜索公司或员工信息
    /// </summary>
    public class CompanyStaffQuery
    {
        /// <summary>
        /// 关键词
        /// </summary>
        public string Search { get; set; }
    }
}
