using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class GetDeviceNameByHWDeviceIDQuery
    {
        public int lHWDeviceID { get; set; }
        public string pBstrDeviceName { get; set; }
    }
}
