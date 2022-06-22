
using KT.Prowatch.Service.DllModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    /// <summary>
    /// 时区、访问有效期限信息 组合
    /// </summary>
    public class TimeZoneAccessDurationModel
    {
        /// <summary>
        /// 时区信息
        /// </summary>
        [JsonProperty("timeZones")]
        public List<TimeZoneData> TimeZones { get; set; }

        /// <summary>
        /// 访问有效期限信息（和时区一一对应）
        /// </summary>
        [JsonProperty("accessDurations")]
        public List<AccessDurationData> AccessDurations { get; set; }
    }
}