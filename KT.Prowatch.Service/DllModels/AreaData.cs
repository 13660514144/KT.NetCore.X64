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
    /// 区域信息
    /// </summary>
    [DataContract]
    public class AreaData
    {
        /// <summary>
        /// 
        /// </summary>
        public AreaData( )
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public AreaData(sPA_Area source)
        {
            this.Id = source.sID;
            this.AreaName = source.sAREA_NAME;
            this.OccupancyCountMin = source.sOCCUPANCY_COUNT_MIN;
            this.OccupancyCountMax = source.sOCCUPANCY_COUNT_MAX;
            this.TwoPersonControl = source.sTWO_PERSON_CONTROL;
            this.PassBack = source.sPASS_BACK;
            this.Locked = source.sLOCKED;
        }

        /// <summary>
        /// 区域ID
        /// </summary>
        [JsonProperty("id")]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 区域描述信息
        /// </summary>
        [JsonProperty("areaName")]
        [DataMember]
        public string AreaName { get; set; }

        /// <summary>
        /// 区域最小人数
        /// </summary>
        [JsonProperty("occupancyCountMin")]
        [DataMember]
        public string OccupancyCountMin { get; set; }

        /// <summary>
        /// 区域最大人数
        /// </summary>
        [JsonProperty("occupancyCountMax")]
        [DataMember]
        public string OccupancyCountMax { get; set; }

        /// <summary>
        /// 双卡核实（0--未激活，1--激活）
        /// </summary>
        [JsonProperty("twoPersonControl")]
        [DataMember]
        public string TwoPersonControl { get; set; }

        /// <summary>
        /// 防反传类型(0--无防反传，1--软件防反传，1--硬件防反传)
        /// </summary>
        [JsonProperty("passBack")]
        [DataMember]
        public string PassBack { get; set; }

        /// <summary>
        /// 锁定(0--未锁定，1--锁定)
        /// </summary>
        [JsonProperty("locked")]
        [DataMember]
        public string Locked { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class AreaArray
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<AreaData> dataArray { get; set; }
    }
}
