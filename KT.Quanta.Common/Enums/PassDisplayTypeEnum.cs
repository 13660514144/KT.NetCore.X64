using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Common.Enums
{
    public class PassDisplayTypeEnum : BaseEnum
    {
        public PassDisplayTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static PassDisplayTypeEnum NoRight => new PassDisplayTypeEnum(1, "NO_RIGHT", "无权限");
        public static PassDisplayTypeEnum Enter => new PassDisplayTypeEnum(2, "INLET", "欢迎进入");
        public static PassDisplayTypeEnum Leave => new PassDisplayTypeEnum(3, "OUTLET", "再见");
        public static PassDisplayTypeEnum ConfigErr => new PassDisplayTypeEnum(4, "CONFIGERR", "配置错误");
        public static PassDisplayTypeEnum API => new PassDisplayTypeEnum(4, "APIERR", "API请求错误");
    }
}
