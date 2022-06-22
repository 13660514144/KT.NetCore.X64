using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Common
{
    public class HandleElevatorTypeEnum : BaseEnum
    {
        public HandleElevatorTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static HandleElevatorTypeEnum MitsubishiElip => new HandleElevatorTypeEnum(1, "MITSUBISHI_ELIP", "三菱E-LIP派梯");
        public static HandleElevatorTypeEnum MitsubishiToward => new HandleElevatorTypeEnum(2, "MITSUBISHI_TOWARD", "三菱ELSGW转E-LIP派梯");
    }
}
