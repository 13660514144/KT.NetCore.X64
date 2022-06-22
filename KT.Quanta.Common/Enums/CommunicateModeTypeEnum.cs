using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Common.Enums
{
    /// <summary>
    /// 通信方式
    /// </summary>
    public class CommunicateModeTypeEnum : BaseEnum
    {
        public CommunicateModeTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static CommunicateModeTypeEnum TCP { get; } = new CommunicateModeTypeEnum(1, "TCP", "TCP通信");
        public static CommunicateModeTypeEnum UDP { get; } = new CommunicateModeTypeEnum(2, "UDP", "UDP通信");
        public static CommunicateModeTypeEnum SERIAL_PORT { get; } = new CommunicateModeTypeEnum(3, "SERIAL_PORT", "串口通信");
        public static CommunicateModeTypeEnum COM_COMPONENT { get; } = new CommunicateModeTypeEnum(4, "COM_COMPONENT", "COM组件");
        public static CommunicateModeTypeEnum WEB_SOCKET { get; } = new CommunicateModeTypeEnum(6, "WEB_SOCKET", "WebSocket通信");
        public static CommunicateModeTypeEnum SIGNAL_R { get; } = new CommunicateModeTypeEnum(7, "SIGNAL_R", "SignalR通信");
        public static CommunicateModeTypeEnum SDK { get; } = new CommunicateModeTypeEnum(8, "SDK", "SDK开发包");
    }
}
