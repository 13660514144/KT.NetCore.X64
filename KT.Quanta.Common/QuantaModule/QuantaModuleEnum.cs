using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Common.QuantaModule
{
    public class QuantaModuleEnum : BaseEnum
    {
        public QuantaModuleEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static QuantaModuleEnum Show => new QuantaModuleEnum(1, "SHOW", "显示");
    }
}
