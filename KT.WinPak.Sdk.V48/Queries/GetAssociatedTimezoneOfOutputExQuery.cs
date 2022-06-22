using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Services
{
    public class GetAssociatedTimezoneOfOutputExQuery
    {
        public int lAcctID { get; set; }
        public int lPanelID { get; set; }
        public int lOutputID { get; set; }
        public int iLockUnlock { get; set; }
        public object vTimezone { get; set; }
    }
}
