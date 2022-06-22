using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Common.QuantaModule
{
    public class QuantaCommandEnum : BaseEnum
    {
        public QuantaCommandEnum(int code, string value, string text) : base(code, value, text)
        {
            Module = QuantaModuleEnum.Show;
        }

        public QuantaModuleEnum Module { get; private set; }

        public static QuantaCommandEnum Pass => new QuantaCommandEnum(1, "PASS", "通行");
        public static QuantaCommandEnum HandleElevator => new QuantaCommandEnum(2, "Handle_Elevator", "派梯");
    }
}
