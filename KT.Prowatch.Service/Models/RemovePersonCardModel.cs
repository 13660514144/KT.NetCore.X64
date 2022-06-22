using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    public class RemovePersonCardModel
    {
        /// <summary>
        /// 人员ID(可为空，为空根据卡号查出 )
        /// </summary>
        [JsonProperty("presonId")]
        public string PersonId { get; set; }

        /// <summary>
        /// 卡号Id
        /// </summary>
        [JsonProperty("cardNo")]
        public string CardNo { get; set; }

        ///// <summary>
        ///// 操作状态
        ///// </summary>
        //[JsonProperty("operationCode")]
        //public int OperationCode { get; set; }

        ///// <summary>
        ///// 操作错误信息
        ///// </summary>
        //[JsonProperty("operationMessage")]
        //public string OperationMessage { get; set; }
    }
}