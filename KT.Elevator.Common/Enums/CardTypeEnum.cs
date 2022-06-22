using KT.Common.Core.Enums;
using System.Collections.Generic;

namespace KT.Elevator.Common.Enums
{
    public class CardTypeEnum : BaseEnum
    {
        public CardTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static CardTypeEnum FACE { get; } = new CardTypeEnum(1, "FACE", "人脸");
        public static CardTypeEnum IC_CARD { get; } = new CardTypeEnum(2, "IC_CARD", "IC卡");
        public static CardTypeEnum QR_CODE { get; } = new CardTypeEnum(3, "QR_CODE", "二维码");

        private static readonly object _locker = new object();
        private static List<CardTypeEnum> _items;
        public static List<CardTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<CardTypeEnum>()
                            {
                                FACE,
                                IC_CARD,
                                QR_CODE
                            };
                        }
                    }
                }
                return _items;
            }
        }
    }
}
