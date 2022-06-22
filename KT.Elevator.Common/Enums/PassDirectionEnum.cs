using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Common.Enums
{
    /// <summary>
    /// 通行方向
    /// </summary>
    public class PassDirectionEnum : BaseEnum
    {
        public PassDirectionEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static PassDirectionEnum ENTRANCE { get; } = new PassDirectionEnum(1, "INLET", "入口");
        public static PassDirectionEnum EXIT { get; } = new PassDirectionEnum(2, "OUTLET", "出口");
    }
}
