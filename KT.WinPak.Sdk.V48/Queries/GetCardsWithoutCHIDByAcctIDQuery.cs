using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetCardsWithoutCHIDByAcctIDQuery
    {
        public int lAcctID { get; set; }
        public int lSubAcctID { get; set; }
        public object vCards { get; set; }
    }
}
