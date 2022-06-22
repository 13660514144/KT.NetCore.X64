using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetAssociatedTimezoneOfOutputQuery
    {
        public int lAcctID { get; set; }
        public int lPanelID { get; set; }
        public int lOutputID { get; set; }
        public object vTimezone { get; set; }
    }
}
