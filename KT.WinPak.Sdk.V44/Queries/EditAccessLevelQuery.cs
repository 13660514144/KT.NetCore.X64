using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class EditAccessLevelQuery
    {
        public string bstrAccl { get; set; }
        public AccessLevel pAccesslevel { get; set; }
        public int pStatus { get; set; }
    }
}
