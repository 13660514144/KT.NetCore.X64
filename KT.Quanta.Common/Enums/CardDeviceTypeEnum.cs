using KT.Common.Core.Enums;
using System.Collections.Generic;

namespace KT.Quanta.Common.Enums
{
    public class CardDeviceTypeEnum : BaseEnum
    {
        public CardDeviceTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        /// IC卡
        public static CardDeviceTypeEnum IC { get; } = new CardDeviceTypeEnum(1, "IC", "IC卡");
        /// 二维码
        public static CardDeviceTypeEnum QR { get; } = new CardDeviceTypeEnum(2, "QR", "二维码");
        /// IC卡、二维码
        public static CardDeviceTypeEnum IC_QR { get; } = new CardDeviceTypeEnum(3, "IC_QR", "人脸");
        /// IC卡、二维码、人脸
        public static CardDeviceTypeEnum IC_QR_FACE { get; } = new CardDeviceTypeEnum(3, "IC_QR_FACE", "人脸");
        /// 人脸摄像头
        public static CardDeviceTypeEnum FACE_CAMERA { get; } = new CardDeviceTypeEnum(4, "FACE_CAMERA", "人脸摄像头");
        /// 门禁读卡器
        public static CardDeviceTypeEnum ACCESS_READER { get; } = new CardDeviceTypeEnum(5, "ACCESS_READER", "门禁读卡器");

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
                                IC ,
                                QR ,
                                IC_QR ,
                                IC_QR_FACE ,
                                FACE_CAMERA ,
                                ACCESS_READER 
                            };
                        }
                    }
                }
                return _items;
            }
        }
    }
}
