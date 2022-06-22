using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class DeleteCardQuery
    {
        public string bstrAcctName { get; set; }
        public string bstrCardno { get; set; }
        public int pStatus { get; set; }
    }
}
