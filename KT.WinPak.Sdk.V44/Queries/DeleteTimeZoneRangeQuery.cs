using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class DeleteTimeZoneRangeQuery
    {
        public int lTimezoneID { get; set; }
        public int lTZRangeID { get; set; }
        public int pStatus { get; set; }
    }
}
