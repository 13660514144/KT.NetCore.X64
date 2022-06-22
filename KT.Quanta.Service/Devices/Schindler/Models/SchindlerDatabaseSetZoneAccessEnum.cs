using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    public class SchindlerDatabaseSetZoneAccessEnum : BaseEnum
    {
        public SchindlerDatabaseSetZoneAccessEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static SchindlerDatabaseSetZoneAccessEnum Always => new SchindlerDatabaseSetZoneAccessEnum(1, "Always", "增加");
        public static SchindlerDatabaseSetZoneAccessEnum None => new SchindlerDatabaseSetZoneAccessEnum(2, "None", "删除");
    }
}
