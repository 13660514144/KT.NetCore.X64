using System.Runtime.InteropServices;

namespace KT.Elevator.Manage.Service.Devices.Hikvision.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LOCAL_LOG_INFO
    {
        public int iLogType;
        public string strTime;
        public string strLogInfo;
        public string strDevInfo;
        public string strErrInfo;
        public string strSavePath;
    }

}