using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetGroupsByPanelIDQuery
    {
        public int lAcctID { get; set; }
        public int lPanelID { get; set; }
        public object vGroups { get; set; }
    }
}
