using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class GetDirectPointTZDetailsofReaderQuery
    {
        public int lReaderID { get; set; }
        public int lDeviceID { get; set; }
        public int lTZID { get; set; }
    }
}
