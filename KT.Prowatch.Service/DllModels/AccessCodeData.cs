using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using ProwatchAPICS;
using ProwatchAPICS;

namespace KT.Prowatch.Service.DllModels
{
    /// <summary>
    /// 访问码信息
    /// </summary>
    [DataContract]
    public class AccessCodeData
    {
        /// <summary>
        /// 
        /// </summary>
        public AccessCodeData()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public AccessCodeData(sPA_AccessCode source)
        {
            this.Id = source.sID;
            this.Desc = source.sDESCRP;
            this.DefaultTimeZone = source.sDEF_TZ;
        }

        /// <summary>
        /// 访问码ID
        /// </summary>
        [JsonProperty("id")]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 访问码描述信息
        /// </summary>
        [JsonProperty("desc")]
        [DataMember]
        public string Desc { get; set; }

        /// <summary>
        /// 默认时区
        /// </summary>
        [JsonProperty("defaultTimeZone")]
        [DataMember]
        public string DefaultTimeZone { get; set; }
    }

    /// <summary>
    /// 访问码列表
    /// </summary>
    [DataContract]
    public class AccessCodeArray
    {
        /// <summary>
        /// 访问码列表数据
        /// </summary>
        [DataMember]
        public List<AccessCodeData> dataArray { get; set; }
    }
}
