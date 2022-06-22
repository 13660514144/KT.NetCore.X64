using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class IsolatePanelsForHGDeleteQuery
    {
        public int lAcctID { get; set; }
        public int lHolGrpID { get; set; }
        public object vPanels { get; set; }
        public int pStatus { get; set; }
    }
}
