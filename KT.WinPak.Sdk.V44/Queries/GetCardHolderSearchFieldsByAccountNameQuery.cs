using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class GetCardHolderSearchFieldsByAccountNameQuery
    {
        public string bstrAcctName { get; set; }
        public object vCHSearchFields { get; set; }
    }
}
