using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Services
{
    public class GetCardHolderSearchFieldsByAccountNameQuery
    {
        public string bstrAcctName { get; set; }
        public string bstrSubAcctName { get; set; }
        public object vCHSearchFields { get; set; }
    }
}
