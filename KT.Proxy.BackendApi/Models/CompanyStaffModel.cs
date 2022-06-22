using KT.Common.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 根据关键词搜索公司或员工信息
    /// </summary>
    public class CompanyStaffModel
    {
        private string staffName;
        private string companyName;

        /// <summary>
        /// 公司Id
        /// </summary>
        public long? CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;

                DisplayName = $"{value}-{StaffName}";

                DisplayName = DisplayName.Trim("-");
            }
        }

        /// <summary>
        /// 员工Id
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>
        /// 员工名称
        /// </summary>
        public string StaffName
        {
            get
            {
                return staffName;
            }
            set
            {
                staffName = value;

                DisplayName = $"{CompanyName}-{value}";

                DisplayName = DisplayName.Trim("-");
            }
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        [JsonIgnore]
        public string DisplayName { get; set; }

    }
}
