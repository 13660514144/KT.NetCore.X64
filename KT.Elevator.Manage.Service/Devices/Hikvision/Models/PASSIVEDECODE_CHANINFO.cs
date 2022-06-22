using System;
using System.Runtime.InteropServices;

namespace KT.Elevator.Manage.Service.Devices.Hikvision.Models
{
    public partial class AcsDemoPublic
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct PASSIVEDECODE_CHANINFO
        {
            public Int32 lPassiveHandle;
            public Int32 lRealHandle;
            public Int32 lUserID;
            public Int32 lSel;
            public IntPtr hFileThread;
            public IntPtr hFileHandle;
            public IntPtr hExitThread;
            public IntPtr hThreadExit;
            public string strRecordFilePath;

            public void init()
            {
                lPassiveHandle = -1;
                lRealHandle = -1;
                lUserID = -1;
                lSel = -1;
                hFileThread = IntPtr.Zero;
                hFileHandle = IntPtr.Zero;
                hExitThread = IntPtr.Zero;
                hThreadExit = IntPtr.Zero;
                strRecordFilePath = null;
            }
        }
    }

}