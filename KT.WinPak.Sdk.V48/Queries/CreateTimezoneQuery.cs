using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Services
{
    public class CreateTimezoneQuery
    {
        public string bstrTZName { get; set; }
        public string bstrTZDesc { get; set; }
        public object vAcctlist { get; set; }
        public int pStatus { get; set; }
    }
}
