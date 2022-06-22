using KT.Common.Core.Enums;
using System.Collections.Generic;

namespace KT.Elevator.Common.Enums
{
    public class DeviceTypeEnum : BaseEnum
    {
        public DeviceTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        /// IC卡
        public static DeviceTypeEnum IC { get; } = new DeviceTypeEnum(1, "IC", "IC卡");
        /// 二维码
        public static DeviceTypeEnum QR { get; } = new DeviceTypeEnum(2, "QR", "二维码");
        /// IC卡、二维码
        public static DeviceTypeEnum IC_QR { get; } = new DeviceTypeEnum(3, "IC_QR", "人脸");
        /// IC卡、二维码、人脸
        public static DeviceTypeEnum IC_QR_FACE { get; } = new DeviceTypeEnum(3, "IC_QR_FACE", "人脸");
        /// 人脸摄像头
        public static DeviceTypeEnum FACE_CAMERA { get; } = new DeviceTypeEnum(4, "FACE_CAMERA", "人脸摄像头");
        /// 门禁读卡器
        public static DeviceTypeEnum ACCESS_READER { get; } = new DeviceTypeEnum(5, "ACCESS_READER", "门禁读卡器");
        /// 二次派梯一体机
        public static DeviceTypeEnum ELEVATOR_SECONDARY { get; } = new DeviceTypeEnum(6, "ELEVATOR_SECONDARY", "二次派梯一体机");
        /// 派梯边缘处理器
        public static DeviceTypeEnum ELEVATOR_PROCESSOR { get; } = new DeviceTypeEnum(7, "ELEVATOR_PROCESSOR", "派梯边缘处理器");
        /// 闸机边缘处理器
        public static DeviceTypeEnum ELEVATOR_GATE_DISPLAY { get; } = new DeviceTypeEnum(8, "ELEVATOR_GATE_DISPLAY", "闸机边缘处理器");
        /// 电梯厅选层器
        public static DeviceTypeEnum ELEVATOR_SELECTOR { get; } = new DeviceTypeEnum(9, "ELEVATOR_SELECTOR", "电梯厅选层器");
        ///// 海康威视人脸识别
        //public static DeviceTypeEnum GATE_PANEL_MACHINE { get; } = new DeviceTypeEnum(10, "GATE_PANEL_MACHINE", "闸机面板机");
        ///// 海康威视梯控主机
        //public static DeviceTypeEnum HIKVISION_ELEVATOR { get; } = new DeviceTypeEnum(11, "HIKVISION_ELEVATOR", "海康威视梯控主机");

    }
}
