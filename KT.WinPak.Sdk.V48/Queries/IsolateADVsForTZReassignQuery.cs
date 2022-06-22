using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class IsolateADVsForTZReassignQuery
    {
        public int lTimezoneID { get; set; }
        public int lAcctID { get; set; }
        public object vADVs { get; set; }
        public int pStatus { get; set; }
    }
}
