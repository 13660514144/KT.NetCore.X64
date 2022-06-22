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
    /// 
    /// </summary>
    [DataContract]
    public class CardData
    {
        /// <summary>
        /// 卡片信息
        /// </summary>
        public CardData()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public CardData(sPA_Card source)
        {
            this.PersonId = source.sPersonID;
            this.CardNo = source.sCardNO;
            this.StatCode = source.sSTAT_COD;
            this.PinCode = source.sPINCODE;
            this.IssueDate = source.sISSUE_DATE;
            this.ExpireDate = source.sEXPIRE_DATE;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public sPA_Card ToSPA()
        {
            sPA_Card source = new sPA_Card();
            source.sPersonID = this.PersonId;
            source.sCardNO = this.CardNo;
            source.sSTAT_COD = this.StatCode;
            source.sPINCODE = this.PinCode;
            source.sISSUE_DATE = this.IssueDate;
            source.sEXPIRE_DATE = this.ExpireDate;

            return source;
        }

        #region 新增
        /// <summary>
        /// 公司Id
        /// </summary>
        [JsonProperty("companyId")]
        public string CompanyId { get; set; }

        #endregion


        /// <summary>
        /// 所属人员ID
        /// </summary>
        [JsonProperty("personId")]
        [DataMember]
        public string PersonId { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [JsonProperty("cardNo")]
        [DataMember]
        public string CardNo { get; set; }

        /// <summary>
        /// 状态码 A--Active, D--Disabled, L--Lost, S--Stolen, T--Terminated, U--Unaccounted, V--Void, X--Expired, O--AutoDisable
        /// </summary>
        [JsonProperty("statCode")]
        [DataMember]
        public string StatCode { get; set; }

        /// <summary>
        /// PIN码
        /// </summary>
        [JsonProperty("pinCode")]
        [DataMember]
        public string PinCode { get; set; }

        /// <summary>
        /// 卡片发行日期
        /// </summary>
        [JsonProperty("issueDate")]
        [DataMember]
        public string IssueDate { get; set; }

        /// <summary>
        /// 卡片失效日期
        /// </summary>
        [JsonProperty("expireDate")]
        [DataMember]
        public string ExpireDate { get; set; }

        /// <summary>
        /// 操作状态
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

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class CardArray
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<CardData> dataArray { get; set; }
    }
}
