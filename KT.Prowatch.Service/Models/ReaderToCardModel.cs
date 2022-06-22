using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    /// <summary>
    /// 读卡器与卡组合
    /// </summary>
    public class ReaderToCardModel
    {
        /// 卡号(sPA_Card 的 sCardNO 字段)，不能为空
        [JsonProperty("cardNo")]
        public String CardNo { get; set; }

        /// 读卡器 ID(sPA_Reader 中的 sID 字段)，不能为空
        [JsonProperty("readerId")]
        public String ReaderId { get; set; }

        /// 时区类型ID(sPA_TimeZone中的sID字段)
        [JsonProperty("timeZoneId")]
        public String TimeZoneId { get; set; }

        /// "Y"--临时访问, "N"或为空时--永久有效
        [JsonProperty("tempAccess")]
        public String TempAccess { get; set; }

        /// 临时访问起始时间（sTEMPACCESS 为"Y"时有效） 
        [JsonProperty("tempAccessStartTime")]
        public String TempAccessStartTime { get; set; }

        /// 临时访问结束时间（sTEMPACCESS 为"Y"时有效） 
        [JsonProperty("tempAccessEndTime")]
        public String TempAccessEndTime { get; set; }
    }
}