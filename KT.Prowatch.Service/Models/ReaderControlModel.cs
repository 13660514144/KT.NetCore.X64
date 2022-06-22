using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    public class ReaderControlModel
    {
        /// <summary>
        /// 读卡器 ID(sPA_Reader 中的 sID 字段)
        /// </summary>
        [JsonProperty("readerId")]
        public String ReaderId { get; set; }

        /// <summary>
        /// 控制类型，支持包括如下类型：1：再启用，2：瞬间解锁，3：解锁，4：锁
        /// </summary>
        [JsonProperty("cmd")]
        public int Cmd { get; set; }
    }
}