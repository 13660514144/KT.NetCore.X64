using KT.Common.Core.Enums;
using System.Collections.Generic;

namespace KT.Quanta.Common.Enums
{
    /// <summary>
    /// 设备型号
    /// </summary>
    public class BrandModelEnum : BaseEnum
    {
        public BrandModelEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static BrandModelEnum QSCS_R811 => new BrandModelEnum(1, "QSCS-R811", "康塔读卡器QSCS-R811");
        public static BrandModelEnum QSCS_BXEN => new BrandModelEnum(2, "QSCS-BXEN", "康塔读卡器QSCS-BXEN");
        public static BrandModelEnum QSCS_BX5N => new BrandModelEnum(3, "QSCS-BX5N", "康塔读卡器QSCS-BX5N");
        public static BrandModelEnum QSCS_BX5X => new BrandModelEnum(6, "QSCS-BX5X", "康塔读卡器QSCS-BX5X");
        public static BrandModelEnum NLS_FM25_R => new BrandModelEnum(7, "NLS-FM25-R", "新大陆读卡器FM25-R");
        public static BrandModelEnum QIACS_QT660_R => new BrandModelEnum(22, "QIACS-QT660-R", "读卡器QIACS-QT660-R");
        public static BrandModelEnum QIACS_R824 => new BrandModelEnum(25, "QIACS-R824", "康塔IC卡二维码读卡器QIACS-R824");
        public static BrandModelEnum QIACS_R992 => new BrandModelEnum(33, "QIACS-R992", "康塔IC卡二维码读卡器QIACS-R992");

        public static BrandModelEnum R401_B_S => new BrandModelEnum(4, "R401-B-S", "R401-B-S");
        public static BrandModelEnum RF_RC730 => new BrandModelEnum(5, "RF-RC730", "RF-RC730");
        public static BrandModelEnum QSCS_DLS81 => new BrandModelEnum(8, "QSCS-DLS81", "康塔二次派梯一体机QSCS-DLS81");
        public static BrandModelEnum HIKVISION_DS_K1T672MW => new BrandModelEnum(9, "HIKVISION-DS-K1T672MW", "海康人脸识别面板机7寸DS-K1T672MW");
        public static BrandModelEnum HIKVISION_DS_K5604Z_ZZH => new BrandModelEnum(10, "HIKVISION-DS-K5604Z-ZZH", "海康人脸识别面板机10寸DS-K5604Z-ZZH");
        public static BrandModelEnum HIKVISION_DS_K2210 => new BrandModelEnum(11, "HIKVISION-DS-K2210", "海康梯控主机DS-K2210");
        public static BrandModelEnum QSCS_3600P => new BrandModelEnum(12, "QSCS-3600P", "康塔边缘处理器QSCS-3600P");
        public static BrandModelEnum QSCS_2050WVH_GS => new BrandModelEnum(13, "QSCS-2050WVH-GS", "康塔闸机显示屏QSCS-2050WVH-GS");
        public static BrandModelEnum QIACS_DLS81 => new BrandModelEnum(14, "QIACS-DLS81", "康塔二次派梯一体机QIACS-DLS81");
        public static BrandModelEnum KONE_DCS => new BrandModelEnum(15, "KONE-DCS", "通力电梯");
        public static BrandModelEnum HITACHI_DFRS => new BrandModelEnum(16, "HITACHI-DFRS", "日立电梯");
        public static BrandModelEnum MITSUBISHI_ELSGW => new BrandModelEnum(17, "Mitsubishi-ELSGW", "三菱电梯ELSGW");
        public static BrandModelEnum SCHINDLER_PORT => new BrandModelEnum(18, "Schindler-PORT", "迅达电梯");
        public static BrandModelEnum QIACS_2050WVH_GS => new BrandModelEnum(19, "QIACS-2050WVH-GS", "康塔闸机显示屏QIACS-2050WVH-GS");
        public static BrandModelEnum RF_AX400 => new BrandModelEnum(20, "RF-AX400", "电梯厅选层器RF-AX400");
        public static BrandModelEnum QIACS_SCREEN100 => new BrandModelEnum(21, "QIACS-Screen100", "本体屏QIACS-Screen100");
        public static BrandModelEnum HIKVISION_DS_5607Z_ZZH => new BrandModelEnum(23, "HIKVISION-DS-5607Z-ZZH", "海康人脸识别面板机10寸DS-5607Z-ZZH");
        public static BrandModelEnum HIKVISION_K152AM => new BrandModelEnum(24, "HIKVISION-K152AM", "海康读卡器K152AM");
        public static BrandModelEnum SCHINDLER_PORT_SELECTOR => new BrandModelEnum(27, "Schindler-PORT-Selector", "迅达电梯选层器");
        public static BrandModelEnum MITSUBISHI_ELIP => new BrandModelEnum(28, "Mitsubishi-ELIP", "三菱电梯E-lip");
        public static BrandModelEnum KONE_DCS_SELECTOR => new BrandModelEnum(29, "KONE-DCS-Selector", "通力电梯选层器");
        public static BrandModelEnum HITACHI_DFRS_SELECTOR => new BrandModelEnum(30, "HITACHI-DFRS-Selector", "日立电梯选层器");
        public static BrandModelEnum MITSUBISHI_ELSGW_SELECTOR => new BrandModelEnum(31, "Mitsubishi-ELSGW-Selector", "三菱电梯ELSGW选层器");
        public static BrandModelEnum MITSUBISHI_ELIP_SELECTOR => new BrandModelEnum(32, "Mitsubishi-ELIP-Selector", "三菱电梯E-lip选层器");
        public static BrandModelEnum HITACHI_ELE_3600 =>new BrandModelEnum(33,"HITACHI-ELE-3600","派梯客户端");
        //public static BrandModelEnum SCHINDLER_PORT_SYNC => new BrandModelEnum(24, "Schindler-PORT-SYNC", "迅达电梯同步数据");
        //public static BrandModelEnum SCHINDLER_PORT_DISPATCH => new BrandModelEnum(25, "Schindler-PORT-DISPATCH", "迅达电梯派梯");
        //public static BrandModelEnum SCHINDLER_PORT_RECORD => new BrandModelEnum(26, "Schindler-PORT-RECORD", "迅达电梯获取记录");


        //public static BrandModelEnum KONE_DCS_DISPATCH => new BrandModelEnum(28, "KONE-DCS-DISPATCH", "通力派梯");
        //public static BrandModelEnum KONE_DCS_ACCESS => new BrandModelEnum(29, "KONE-DCS-ACCESS", "通力选层器");
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
                                HIKVISION_DS_5607Z_ZZH,
                                HIKVISION_DS_K2210,
                                QSCS_3600P,
                                QSCS_2050WVH_GS,
                                QIACS_DLS81,
                                HITACHI_ELE_3600
                            };
                        }
                    }
                }
                return _items;
            }
        }
    }
}
