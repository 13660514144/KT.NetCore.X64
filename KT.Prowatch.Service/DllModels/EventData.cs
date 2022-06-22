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
    /// 事件信息
    /// </summary>
    [DataContract]
    public class EventData
    {
        /// <summary>
        /// 
        /// </summary>
        public EventData()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public EventData(sPA_Event source)
        {
            this.LogicDevDesc = source.sLOGDEVDESCRP;
            this.LogicDevTypeDesc = source.sLOGDEVTYPEDESC;
            this.ReceiveDate = source.sREC_DAT;
            this.EvntDate = source.sEVNT_DAT;
            this.EvntDesc = source.sEVNT_DESCRP;
            this.Desc = source.sDESCRP;
            this.Location = source.sLOCATION;
            this.CardNo = source.sCARDNO;
            this.LastName = source.sLNAME;
            this.FirstName = source.sFNAME;
            this.StateCode = source.sSTAT_COD;
            this.CompanyName = source.sCOMP_NAME;
            this.ThreatLevel = source.sTHREAT_LEV;
        }

        /// <summary>
        /// 逻辑设备描述
        /// </summary>
        [JsonProperty("logicDevDesc")]
        [DataMember]
        public string LogicDevDesc { get; set; }

        /// <summary>
        /// 逻辑设备类型描述
        /// </summary>
        [JsonProperty("logicDevTypeDesc")]
        [DataMember]
        public string LogicDevTypeDesc { get; set; }

        /// <summary>
        /// 系统时间(接收到事件的时间)
        /// </summary>
        [JsonProperty("receiveDate")]
        [DataMember]
        public string ReceiveDate { get; set; }

        /// <summary>
        /// 事件发生时间
        /// </summary>
        [JsonProperty("evntDate")]
        [DataMember]
        public string EvntDate { get; set; }

        /// <summary>
        /// 事件描述
        /// </summary>
        [JsonProperty("evntDesc")]
        [DataMember]
        public string EvntDesc { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("desc")]
        [DataMember]
        public string Desc { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [JsonProperty("location")]
        [DataMember]
        public string Location { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [JsonProperty("cardNo")]
        [DataMember]
        public string CardNo { get; set; }

        /// <summary>
        /// 持卡人姓
        /// </summary>
        [JsonProperty("lastName")]
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// 持卡人名
        /// </summary>
        [JsonProperty("firstName")]
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// 卡状态
        /// </summary>
        [JsonProperty("stateCode")]
        [DataMember]
        public string StateCode { get; set; }

        /// <summary>
        /// 公司名
        /// </summary>
        [JsonProperty("companyName")]
        [DataMember]
        public string CompanyName { get; set; }

        /// <summary>
        /// 报警等级
        /// </summary>
        [JsonProperty("threatLevel")]
        [DataMember]
        public string ThreatLevel { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class EventArray
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<EventData> dataArray { get; set; }
    }
}
