using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    public class ReaderToACCodeModel
    {
        /// <summary>
        /// 访问码 ID(sPA_AccessCode 中的 sID 字段)，不能为空
        /// </summary>
        [JsonProperty("acCodeId")]
        public String AcCodeId { get; set; }

        /// <summary>
        /// 读卡器 ID(sPA_Reader 中的 sID 字段)，不能为空
        /// </summary>
        [JsonProperty("readerId")]
        public String ReaderId { get; set; }

        /// <summary>
        /// 时区类型ID(sPA_TimeZone中的sID字段)，如空则使用访问码默认时区
        /// </summary>
        [JsonProperty("timeZoneId")]
        public String TimeZoneId { get; set; }
    }
}