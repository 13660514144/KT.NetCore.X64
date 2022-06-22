using System;
using System.Runtime.InteropServices;

namespace KT.Quanta.Service.Devices.Hikvision.Models
{
    public partial class AcsDemoPublic
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct STRU_CHANNEL_INFO
        {
            public int iDeviceIndex;  	//device index
            public int iChanIndex;  	//channel index
            public DEMO_CHANNEL_TYPE iChanType;
            public int iChannelNO;         //channel NO.       
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chChanName;         //channel name
            public uint dwProtocol;  	//network protocol
            public uint dwStreamType; //stream type，0-main stream，1-sub code stream，2-code stream 3，
            public uint dwLinkMode;//Stream connection: 0：TCP,1：UDP,2：multicast,3 - RTP，4-RTP/RTSP,5-RSTP/HTTP
            public bool bPassbackRecord; //0- disable the video comes back,1 enable the video comes back
            public uint dwPreviewMode;  //Preview mode 0-normal 1-delay 
            public int iPicResolution;    //resolution
            public int iPicQuality;    //image quality
            public Int32 lRealHandle;          //preview handle
            public bool bLocalManualRec;     //manual record
            public bool bAlarm;    //alarm
            public bool bEnable;  	//enable
            public uint dwImageType;  //channel status icon
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string chAccessChanIP;   //ip addr of IP channel
            public uint nPreviewProtocolType;	//stream protocol type 0-proprietary protocol 1-RTSP protocol
            public IntPtr pNext;

            public void init()
            {
                iDeviceIndex = -1;
                iChanIndex = -1;
                iChannelNO = -1;
                iChanType = DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_INVALID;
                chChanName = null;
                dwProtocol = 0;

                dwStreamType = 0;
                dwLinkMode = 0;

                iPicResolution = 0;
                iPicQuality = 2;

                lRealHandle = -1;
                bLocalManualRec = false;
                bAlarm = false;
                bEnable = false;
                dwImageType = CHAN_ORIGINAL;
                chAccessChanIP = null;
                pNext = IntPtr.Zero;
                dwPreviewMode = 0;
                bPassbackRecord = false;
                nPreviewProtocolType = 0;
            }
        }
    }

}