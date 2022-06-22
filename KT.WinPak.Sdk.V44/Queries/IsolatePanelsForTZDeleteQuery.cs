using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class IsolatePanelsForTZDeleteQuery
    {
        public int lAcctID { get; set; }
        public int lTimezoneID { get; set; }
        public object vPanels { get; set; }
        public int pStatus { get; set; }
    }
}
