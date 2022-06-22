using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class ReassignAccessLevelQuery
    {
        public string bstrAcctName { get; set; }
        public string bstrExAcclName { get; set; }
        public string bstrNewAcclName { get; set; }
        public object vCards { get; set; }
        public int pStatus { get; set; }
    }
}
