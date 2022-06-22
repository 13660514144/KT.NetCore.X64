using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    public class SchindlerModuleTypeEnum : BaseEnum
    {
        public SchindlerModuleTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static SchindlerModuleTypeEnum SYNC => new SchindlerModuleTypeEnum(1, "SYNC", "同步数据");
        public static SchindlerModuleTypeEnum DISPATCH => new SchindlerModuleTypeEnum(2, "DISPATCH", "派梯");
        public static SchindlerModuleTypeEnum RECORD => new SchindlerModuleTypeEnum(3, "RECORD", "记录");
    }
}
