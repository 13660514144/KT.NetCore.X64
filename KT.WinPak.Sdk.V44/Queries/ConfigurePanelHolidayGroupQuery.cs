using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class ConfigurePanelHolidayGroupQuery
    {
        public int lAcctID { get; set; }
        public int lPanelID { get; set; }
        public object vHolidayGroupIDs { get; set; }
        public int pStatus { get; set; }
    }
}
