using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class GetDevNameByDeviceIDQuery
    {
        public int lDeviceID { get; set; }
        public string pBstrDeviceName { get; set; }
    }
}
