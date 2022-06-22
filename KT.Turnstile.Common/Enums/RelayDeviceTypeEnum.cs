using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Common.Enums
{
    public class RelayDeviceTypeEnum : BaseEnum
    {
        public RelayDeviceTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static RelayDeviceTypeEnum DEFAULT { get; } = new RelayDeviceTypeEnum(1, "DEFAULT", "默认");

        private static readonly object _locker = new object();
        private static List<RelayDeviceTypeEnum> _items;
        public static List<RelayDeviceTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<RelayDeviceTypeEnum>()
                            {
                                DEFAULT
                            };
                        }
                    }
                }
                return _items;
            }
        }
    }
}
