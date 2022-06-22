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
    /// 人员信息
    /// </summary>
    public class PersonData
    {
        /// <summary>
        /// 
        /// </summary>
        public PersonData()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public PersonData(sPA_Person source)
        {
            this.Id = source.sID;
            this.FirstName = source.sFNAME;
            this.LastName = source.sLNAME;
            this.IssueDate = source.sISSUE_DATE;
            this.ExpireDate = source.sEXPIRE_DATE;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public sPA_Person ToSPA()
        {
            sPA_Person source = new sPA_Person();
            source.sID = this.Id;
            source.sFNAME = this.FirstName;
            source.sLNAME = this.LastName;
            source.sISSUE_DATE = this.IssueDate;
            source.sEXPIRE_DATE = this.ExpireDate;
            return source;
        }

        #region 新增
        /// <summary>
        /// 卡类型信息
        /// </summary>
        [JsonProperty("badgeTypeId")]
        public string BadgeTypeId { get; set; }

        #endregion

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        [JsonProperty("lastName")]
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        [JsonProperty("firstName")]
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// 人员发行日期
        /// </summary>
        [JsonProperty("issueDate")]
        [DataMember]
        public string IssueDate { get; set; }

        /// <summary>
        /// 人员过期日期
        /// </summary>
        [JsonProperty("expireDate")]
        [DataMember]
        public string ExpireDate { get; set; }

        /// <summary>
        /// 操作状态码
        /// </summary>
        [JsonProperty("operationCode")]
        [DataMember]
        public int OperationCode { get; set; }

        /// <summary>
        /// 操作错误信息
        /// </summary>
        [JsonProperty("operationMessage")]
        [DataMember]
        public string OperationMessage { get; set; }
    }
}
