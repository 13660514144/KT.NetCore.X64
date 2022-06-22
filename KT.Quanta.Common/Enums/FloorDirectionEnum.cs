using KT.Common.Core.Enums;
using System.Collections.Generic;

namespace KT.Quanta.Common.Enums
{
    public class FloorDirectionEnum : BaseEnum
    {
        public FloorDirectionEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static FloorDirectionEnum Front { get; } = new FloorDirectionEnum(1, "FRONT", "前门");
        public static FloorDirectionEnum Rear { get; } = new FloorDirectionEnum(2, "REAR", "后门");
    }
}
