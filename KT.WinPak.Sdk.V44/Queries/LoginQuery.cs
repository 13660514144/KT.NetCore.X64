using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class LoginQuery
    {
        public string bstrUserName { get; set; }
        public string bstrPassword { get; set; }
        public string bstrDomainName { get; set; }
        public int plUserID { get; set; }
    }
}
