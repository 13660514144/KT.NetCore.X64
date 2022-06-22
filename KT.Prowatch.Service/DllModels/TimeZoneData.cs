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
    /// 时区信息
    [DataContract]
    public class TimeZoneData
    {
        /// <summary>
        /// 
        /// </summary>
        public TimeZoneData()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public TimeZoneData(sPA_TimeZone source)
        {
            this.Id = source.sID;
            this.Desc = source.sDESCRP;
            this.SystemZone = source.sSYSTEM_ZONE;
            this.AllAccess = source.sALL_ACCESS;
        }

        /// <summary>
        /// 时区ID
        /// </summary>
        [JsonProperty("id")]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 时区描述信息
        /// </summary>
        [JsonProperty("desc")]
        [DataMember]
        public string Desc { get; set; }

        /// <summary>
        /// 系统自带(1为系统，0为用户定义)
        /// </summary>
        [JsonProperty("systemZone")]
        [DataMember]
        public string SystemZone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("allAccess")]
        [DataMember]
        public string AllAccess { get; set; }
    }


    /// <summary>
    /// 时区信息列表
    /// </summary>
    [DataContract]
    public class TimeZoneArray
    {
        /// <summary>
        /// 时区信息列表数据
        /// </summary>
        [DataMember]
        public List<TimeZoneData> dataArray { get; set; }
    }
}
