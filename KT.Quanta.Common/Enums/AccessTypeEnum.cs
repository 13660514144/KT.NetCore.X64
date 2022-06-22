using KT.Common.Core.Enums;
using System.Collections.Generic;

namespace KT.Quanta.Common.Enums
{
    public class AccessTypeEnum : BaseEnum
    {
        public AccessTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static AccessTypeEnum FACE { get; } = new AccessTypeEnum(1, "FACE", "人脸");
        public static AccessTypeEnum IC_CARD { get; } = new AccessTypeEnum(2, "IC_CARD", "IC卡");
        public static AccessTypeEnum QR_CODE { get; } = new AccessTypeEnum(3, "QR_CODE", "二维码");

        private static readonly object _locker = new object();
        private static List<AccessTypeEnum> _items;
        public static List<AccessTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<AccessTypeEnum>()
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
