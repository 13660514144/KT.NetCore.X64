using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class AddUpdateCardQuery
    {
        public int dwRecordID { get; set; }
        public object sCardNo { get; set; }
        public int lAccountID { get; set; }
        public int lCardStatus { get; set; }
        public int lissue { get; set; }
        public int lCardholderID { get; set; }
        public object PIN1 { get; set; }
        public DateTime dtActivationDate { get; set; }
        public DateTime dtExpirationDate { get; set; }
        public int Backdrop1ID { get; set; }
        public int Backdrop2ID { get; set; }
        public int bMultiple { get; set; }
        public object alAccessLevelIDs { get; set; }
        public bool bTempCard { get; set; }
        public short iNXCardType { get; set; }
        public short nUsageLimits { get; set; }
        public bool bLimitedCard { get; set; }
    }
}
