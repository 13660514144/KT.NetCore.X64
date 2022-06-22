using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    /// <summary>
    /// 人员、卡、访问码、读卡器 组合
    /// </summary>
    public class PersonCardACCodeReaderModel : PersonCardModel
    {
        /// <summary>
        /// 访问码ID
        /// </summary>
        [JsonProperty("acCodeId")]
        public string ACCodeId { get; set; }

        /// <summary>
        /// 向读卡器中增加卡
        /// </summary>
        [JsonProperty("readerCard")]
        public ReaderToCardModel ReaderCard { get; set; }
    }
}