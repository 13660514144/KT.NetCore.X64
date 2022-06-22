using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetAccessLevelByNameQuery
    {
        public IAccessLevel VAccessLevel { get; set; }

        public string bstrAcclName { get; set; }
        public string bstrAcctName { get; set; }
    }
}
