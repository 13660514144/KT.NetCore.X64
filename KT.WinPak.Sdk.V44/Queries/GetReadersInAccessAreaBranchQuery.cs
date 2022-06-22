using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetReadersInAccessAreaBranchQuery
    {
        public string bstrAcctName { get; set; }
        public string bstrBranchName { get; set; }
        public object vReaders { get; set; }
    }
}
