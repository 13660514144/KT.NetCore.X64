using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class ConfigureTimeZoneRangeQuery
    {
        public int lAcctID { get; set; }
        public int lTimezoneID { get; set; }
        public object vTZs { get; set; }
        public int pStatus { get; set; }
    }
}
