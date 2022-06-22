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
    /// 卡证类型信息
    /// </summary>
    [DataContract]
    public class BadgeTypeData
    {
        /// <summary>
        /// 
        /// </summary>
        public BadgeTypeData()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public BadgeTypeData(sPA_BadgeType source)
        {
            this.Id = source.sID;
            this.Desc = source.sDESCRP;
        }

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 类型描述
        /// </summary>
        [JsonProperty("desc")]
        [DataMember]
        public string Desc { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class BadgeTypeArray
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<BadgeTypeData> dataArray { get; set; }
    }
}
