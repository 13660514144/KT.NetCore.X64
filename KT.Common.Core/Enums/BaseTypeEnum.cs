using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Core.Enums
{
    public class BaseTypeEnum : BaseEnum
    {
        public BaseTypeEnum(int code, string value, string text, int baseValue) : base(code, value, text)
        {
            BaseValue = baseValue;
        }

        public static BaseTypeEnum BIN { get; } = new BaseTypeEnum(1, "BIN", "二进制", 2);
        public static BaseTypeEnum OCT { get; } = new BaseTypeEnum(2, "OCT", "八进制", 8);
        public static BaseTypeEnum DEC { get; } = new BaseTypeEnum(3, "DEC", "十进制", 10);
        public static BaseTypeEnum HEX { get; } = new BaseTypeEnum(4, "HEX", "十六进制", 16);

        public int BaseValue { get; set; }
    }
}