using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Network.Helpers
{
    /// <summary>
    /// 所有操作命令
    /// </summary>
    public class AllActionEnum : BaseEnum
    {
        public AllActionEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static AllActionEnum AddOrEditPassRights => new AllActionEnum(1, "AddOrEditPassRights", "批量新增或修改权限");
        public static AllActionEnum AddOrEditPassRight => new AllActionEnum(2, "AddOrEditPassRight", "新增或修改权限");
        public static AllActionEnum DeletePassRight => new AllActionEnum(3, "DeletePassRight", "删除权限");
        public static AllActionEnum AddOrEditCardDevices => new AllActionEnum(4, "AddOrEditCardDevices", "批量新增或修改读卡器");
        public static AllActionEnum AddOrEditCardDevice => new AllActionEnum(5, "AddOrEditCardDevice", "新增或修改读卡器");
        public static AllActionEnum DeleteCardDevice => new AllActionEnum(6, "DeleteCardDevice", "删除读卡器");
        public static AllActionEnum HandleElevatorSuccess => new AllActionEnum(7, "HandleElevatorSuccess", "派梯成功回调");
        public static AllActionEnum AddOrEditHandleElevatorDevice => new AllActionEnum(8, "AddOrEditHandleElevatorDevice", "新增或修改派梯设备");
        public static AllActionEnum DeleteHandleElevatorDevice => new AllActionEnum(9, "DeleteHandleElevatorDevice", "删除派梯设备");
        public static AllActionEnum RightHandleElevator => new AllActionEnum(10, "RightHandleElevator", "权限派梯");

        public const string AddOrEditPassRightsValue = "AddOrEditPassRights";
        public const string AddOrEditPassRightValue = "AddOrEditPassRight";
        public const string DeletePassRightValue = "DeletePassRight";
        public const string AddOrEditCardDevicesValue = "AddOrEditCardDevices";
        public const string AddOrEditCardDeviceValue = "AddOrEditCardDevice";
        public const string DeleteCardDeviceValue = "DeleteCardDevice";
        public const string HandleElevatorSuccessValue = "HandleElevatorSuccess";
        public const string AddOrEditHandleElevatorDeviceValue = "AddOrEditHandleElevatorDevice";
        public const string DeleteHandleElevatorDeviceValue = "DeleteHandleElevatorDevice";
        public const string RightHandleElevatorValue = "RightHandleElevator";

        public const int AddOrEditPassRightsCode = 1;
        public const int AddOrEditPassRightCode = 2;
        public const int DeletePassRightCode = 3;
        public const int AddOrEditCardDevicesCode = 4;
        public const int AddOrEditCardDeviceCode = 5;
        public const int DeleteCardDeviceCode = 6;
        public const int HandleElevatorSuccessCode = 7;
        public const int AddOrEditHandleElevatorDeviceCode = 8;
        public const int DeleteHandleElevatorDeviceCode = 9;
        public const int RightHandleElevatorCode = 10;
    }
}
