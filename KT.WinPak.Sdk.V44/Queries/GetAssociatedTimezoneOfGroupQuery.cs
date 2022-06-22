using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetAssociatedTimezoneOfGroupQuery
    {
        public int lAcctID { get; set; }
        public int lPanelID { get; set; }
        public int lGroupID { get; set; }
        public object vTimezone { get; set; }
    }
}
