using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetTimeZoneRangesByTZIDQuery
    {
        public int lTimezoneID { get; set; }
        public object vTZRanges { get; set; }
    }
}
