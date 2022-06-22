using System;
using System.Runtime.InteropServices;

namespace KT.Quanta.Service.Devices.Hikvision.Models
{
    public partial class AcsDemoPublic
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct PREVIEW_IFNO
        {
            public int iDeviceIndex;  	//device index
            public int iChanIndex;  	//channel index
            public byte PanelNo;
            public int lRealHandle;
            public IntPtr hPlayWnd;
        }
    }

}