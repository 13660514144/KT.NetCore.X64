using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    /// <summary>
    /// 心跳
    /// 
    /// The primary key for a user record in the PORT Technology database is different to the primary key for a user record in the third-party
    /// database. The "user number" was introduced in the External Call Manager, reflecting the primary key for the third-party's user
    /// record.This allows a third-party interface to submit calls and user data to the PORT Technology system using its own primary key.
    /// When such a "user number" is present, the live report system includes an additional attribute called individualID in the allocation
    /// message.The individualID reflects the primary key used by the external system.
    /// <Allocation date = "2020-09-24" time="10:37:20" start="1" dest="4" ID="11" individualID="0" firstname="First Name" lastname="Family Name" personalNo="0031312"/>
    /// </summary>
    [XmlRoot("Allocation")]
    public class SchindlerReportAllocationResponse
    {
        /// <summary>
        /// 日期
        /// </summary>
        [XmlAttribute("date")]
        public string Date { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [XmlAttribute("time")]
        public string Time { get; set; }

        /// <summary>
        /// 目地楼层
        /// </summary>
        [XmlAttribute("start")]
        public string SourceFloor { get; set; }

        /// <summary>
        /// 目地楼层
        /// </summary>
        [XmlAttribute("dest")]
        public string DestinationFloor { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [XmlAttribute("ID")]
        public string UserId { get; set; }

        /// <summary>
        /// 外部系统id
        /// </summary>
        [XmlAttribute("individualID")]
        public string IndividualId { get; set; }

        /// <summary>
        /// 姓氏
        /// </summary>
        [XmlAttribute("firstname")]
        public string FirstName { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        [XmlAttribute("lastname")]
        public string LastName { get; set; }

        /// <summary>
        /// 人员id
        /// </summary>
        [XmlAttribute("personalNo")]
        public string PersonId { get; set; }


    }
}
