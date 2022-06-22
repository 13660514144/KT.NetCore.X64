using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Prowatch.Service.Enums
{
    public class CardStateEnum : BaseEnum
    {
        public static CardStateEnum ENABLE { get; } = new CardStateEnum(1, "A", "启用");
        public static CardStateEnum DISABLE { get; } = new CardStateEnum(2, "D", "禁用");

        public CardStateEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        private static List<CardStateEnum> _items;
        public static List<CardStateEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<CardStateEnum>() {
                        ENABLE,
                        DISABLE
                    };
                }
                return _items;
            }
        }
    }
}
