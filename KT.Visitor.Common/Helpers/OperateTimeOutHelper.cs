using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.SelfApp.Helpers
{
    /// <summary>
    /// 检测 窗体超过若干秒无操作 获取键盘鼠标最后的操作时间
    /// </summary>
    public static class OperateTimeOutHelper
    {
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        /// <summary>
        /// 获取最后操作时间差
        /// </summary>
        /// <returns></returns>
        public static long GetIdleTick()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = Marshal.SizeOf(lastInputInfo);
            if (!GetLastInputInfo(ref lastInputInfo))
            {
                return 0;
            }
            return Environment.TickCount - (long)lastInputInfo.dwTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }
    }
}
