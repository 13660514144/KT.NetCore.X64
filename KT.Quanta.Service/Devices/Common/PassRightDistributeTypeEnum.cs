using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Common
{
    public class PassRightDistributeTypeEnum : BaseEnum
    {
        public PassRightDistributeTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static PassRightDistributeTypeEnum AddOrEdit => new PassRightDistributeTypeEnum(1, "ADD_OR_EDIT", "新增或修改");
        public static PassRightDistributeTypeEnum Delete => new PassRightDistributeTypeEnum(2, "DELETE", "删除");
        public static PassRightDistributeTypeEnum AddOrEditByIds => new PassRightDistributeTypeEnum(3, "ADD_OR_EDIT_BY_IDS", "根据Ids新增或修改");
        public static PassRightDistributeTypeEnum DeleteByIds => new PassRightDistributeTypeEnum(4, "DELETE_BY_IDS", "根据Ids删除");
    }
}
