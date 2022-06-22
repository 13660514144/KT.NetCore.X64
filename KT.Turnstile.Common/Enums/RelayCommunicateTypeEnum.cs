using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Common.Enums
{
    public class RelayCommunicateTypeEnum : BaseEnum
    {
        public RelayCommunicateTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static RelayCommunicateTypeEnum TCP { get; } = new RelayCommunicateTypeEnum(1, "TCP", "TCP通信");
        public static RelayCommunicateTypeEnum UDP { get; } = new RelayCommunicateTypeEnum(2, "UDP", "UDP通信");


        private static readonly object _locker = new object();
        private static List<RelayCommunicateTypeEnum> _items;
        public static List<RelayCommunicateTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<RelayCommunicateTypeEnum>()
                            {
                                TCP,
                                UDP
                            };
                        }
                    }
                }
                return _items;
            }
        }
    }
}
