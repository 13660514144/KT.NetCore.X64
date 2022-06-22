using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class IsolateActionGroupsForTZReassignQuery
    {
        public int lTimezoneID { get; set; }
        public int lAcctID { get; set; }
        public object vActionGroups { get; set; }
        public int pStatus { get; set; }
    }
}
