using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class EditTimeZoneQuery
    {
        public string bstrExistTimeZoneName { get; set; }
        public string bstrAccountName { get; set; }
        public NCIHelperLib.TimeZone pTimeZone { get; set; }
        public int pStatus { get; set; }
    }
}
