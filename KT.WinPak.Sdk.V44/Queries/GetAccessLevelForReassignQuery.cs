using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetAccessLevelForReassignQuery
    {
        public string bstrAccountName { get; set; }
        public string bstrExAcclName { get; set; }
        public object vAccessLevels { get; set; }
    }
}
