using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class ConfigureGroupTimezoneQuery
    {
        public int lAcctID { get; set; }
        public int lPanelID { get; set; }
        public int lGroupID { get; set; }
        public int lTimezoneID { get; set; }
        public int pStatus { get; set; }
    }
}
