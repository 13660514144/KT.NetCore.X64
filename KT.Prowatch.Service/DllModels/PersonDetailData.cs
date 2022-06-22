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
    /// 人员详细信息
    /// </summary>
    [DataContract]
    public class PersonDetailData
    {
        /// <summary>
        /// 
        /// </summary>
        public PersonDetailData()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public PersonDetailData(sPA_PersonDetail source)
        {
            this.Id = source.sID;
            this.BirthDate = source.sBIRTHDATE;
            this.BadgeNumber = source.sBADGENUMBER;
            this.Address1 = source.sADDRESS1;
            this.Address2 = source.sADDRESS2;
            this.City = source.sCITY;
            this.State = source.sSTATE;
            this.Zip = source.sZIP;
            this.Country = source.sCOUNTRY;
            this.SuperVisor = source.sSUPERVISOR;
            this.Department = source.sDEPARTMENT;
            this.Title = source.sTITLE;
            this.Building = source.sBUILDING;
            this.Floor = source.sFLOOR;
            this.HomePhone = source.sHOMEPHONE;
            this.OfficePhone = source.sOFFICEPHONE;
            this.Extension = source.sEXTENSION;
            this.EmerAddress1 = source.sEMERADDRESS1;
            this.EmerAddress2 = source.sEMERADDRESS2;
        }


        internal sPA_PersonDetail ToSPA(   )
        {
            sPA_PersonDetail source = new sPA_PersonDetail();
            source.sID = this.Id;
            source.sBIRTHDATE = this.BirthDate;
            source.sBADGENUMBER = this.BadgeNumber;
            source.sADDRESS1 = this.Address1;
            source.sADDRESS2 = this.Address2;
            source.sCITY = this.City;
            source.sSTATE = this.State;
            source.sZIP = this.Zip;
            source.sCOUNTRY = this.Country;
            source.sSUPERVISOR = this.SuperVisor;
            source.sDEPARTMENT = this.Department;
            source.sTITLE = this.Title;
            source.sBUILDING = this.Building;
            source.sFLOOR = this.Floor;
            source.sHOMEPHONE = this.HomePhone;
            source.sOFFICEPHONE = this.OfficePhone;
            source.sEXTENSION = this.Extension;
            source.sEMERADDRESS1 = this.EmerAddress1;
            source.sEMERADDRESS2 = this.EmerAddress2;
            return source;
        }
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [JsonProperty("birthDate")]
        [DataMember]
        public string BirthDate { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [JsonProperty("badgeNumber")]
        [DataMember]
        public string BadgeNumber { get; set; }

        /// <summary>
        /// 地址1
        /// </summary>
        [JsonProperty("address1")]
        [DataMember]
        public string Address1 { get; set; }

        /// <summary>
        /// 地址2
        /// </summary>
        [JsonProperty("address2")]
        [DataMember]
        public string Address2 { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [JsonProperty("city")]
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// 州/省
        /// </summary>
        [JsonProperty("state")]
        [DataMember]
        public string State { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [JsonProperty("zip")]
        [DataMember]
        public string Zip { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [JsonProperty("country")]
        [DataMember]
        public string Country { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        [JsonProperty("superVisor")]
        [DataMember]
        public string SuperVisor { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [JsonProperty("department")]
        [DataMember]
        public string Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [JsonProperty("title")]
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// 楼
        /// </summary>
        [JsonProperty("building")]
        [DataMember]
        public string Building { get; set; }

        /// <summary>
        /// 层
        /// </summary>
        [JsonProperty("floor")]
        [DataMember]
        public string Floor { get; set; }

        /// <summary>
        /// 家庭电话
        /// </summary>
        [JsonProperty("homePhone")]
        [DataMember]
        public string HomePhone { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        [JsonProperty("officePhone")]
        [DataMember]
        public string OfficePhone { get; set; }

        /// <summary>
        /// 扩展信息（用作证件类型）
        /// </summary>
        [JsonProperty("extension")]
        [DataMember]
        public string Extension { get; set; }

        /// <summary>
        /// 紧急联系方式（用作证件号码）
        /// </summary>
        [JsonProperty("emerContact")]
        [DataMember]
        public string EmerContact { get; set; }

        /// <summary>
        /// 紧急地址1（用作邮箱）
        /// </summary>
        [JsonProperty("emerAddress1")]
        [DataMember]
        public string EmerAddress1 { get; set; }

        /// <summary>
        /// 紧急地址2（用作图片信息）
        /// </summary>
        [JsonProperty("emerAddress2")]
        [DataMember]
        public string EmerAddress2 { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class PersonDetailArray
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<PersonDetailData> dataArray { get; set; }
    }
}
