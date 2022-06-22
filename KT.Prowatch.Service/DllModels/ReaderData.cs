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
    /// 逻辑设备(读卡器)信息
    /// </summary>
    [DataContract]
    public class ReaderData
    {
        /// <summary>
        /// 
        /// </summary>
        public ReaderData()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public ReaderData(sPA_Reader source)
        {
            this.Id = source.sID;
            this.Desc = source.sDESCRP;
            this.Location = source.sLOCATION;
            this.AltDesc = source.sALT_DESCRP;
            this.Panel = source.sPANEL;
            this.Site = source.sSITE;
        }

        /// <summary>
        /// 读卡器ID
        /// </summary>
        [JsonProperty("id")]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 读卡器描述信息
        /// </summary>
        [JsonProperty("desc")]
        [DataMember]
        public string Desc { get; set; }

        /// <summary>
        /// 读卡器位置
        /// </summary>
        [JsonProperty("location")]
        [DataMember]
        public string Location { get; set; }

        /// <summary>
        /// 读卡器可选描述信息
        /// </summary>
        [JsonProperty("altDesc")]
        [DataMember]
        public string AltDesc { get; set; }

        /// <summary>
        /// 读卡器所属控制器
        /// </summary>
        [JsonProperty("panel")]
        [DataMember]
        public string Panel { get; set; }

        /// <summary>
        /// 读卡器所属站点
        /// </summary>
        [JsonProperty("site")]
        [DataMember]
        public string Site { get; set; }
    }

    /// <summary>
    /// 读卡器列表
    /// </summary>
    [DataContract]
    public class ReaderArray
    {
        /// <summary>
        /// 读卡器列表数据 
        /// </summary>
        [DataMember]
        public List<ReaderData> dataArray { get; set; }
    }
}
