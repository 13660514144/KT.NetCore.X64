using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Common.Enums
{
    /// <summary>
    /// 通信设备
    /// </summary>
    public class CommunicateDeviceTypeEnum : BaseEnum
    {
        public CommunicateDeviceTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static CommunicateDeviceTypeEnum HIKVISION { get; } = new CommunicateDeviceTypeEnum(1, "HIKVISION", "海康硬件设备");
        public static CommunicateDeviceTypeEnum QUANTA { get; } = new CommunicateDeviceTypeEnum(2, "QUANTA", "Quanta设备");
        //public static CommunicateDeviceTypeEnum KONE { get; } = new CommunicateDeviceTypeEnum(3, "KONE", "通力电梯");

        //public static CommunicateDeviceTypeEnum SCHINDLER { get; } = new CommunicateDeviceTypeEnum(4, "SCHINDLER", "迅达电梯");

        public static CommunicateDeviceTypeEnum SCHINDLER_SYNC { get; } = new CommunicateDeviceTypeEnum(5, "SCHINDLER_SYNC", "迅达同步数据");
        public static CommunicateDeviceTypeEnum SCHINDLER_DISPATCH { get; } = new CommunicateDeviceTypeEnum(6, "SCHINDLER_DISPATCH", "迅达派梯");
        public static CommunicateDeviceTypeEnum SCHINDLER_RECORD { get; } = new CommunicateDeviceTypeEnum(7, "SCHINDLER_RECORD", "迅达记录");

        public static CommunicateDeviceTypeEnum KONE_RCGIF { get; } = new CommunicateDeviceTypeEnum(8, "KONE_RCGIF", "通力派梯");
        public static CommunicateDeviceTypeEnum KONE_ELI { get; } = new CommunicateDeviceTypeEnum(9, "KONE_ELI", "通力面板机");

        public static CommunicateDeviceTypeEnum MITSUBISHI_ELSGW { get; } = new CommunicateDeviceTypeEnum(10, "MITSUBISHI-ELSGW", "三菱电梯ELSGW");
        public static CommunicateDeviceTypeEnum MITSUBISHI_ELIP { get; } = new CommunicateDeviceTypeEnum(11, "MITSUBISHI-ELIP", "三菱电梯E-LIP");
    }
}
