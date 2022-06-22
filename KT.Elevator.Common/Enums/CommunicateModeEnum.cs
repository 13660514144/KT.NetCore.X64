using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Common.Enums
{
    /// <summary>
    /// 通信方式
    /// </summary>
    public class CommunicateModeEnum : BaseEnum
    {
        public CommunicateModeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static CommunicateModeEnum TCP { get; } = new CommunicateModeEnum(1, "TCP", "TCP通信");
        public static CommunicateModeEnum UDP { get; } = new CommunicateModeEnum(2, "UDP", "UDP通信");
        public static CommunicateModeEnum SERIAL_PORT { get; } = new CommunicateModeEnum(3, "SERIAL_PORT", "串口通信");
        public static CommunicateModeEnum COM_COMPONENT { get; } = new CommunicateModeEnum(4, "COM_COMPONENT", "COM组件");
        public static CommunicateModeEnum SIGNAL_R { get; } = new CommunicateModeEnum(5, "SIGNAL_R", "SignalR通信");
        public static CommunicateModeEnum WEB_SOCKET { get; } = new CommunicateModeEnum(6, "WEB_SOCKET", "WebSocket通信");

        private static readonly object _locker = new object();
        private static List<CommunicateModeEnum> _items;
        public static List<CommunicateModeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<CommunicateModeEnum>()
                            {
                                TCP,
                                UDP ,
                                SERIAL_PORT ,
                                COM_COMPONENT ,
                                SIGNAL_R ,
                                WEB_SOCKET
                            };
                        }
                    }
                }
                return _items;
            }
        }
    }
}
