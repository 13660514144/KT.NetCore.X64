using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class CreateAccessLevelQuery
    {
        public string bstrAcclName { get; set; }
        public string bstrAcclDesc { get; set; }
        public object vAcctlist { get; set; }
        public int pStatus { get; set; }
    }
}
