using KT.Common.Core.Enums;

namespace KT.Quanta.Common.Enums
{
    public class QrCodeTypeEnum : BaseEnum
    {
        public QrCodeTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        /// IC卡
        public static QrCodeTypeEnum Xita { get; } = new QrCodeTypeEnum(1, "XITA", "西塔");
        /// 二维码
        public static QrCodeTypeEnum Quanta { get; } = new QrCodeTypeEnum(2, "QUANTA", "康塔");
    }
}
