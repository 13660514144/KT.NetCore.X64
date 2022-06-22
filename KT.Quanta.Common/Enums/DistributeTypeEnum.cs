using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Common.Enums
{
    public class DistributeTypeEnum : BaseEnum
    {
        public DistributeTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static DistributeTypeEnum ELEVATOR_ADD_OR_UPDATE_CARD_DEVICE { get; } = new DistributeTypeEnum(1, "ELEVATOR_ADD_OR_UPDATE_CARD_DEVICE", "新增或修改读卡器设备");
        public static DistributeTypeEnum ELEVATOR_DELETE_CARD_DEVICE { get; } = new DistributeTypeEnum(2, "ELEVATOR_DELETE_CARD_DEVICE", "删除除卡器");
        public static DistributeTypeEnum ELEVATOR_ADD_OR_UPDATE_PASS_RIGHT { get; } = new DistributeTypeEnum(3, "ELEVATOR_ADD_OR_UPDATE_PASS_RIGHT", "新增或修改通行权限");
        public static DistributeTypeEnum ELEVATOR_DELETE_PASS_RIGHT { get; } = new DistributeTypeEnum(4, "ELEVATOR_DELETE_PASS_RIGHT", "删除通行权限");
        public static DistributeTypeEnum ELEVATOR_ADD_OR_EDIT_HANDLE_ELEVATOR_DEVICE { get; } = new DistributeTypeEnum(5, "ELEVATOR_ADD_OR_EDIT_HANDLE_ELEVATOR_DEVICE", "新增或修改二次派梯一体机");
        public static DistributeTypeEnum ELEVATOR_DELETE_HANDLE_ELEVATOR_DEVICE { get; } = new DistributeTypeEnum(6, "ELEVATOR_DELETE_HANDLE_ELEVATOR_DEVICE", "新增或修改二次派梯一体机");

        public static DistributeTypeEnum TURNSTILE_ADD_OR_UPDATE_CARD_DEVICE { get; } = new DistributeTypeEnum(1, "TURNSTILE_ADD_OR_UPDATE_CARD_DEVICE", "新增或修改读卡器设备");
        public static DistributeTypeEnum TURNSTILE_DELETE_CARD_DEVICE { get; } = new DistributeTypeEnum(2, "TURNSTILE_DELETE_CARD_DEVICE", "删除除卡器");
        public static DistributeTypeEnum TURNSTILE_ADD_OR_UPDATE_PASS_RIGHT { get; } = new DistributeTypeEnum(3, "TURNSTILE_ADD_OR_UPDATE_PASS_RIGHT", "新增或修改通行权限");
        public static DistributeTypeEnum TURNSTILE_DELETE_PASS_RIGHT { get; } = new DistributeTypeEnum(4, "TURNSTILE_DELETE_PASS_RIGHT", "删除通行权限");
        public static DistributeTypeEnum TURNSTILE_ADD_OR_UPDATE_RIGHT_GROUP { get; } = new DistributeTypeEnum(5, "TURNSTILE_ADD_OR_UPDATE_RIGHT_GROUP", "新增或修改通行权限组");
        public static DistributeTypeEnum TURNSTILE_DELETE_RIGHT_GROUP { get; } = new DistributeTypeEnum(6, "TURNSTILE_DELETE_RIGHT_GROUP", "删除通行权限组");

    }
}
