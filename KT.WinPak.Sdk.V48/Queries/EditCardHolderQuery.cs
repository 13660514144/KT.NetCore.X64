using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class EditCardHolderQuery
    {
        public int lCardholderID { get; set; }
        public CardHolder pCH { get; set; }
        public int pStatus { get; set; }
    }
}
