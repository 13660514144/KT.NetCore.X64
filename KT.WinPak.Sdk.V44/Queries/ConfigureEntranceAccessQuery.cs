using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class ConfigureEntranceAccessQuery
    {
        public string bstrAcclName { get; set; }
        public string bstrReaderName { get; set; }
        public string bstrTZName { get; set; }
        public string bstrGroupName { get; set; }
        public int pStatus { get; set; }
    }
}
