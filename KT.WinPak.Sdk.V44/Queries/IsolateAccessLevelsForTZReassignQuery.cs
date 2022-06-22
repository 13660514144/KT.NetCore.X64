using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class IsolateAccessLevelsForTZReassignQuery
    {
        public int lTimezoneID { get; set; }
        public int lAcctID { get; set; }
        public object vAccessLevels { get; set; }
        public int pStatus { get; set; }
    }
}
