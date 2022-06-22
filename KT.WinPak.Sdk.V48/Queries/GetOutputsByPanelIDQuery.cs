using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetOutputsByPanelIDQuery
    {
        public int lAcctID { get; set; }
        public int lPanelID { get; set; }
        public object vOutputs { get; set; }
    }
}
