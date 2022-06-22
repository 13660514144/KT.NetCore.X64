using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class ReassignActionGroupTZQuery
    {
        public int lNewTimeZoneID { get; set; }
        public int lAcctID { get; set; }
        public int lOldTimeZoneID { get; set; }
        public object vActionGroups { get; set; }
        public int pStatus { get; set; }
    }
}
