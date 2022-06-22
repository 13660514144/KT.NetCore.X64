using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class ReassignAccessLevelTZQuery
    {
        public int lAcctID { get; set; }
        public int lOldTimeZoneID { get; set; }
        public int lNewTimeZoneID { get; set; }
        public object vAccessLevels { get; set; }
        public int pStatus { get; set; }
    }
}
