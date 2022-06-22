using KT.Common.Core.Enums;

namespace KT.Proxy.BackendApi.Enums
{
    public class VisitorImportQrPrintTypeEnum : BaseEnum
    {
        public VisitorImportQrPrintTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }
        public static VisitorImportQrPrintTypeEnum ALL = new VisitorImportQrPrintTypeEnum(1, "ALL", "全部打印");
        public static VisitorImportQrPrintTypeEnum SOME = new VisitorImportQrPrintTypeEnum(2, "SOME", "选择打印");
    }
}
