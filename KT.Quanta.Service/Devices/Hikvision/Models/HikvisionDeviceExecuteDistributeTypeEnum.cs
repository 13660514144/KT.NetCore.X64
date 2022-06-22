using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Hikvision
{
    public class HikvisionDeviceExecuteDistributeTypeEnum : BaseEnum
    {
        public HikvisionDeviceExecuteDistributeTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static HikvisionDeviceExecuteDistributeTypeEnum AddOrUpdatePassRight => new HikvisionDeviceExecuteDistributeTypeEnum(1, "GET_CARD", "获取卡");
        public static HikvisionDeviceExecuteDistributeTypeEnum DeletePassRight => new HikvisionDeviceExecuteDistributeTypeEnum(2, "SET_CARD", "新增或修改卡");
    }
}
