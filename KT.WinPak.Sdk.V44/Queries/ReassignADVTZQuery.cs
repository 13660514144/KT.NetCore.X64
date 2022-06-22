using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class ReassignADVTZQuery
    {
        public int lAcctID { get; set; }
        public int lOldTimeZoneID { get; set; }
        public int lNewTimeZoneID { get; set; }
        public object vADVs { get; set; }
        public int pStatus { get; set; }
    }
}
