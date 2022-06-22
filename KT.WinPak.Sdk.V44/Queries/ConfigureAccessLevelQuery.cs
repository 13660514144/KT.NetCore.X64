using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class ConfigureAccessLevelQuery
    {
        public string bstrAcclName { get; set; }
        public object vReaders { get; set; }
        public string bstrTZName { get; set; }
        public int pStatus { get; set; }
    }
}
