using System;
using System.Collections.Generic;
using System.Text;

namespace HelperTools
{
  
    public class ReqElvator
    {
        public string Ip { get; set; }
        public string Sign { get; set; }
        public string Port { get; set; }
        public string CardId { get; set; } = string.Empty;
        public string PassFlg { get; set; } = "1";
        public string AccessType { get; set; }
    }
}
