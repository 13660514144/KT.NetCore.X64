using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    public class RemoveReaderCardModel
    {
        /// <summary>
        /// 卡号(sPA_Card 的 cardNo 字段)，不能为空
        /// </summary>
        [JsonProperty("cardNo")]
        public String CardNo { get; set; }

        /// <summary>
        /// 读卡器 ID(sPA_Reader 中的 sID 字段)，不能为空
        /// </summary>
        [JsonProperty("readerId")]
        public String ReaderId { get; set; }
    }
}