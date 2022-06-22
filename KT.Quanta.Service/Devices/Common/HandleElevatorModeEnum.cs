using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Common
{
    public class HandleElevatorModeEnum : BaseEnum
    {
        public HandleElevatorModeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static HandleElevatorModeEnum SingleFloor => new HandleElevatorModeEnum(1, "SINGLE_FLOOR", "直接派梯");
        public static HandleElevatorModeEnum MultiFloor => new HandleElevatorModeEnum(2, "MULTI_FLOOR", "多楼层派梯");
    }
}
