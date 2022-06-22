using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Enums
{
    public class SystemConfigEnum : BaseEnum
    {
        public static SystemConfigEnum READER { get; } = new SystemConfigEnum(1, "READER", "证件阅读器");
        public static SystemConfigEnum PRINTER { get; } = new SystemConfigEnum(2, "PRINTER", "打印机");
        public static SystemConfigEnum CARD_DEVICE { get; } = new SystemConfigEnum(3, "CARD_DEVICE", "读卡器");
        public static SystemConfigEnum CARD_ISSUE_METHOD { get; } = new SystemConfigEnum(4, "CARD_ISSUE_METHOD", "发卡机");
        public static SystemConfigEnum SERVER_ADDRESS { get; } = new SystemConfigEnum(5, "SERVER_ADDRESS", "服务器地址");
        public static SystemConfigEnum SYSTEM_NAME { get; } = new SystemConfigEnum(6, "SYSTEM_NAME", "系统名称");
        public static SystemConfigEnum SYSTEM_LOGO_URL { get; } = new SystemConfigEnum(7, "SYSTEM_LOGO_URL", "系统LogoUrl地址");
        public static SystemConfigEnum UPLOAD_FILE_SIZE { get; } = new SystemConfigEnum(8, "UPLOAD_FILE_SIZE", "上传文件最大值(kb)");
        public static SystemConfigEnum IS_REMEMBER_AUTH_MODEL { get; } = new SystemConfigEnum(9, "IS_REMEMBER_AUTH_MODEL", "下次不再提示授权方式");
        public static SystemConfigEnum AUTH_MODEL { get; } = new SystemConfigEnum(10, "AUTH_MODEL", "授权方式");
        public static SystemConfigEnum AUTH_TIME_MODEL { get; } = new SystemConfigEnum(11, "AUTH_TIME_MODEL", "授权时限");
        public static SystemConfigEnum AUTH_TIME_DAY { get; } = new SystemConfigEnum(12, "AUTH_TIME_DAY", "授权天数");

        private SystemConfigEnum(int code, string value, string text) : base(code, value, text)
        {

        }
    }
}
