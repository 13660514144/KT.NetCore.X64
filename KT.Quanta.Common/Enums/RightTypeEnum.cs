using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Common.Enums
{
    public class RightTypeEnum : BaseEnum
    {
        public RightTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static RightTypeEnum TURNSTILE => new RightTypeEnum(1, "TURNSTILE", "闸机");
        public static RightTypeEnum ELEVATOR => new RightTypeEnum(2, "ELEVATOR", "梯控");
    }
}
