using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace KT.WinPak.SDK.Models
{

    [XmlRootAttribute("NLZ", IsNullable = false)]
    public class EventModel
    {
        public string AckStatus { get; set; }
        public string Idx { get; set; }
        public string TranID { get; set; }
        public string CommSrvID { get; set; }
        public string DeviceID { get; set; }
        public string HID { get; set; }
        public string Prio { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Cnt { get; set; }
        public string Oper { get; set; }
        public string Note { get; set; }
        public string SS { get; set; }
        public string Status { get; set; }
        public string EventID { get; set; }
        public string HIDSubType1 { get; set; }
        public string Point { get; set; }
        public string Site { get; set; }
        public string Account { get; set; }
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public string RP { get; set; }


        //<NLZ>
        //    <AckStatus>1</AckStatus>
        //    <Idx>-1</Idx>
        //    <TranID> </TranID>
        //    <CommSrvID>1</CommSrvID>
        //    <DeviceID>4</DeviceID>
        //    <HID>12</HID>
        //    <Prio>79</Prio>
        //    <Date>01/17/2020</Date>
        //    <Time>16:49:00</Time>
        //    <Cnt>1</Cnt>
        //    <Oper></Oper>
        //    <Note>卡有效，允许进入。</Note>
        //    <SS></SS>
        //    <Status>有效卡</Status>
        //    <EventID>701</EventID>
        //    <HIDSubType1>55</HIDSubType1>
        //    <Point>0</Point>
        //    <Site>康塔大厦控制区域</Site>
        //    <Account>admin</Account>
        //    <CardNumber>223208642</CardNumber>
        //    <FullName>陈 钰贤</FullName>
        //    <RP>pro3000-1 - 读卡器 2</RP>
        // </NLZ>
    }
}
