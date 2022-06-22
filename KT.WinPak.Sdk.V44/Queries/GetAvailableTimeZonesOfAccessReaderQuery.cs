using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class GetAvailableTimeZonesOfAccessReaderQuery
    {
        public string bstrAcctName { get; set; }
        public string bstrReaderName { get; set; }
        public object vTimezones { get; set; }
    }
}
