using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetCardByCardNumberQuery
    {
        public string bstrCardno { get; set; }
        public string bstrAcctName { get; set; }
        public ICard vcard { get; set; }
    }
}
