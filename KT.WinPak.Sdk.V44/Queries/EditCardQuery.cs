using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class EditCardQuery
    {
        public string bstrCardno { get; set; }
        public string bstrAcctName { get; set; }
        public Card pcard { get; set; }
        public int pStatus { get; set; }
    }
}
