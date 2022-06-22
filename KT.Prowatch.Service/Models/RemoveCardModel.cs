using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    public class RemoveCardModel
    {
        /// <summary>
        /// 卡号
        /// </summary>
        [JsonProperty("cardNo")]
        public string CardNo { get; set; }
    }
}