using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    /// <summary>
    /// 人员、卡、访问码 组合
    /// </summary>
    public class PersonCardACCodeModel : PersonCardModel
    {
        /// <summary>
        /// 访问码ID
        /// </summary>
        [JsonProperty("acCodeId")]
        public string ACCodeId { get; set; }
    }
}