using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class DeletePanelTZQuery
    {
        public int lOldTimeZoneID { get; set; }
        public int lAcctID { get; set; }
        public object vPanels { get; set; }
        public int pStatus { get; set; }
    }
}
