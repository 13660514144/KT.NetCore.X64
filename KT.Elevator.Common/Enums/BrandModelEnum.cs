using KT.Common.Core.Enums;
using System.Collections.Generic;

namespace KT.Elevator.Common.Enums
{
    public class BrandModelEnum : BaseEnum
    {
        public BrandModelEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static BrandModelEnum QSCS_R811 { get; } = new BrandModelEnum(1, "QSCS-R811", "康塔读卡器R811");
        public static BrandModelEnum QSCS_BXEN { get; } = new BrandModelEnum(2, "QSCS-BXEN", "康塔读卡器BXEN");
        public static BrandModelEnum QSCS_BX5N { get; } = new BrandModelEnum(3, "QSCS-BX5N", "康塔读卡器BX5N");
        public static BrandModelEnum R401_B_S { get; } = new BrandModelEnum(4, "R401-B-S", "R401-B-S");
        public static BrandModelEnum RF_RC730 { get; } = new BrandModelEnum(5, "RF-RC730", "RF-RC730");
        public static BrandModelEnum QSCS_BX5X { get; } = new BrandModelEnum(6, "QSCS-BX5X", "康塔读卡器BX5X");
        public static BrandModelEnum NLS_FM25_R { get; } = new BrandModelEnum(7, "NLS-FM25-R", "新大陆读卡器FM25-R");
        public static BrandModelEnum QSCS_DLS81 { get; } = new BrandModelEnum(8, "QSCS-DLS81", "康塔读卡器DLS81");
        public static BrandModelEnum HIKVISION_DS_K1T672MW { get; } = new BrandModelEnum(9, "HIKVISION-DS-K1T672MW", "海康人脸识别面板机7寸DS-K1T672MW");
        public static BrandModelEnum HIKVISION_DS_K5604Z_ZZH { get; } = new BrandModelEnum(10, "HIKVISION-DS-K5604Z-ZZH", "海康人脸识别面板机10寸K5604Z-ZZH");
        public static BrandModelEnum HIKVISION_DS_K2210 { get; } = new BrandModelEnum(11, "HIKVISION-DS-K2210", "海康梯控主机DS-K2210");
        public static BrandModelEnum QSCS_3600P { get; } = new BrandModelEnum(12, "QSCS-3600P", "康塔边缘处理器3600P");

        private static readonly object _locker = new object();
        private static List<BrandModelEnum> _items;
        public static List<BrandModelEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<BrandModelEnum>()
                            {
                                QSCS_R811   ,
                                QSCS_BXEN   ,
                                QSCS_BX5N   ,
                                R401_B_S    ,
                                RF_RC730    ,
                                QSCS_BX5X   ,
                                NLS_FM25_R,
                                QSCS_DLS81,
                                HIKVISION_DS_K1T672MW,
                                HIKVISION_DS_K5604Z_ZZH,
                                HIKVISION_DS_K2210,
                                QSCS_3600P
                            };
                        }
                    }
                }
                return _items;
            }
        }
    }
}
