using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class BulkAddCardsQuery
    {
        public object sStartNo { get; set; }
        public object sStopNo { get; set; }
        public int lAccountID { get; set; }
        public int lCardStatus { get; set; }
        public DateTime dtActivationDate { get; set; }
        public DateTime dtExpirationDate { get; set; }
        public int lOperID { get; set; }
        public object sOpName { get; set; }
        public object alAccessLevelIDs { get; set; }
        public int bMultiple { get; set; }
    }
}
