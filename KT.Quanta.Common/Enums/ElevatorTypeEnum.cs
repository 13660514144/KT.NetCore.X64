using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Common.Enums
{
    /// <summary>
    /// 电梯
    /// </summary>
    public class ElevatorTypeEnum : BaseEnum
    {
        public ElevatorTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }
        public static ElevatorTypeEnum KONE { get; } = new ElevatorTypeEnum(1, "KONE", "通力电梯");
    }
}
