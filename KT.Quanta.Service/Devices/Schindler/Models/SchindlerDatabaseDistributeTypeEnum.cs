using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    public class SchindlerDatabaseDistributeTypeEnum : BaseEnum
    {
        public SchindlerDatabaseDistributeTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static SchindlerDatabaseDistributeTypeEnum ChangeOrInsertPerson => new SchindlerDatabaseDistributeTypeEnum(1, "CHANGE_OR_INSERT_PERSON", "新增或修改用户");
        public static SchindlerDatabaseDistributeTypeEnum ChangePersonZone => new SchindlerDatabaseDistributeTypeEnum(2, "CHANGE_PERSON_ZONE", "修改用户Zone");
        public static SchindlerDatabaseDistributeTypeEnum DeletePerson => new SchindlerDatabaseDistributeTypeEnum(3, "DELETE_PERSON", "删除用户");

    }
}
