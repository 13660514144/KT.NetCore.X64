using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Models
{
    public class KoneModuleTypeEnum : BaseEnum
    {
        public KoneModuleTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }
        public static KoneModuleTypeEnum RCGIF => new KoneModuleTypeEnum(1, "RCGIF", "派梯");
        public static KoneModuleTypeEnum ELI => new KoneModuleTypeEnum(2, "ELI", "组控制授权面板");
    }
}
