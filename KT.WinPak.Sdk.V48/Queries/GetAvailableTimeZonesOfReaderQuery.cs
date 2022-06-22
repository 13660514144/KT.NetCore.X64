using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetAvailableTimeZonesOfReaderQuery
    {
        public string bstrReaderName { get; set; }
        public object vTimezones { get; set; }
    }
}
