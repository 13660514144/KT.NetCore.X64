using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace KT.Elevator.Manage.Service.Devices.Hikvision.Models
{
    public class ISAPIRemoteConfig
    {
        public ISAPIRemoteConfig(int UserID, int commandCode, string url)
        {
            this.url = url;
            this.cmd = commandCode;
            this.remoteConfigHandle = -1;
            this.listener = null;
            this.lUserID = UserID;
        }
        public ISAPIRemoteConfig(int UserID, int commandCode, string url, CHCNetSDK.RemoteConfigCallback callback)
        {
            this.url = url;
            this.cmd = commandCode;
            this.listener = callback;
            this.remoteConfigHandle = -1;
            this.lUserID = UserID;
        }

        public void SetListener(CHCNetSDK.RemoteConfigCallback callback)
        {
            this.listener = callback;
        }

        public bool Start()
        {
            Debug.Assert(remoteConfigHandle == -1);
            if (remoteConfigHandle == -1)
            {
                Debug.Assert(url != null);
                IntPtr ptrURL = Marshal.StringToHGlobalAnsi(url);

                remoteConfigHandle = CHCNetSDK.NET_DVR_StartRemoteConfig(lUserID, (uint)cmd, ptrURL, url.Length, listener, IntPtr.Zero);
                Marshal.FreeHGlobal(ptrURL);
                if (-1 == remoteConfigHandle)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public void Stop()
        {
            if (remoteConfigHandle != -1)
            {
                CHCNetSDK.NET_DVR_StopRemoteConfig(remoteConfigHandle);
                remoteConfigHandle = -1;
            }
        }
        public bool Send(string input)
        {
            Debug.Assert(input != null);
            Debug.Assert(remoteConfigHandle != -1);

            Encoding utf8 = null;
            int inputlen = 0;
            try
            {
                utf8 = System.Text.Encoding.GetEncoding("utf-8");//转换编码
                inputlen = utf8.GetByteCount(input);
            }
            catch
            {
                // do something
            }

            IntPtr ptrData = Marshal.AllocHGlobal(inputlen + 1);
            for (int i = 0; i < inputlen + 1; i++)
            {
                Marshal.WriteByte(ptrData, i, 0);
            }

            Marshal.Copy(utf8.GetBytes(input), 0, ptrData, inputlen);

            if (!CHCNetSDK.NET_DVR_SendRemoteConfig(remoteConfigHandle, (int)CHCNetSDK.LONG_CFG_SEND_DATA_TYPE_ENUM.ENUM_SEND_JSON_DATA, ptrData, (uint)inputlen))
            {
                Marshal.FreeHGlobal(ptrData);
                return false;
            }
            Marshal.FreeHGlobal(ptrData);
            return true;
        }

        private CHCNetSDK.RemoteConfigCallback listener;
        private string url;
        private int cmd;
        public int remoteConfigHandle;
        private int lUserID;
    }

}