using KT.Common.Core.Enums;
using System.Collections.Generic;

namespace KT.Quanta.Common.Enums
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public class DeviceTypeEnum : BaseEnum
    {
        public DeviceTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        /// 二次派梯一体机
        public static DeviceTypeEnum ELEVATOR_SECONDARY { get; } = new DeviceTypeEnum(6, "ELEVATOR_SECONDARY", "二次派梯一体机");
        //边缘处理器派梯
        public static DeviceTypeEnum ELEVATOR_DISPATCH { get; } = new DeviceTypeEnum(17, "ELEVATOR_DISPATCH", "边缘处理器派梯");
        /// 派梯边缘处理器
        public static DeviceTypeEnum ELEVATOR_PROCESSOR { get; } = new DeviceTypeEnum(7, "ELEVATOR_PROCESSOR", "派梯边缘处理器");
        /// 派梯边缘处理器
        public static DeviceTypeEnum ELEVATOR_PROCESSOR_CARD_DEVICE { get; } = new DeviceTypeEnum(8, "ELEVATOR_PROCESSOR_CARD_DEVICE", "派梯边缘处理器-读卡器");
        /// 闸机边缘处理器
        public static DeviceTypeEnum ELEVATOR_GATE_DISPLAY { get; } = new DeviceTypeEnum(9, "ELEVATOR_GATE_DISPLAY", "闸机边缘处理器");
        /// 电梯厅选层器
        public static DeviceTypeEnum ELEVATOR_SELECTOR { get; } = new DeviceTypeEnum(10, "ELEVATOR_SELECTOR", "电梯厅选层器");
        /// 闸机边缘处理器
        public static DeviceTypeEnum TURNSTILE_PROCESSOR { get; } = new DeviceTypeEnum(11, "TURNSTILE_PROCESSOR", "闸机边缘处理器");
        /// 派梯边缘处理器
        public static DeviceTypeEnum TURNSTILE_PROCESSOR_CARD_DEVICE { get; } = new DeviceTypeEnum(12, "TURNSTILE_PROCESSOR_CARD_DEVICE", "闸机边缘处理器-读卡器");

        ///// 海康威视人脸识别
        //public static DeviceTypeEnum GATE_PANEL_MACHINE { get; } = new DeviceTypeEnum(10, "GATE_PANEL_MACHINE", "闸机面板机");
        ///// 海康威视梯控主机
        //public static DeviceTypeEnum HIKVISION_ELEVATOR { get; } = new DeviceTypeEnum(11, "HIKVISION_ELEVATOR", "海康威视梯控主机");

        public static DeviceTypeEnum ELEVATOR_SERVER_GROUP { get; } = new DeviceTypeEnum(13, "ELEVATOR_SERVER_GROUP", "电梯服务组");
        public static DeviceTypeEnum ELEVATOR_SELF_DISPLAY { get; } = new DeviceTypeEnum(14, "ELEVATOR_SELF_DISPLAY", "本体派梯屏");
        public static DeviceTypeEnum ELEVATOR_SERVER { get; } = new DeviceTypeEnum(15, "ELEVATOR_SERVER", "电梯服务");
        public static DeviceTypeEnum ELEVATOR_CLIENT { get; } = new DeviceTypeEnum(16, "ELEVATOR_CLIENT","派梯客户端");
        private static readonly object _locker = new object();
        private static List<DeviceTypeEnum> _items;
        public static List<DeviceTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<DeviceTypeEnum>()
                            {
                                ELEVATOR_SECONDARY ,
                                ELEVATOR_PROCESSOR ,
                                ELEVATOR_GATE_DISPLAY ,
                                ELEVATOR_SELECTOR ,
                                TURNSTILE_PROCESSOR ,
                                ELEVATOR_CLIENT
                            };
                        }
                    }
                }
                return _items;
            }
        }
    }
}
