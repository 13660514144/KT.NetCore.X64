using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Services
{
    public class GetAccessTreeByNameQuery
    {
        public string bstrAcclName { get; set; }
        public string bstrAcctName { get; set; }
        public string pbstrAccessTree { get; set; }
    }
}
