using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using ProwatchAPICS;

namespace KT.Prowatch.Service.DllModels
{
    /// <summary>
    /// Id类
    /// </summary>
    [DataContract]
    public class IdData
    {
        /// <summary>
        /// 
        /// </summary>
        public IdData()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public IdData(sPA_ID source)
        {
            this.Data = source.sData;
        }
        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty("data")]
        [DataMember]
        public string Data { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class IdArray
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<IdData> dataArray { get; set; }
    }
}
