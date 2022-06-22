using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetAccessAreaBranchesByAccountNameQuery
    {
        public string bstrAcctName { get; set; }
        public object vBranches { get; set; }
    }
}
