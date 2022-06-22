using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class ReassignCardTZQuery
    {
        public int lAcctID { get; set; }
        public int lOldTimeZoneID { get; set; }
        public object vCards { get; set; }
        public int lNewTimeZoneID { get; set; }
        public int pStatus { get; set; }
    }
}
