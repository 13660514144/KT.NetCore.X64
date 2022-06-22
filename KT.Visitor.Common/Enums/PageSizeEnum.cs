using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Common.Enums
{
    public class PageSizeEnum : BaseEnum
    {
        public static PageSizeEnum ONE_SIZE { get; } = new PageSizeEnum(20, "ONE_SIZE", "20条/页");
        public static PageSizeEnum TWO_SIZE { get; } = new PageSizeEnum(30, "TWO_SIZE", "30条/页");
        public static PageSizeEnum THREE_SIZE { get; } = new PageSizeEnum(50, "THREE_SIZE", "50条/页");

        public PageSizeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        private static object _locker = new object();
        private static List<PageSizeEnum> _items;
        public static List<PageSizeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<PageSizeEnum>() {
                                ONE_SIZE,
                                TWO_SIZE,
                                THREE_SIZE
                            };
                        }
                    }
                }
                return _items;
            }
        }
    }
}