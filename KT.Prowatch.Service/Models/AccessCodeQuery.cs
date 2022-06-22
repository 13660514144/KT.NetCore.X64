using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    /// <summary>
    /// 访问码条件查询
    /// </summary>
    public class AccessCodeQuery
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        [JsonProperty("personId")]
        public String PersonId { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [JsonProperty("cardNo")]
        public String CardNo { get; set; }
    }
}