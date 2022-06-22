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
    /// 公司信息
    /// </summary>
    [DataContract]
    public class CompanyData
    {
        /// <summary>
        /// 
        /// </summary>
        public CompanyData()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public CompanyData(sPA_Company source)
        {
            this.Id = source.sID;
            this.Name = source.sNAM;
        }

        public sPA_Company ToSPA()
        {
            sPA_Company source = new sPA_Company();
            source.sID = this.Id;
            source.sNAM = this.Name;
            return source;
        }
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [DataMember]
        public string Name { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class CompanyArray
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<CompanyData> dataArray { get; set; }
    }
}
