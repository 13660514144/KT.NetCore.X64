using KT.Common.Core.Enums;
using System.Collections.Generic;

namespace KT.Turnstile.Common.Enums
{
    public class CardDeviceTypeEnum : BaseEnum
    {
        public CardDeviceTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static CardDeviceTypeEnum QSCS_R811 { get; } = new CardDeviceTypeEnum(1, "QSCS-R811", "QSCS-R811");
        public static CardDeviceTypeEnum QSCS_BXEN { get; } = new CardDeviceTypeEnum(2, "QSCS-BXEN", "QSCS-BXEN");
        public static CardDeviceTypeEnum QSCS_BX5N { get; } = new CardDeviceTypeEnum(3, "QSCS-BX5N", "QSCS-BX5N");
        public static CardDeviceTypeEnum R401_B_S { get; } = new CardDeviceTypeEnum(4, "R401-B-S", "R401-B-S");
        public static CardDeviceTypeEnum RF_RC730 { get; } = new CardDeviceTypeEnum(5, "RF-RC730", "RF-RC730");
        public static CardDeviceTypeEnum QSCS_BX5X { get; } = new CardDeviceTypeEnum(6, "QSCS-BX5X", "QSCS-BX5X");
        public static CardDeviceTypeEnum NLS_FM25_R { get; } = new CardDeviceTypeEnum(7, "NLS-FM25-R", "NLS-FM25-R");

        private static readonly object _locker = new object();
        private static List<CardDeviceTypeEnum> _items;
        public static List<CardDeviceTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<CardDeviceTypeEnum>()
                            {
                                QSCS_R811   ,
                                QSCS_BXEN   ,
                                QSCS_BX5N   ,
                                R401_B_S    ,
                                RF_RC730    ,
                                QSCS_BX5X   ,
                                NLS_FM25_R
                            };
                        }
                    }
                }
                return _items;
            }
        }
    }
}
