using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class ConnectWPDatabaseQuery
    {
        public string bstrUserName { get; set; }
        public string bstrPassword { get; set; }
        public string bstrDomainName { get; set; }
        public int lUserID { get; set; }
        public int pStatus { get; set; }
    }
}
