using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Common.Enums
{
    public class DistributeTypeEnum : BaseEnum
    {
        public DistributeTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }
        public static DistributeTypeEnum ADD { get; } = new DistributeTypeEnum(1, "ADD", "新增");
        public static DistributeTypeEnum EDIT { get; } = new DistributeTypeEnum(2, "EDIT", "修改");
        public static DistributeTypeEnum DELETE { get; } = new DistributeTypeEnum(3, "DELETE", "删除");
        public static DistributeTypeEnum DELETE_RECORD { get; } = new DistributeTypeEnum(3, "DELETE_RECORD", "删除删除推送错误");

        public static DistributeTypeEnum ADD_OR_UPDATE_CARD_DEVICE { get; } = new DistributeTypeEnum(1, "ADD_OR_UPDATE_CARD_DEVICE", "新增或修改读卡器设备");
        public static DistributeTypeEnum DELETE_CARD_DEVICE { get; } = new DistributeTypeEnum(2, "DELETE_CARD_DEVICE", "删除除卡器");
        public static DistributeTypeEnum ADD_OR_UPDATE_PASS_RIGHT { get; } = new DistributeTypeEnum(3, "ADD_OR_UPDATE_PASS_RIGHT", "新增或修改通行权限");
        public static DistributeTypeEnum DELETE_PASS_RIGHT { get; } = new DistributeTypeEnum(4, "DELETE_PASS_RIGHT", "删除通行权限");
        //public static DistributeTypeEnum ADD_OR_EDIT_ELEVATOR_GROUP_FLOORS { get; } = new DistributeTypeEnum(5, "ADD_OR_EDIT_ELEVATOR_GROUP_FLOORS", "新增或修电梯组可去楼层映射");
        //public static DistributeTypeEnum DELETE_ELEVATOR_GROUP_FLOORS { get; } = new DistributeTypeEnum(6, "DELETE_ELEVATOR_GROUP_FLOORS", "删除电梯组可去楼层映射");
        public static DistributeTypeEnum ADD_OR_EDIT_HANDLE_ELEVATOR_DEVICE { get; } = new DistributeTypeEnum(5, "ADD_OR_EDIT_HANDLE_ELEVATOR_DEVICE", "新增或修改二次派梯一体机");
        public static DistributeTypeEnum DELETE_HANDLE_ELEVATOR_DEVICE { get; } = new DistributeTypeEnum(6, "DELETE_HANDLE_ELEVATOR_DEVICE", "新增或修改二次派梯一体机");
    }
}
