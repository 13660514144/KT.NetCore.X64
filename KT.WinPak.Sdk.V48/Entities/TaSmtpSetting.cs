using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaSmtpSetting
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FromAddress { get; set; }
        public string HostName { get; set; }
        public int PortNo { get; set; }
        public int Timeout { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public bool EnableSsl { get; set; }
    }
}
