using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    public class ACCodeToCardModel
    {
        /// <summary>
        /// 访问码
        /// </summary>
        [JsonProperty("acCodeId")]
        public string ACCodeId { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        [JsonProperty("cardNo")]
        public string CardNo { get; set; }
    }
}