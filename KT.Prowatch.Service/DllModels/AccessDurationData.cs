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
    /// 访问有效期限信息
    /// </summary>
    [DataContract]
    public class AccessDurationData
    {
        /// <summary>
        /// 
        /// </summary>
        public AccessDurationData()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public AccessDurationData(sPA_Access_Duration source)
        {
            this.Expire = source.sEXPIRE;
            this.TempAccess = source.sTEMPACCESS;
            this.TempAccessStartTime = source.sTEMPACCESS_START_TIME;
            this.TempAccessEndTime = source.sTEMPACCESS_END_TIME;
            this.Duration = source.sDURATION;
            this.DurType = source.sDUR_TYPE;
        }

        /// <summary>
        /// "Y"--有失效期, "N"--永久有效, 为空时根据tempAccess
        /// </summary>
        [JsonProperty("expire")]
        [DataMember]
        public string Expire { get; set; }

        /// <summary>
        /// "Y"--临时访问, "N"--持续时间访问(expire为"Y"时有效), "N"--永久有效(expire为空时)
        /// </summary>
        [JsonProperty("tempAccess")]
        [DataMember]
        public string TempAccess { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonProperty("tempAccessStartTime")]
        [DataMember]
        public string TempAccessStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonProperty("tempAccessEndTime")]
        [DataMember]
        public string TempAccessEndTime { get; set; }

        /// <summary>
        /// 持续时间(expire为"Y"并且tempAccess为"N"时有效)
        /// </summary>
        [JsonProperty("duration")]
        [DataMember]
        public string Duration { get; set; }

        /// <summary>
        /// 时间类型("D"--天, "H"--小时, "M"--分钟)
        /// </summary>
        [JsonProperty("durType")]
        [DataMember]
        public string DurType { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class AccessDurationArray
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<AccessDurationData> dataArray { get; set; }
    }
}
