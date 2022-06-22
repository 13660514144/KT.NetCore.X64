using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetTZsForReassignQuery
    {
        public int lTimezoneID { get; set; }
        public int lExistTimeZoneID { get; set; }
        public object vTimezones { get; set; }
    }
}
