using KT.Common.Core.Enums;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models
{
    public class MitsubishiElsgwCommandEnum : BaseEnum
    {
        public MitsubishiElsgwCommandEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static MitsubishiElsgwCommandEnum Heartbeat => new MitsubishiElsgwCommandEnum(241, "HEARTBEAT", "心跳包(F1h)");
        public static MitsubishiElsgwCommandEnum HandleResult => new MitsubishiElsgwCommandEnum(129, "HANDLE_RESULT", "派梯结果(81h)");
        public static MitsubishiElsgwCommandEnum SingleFloorCall => new MitsubishiElsgwCommandEnum(1, "SINGLE_FLOOR_CALL", "直接派梯(01h)");
        public static MitsubishiElsgwCommandEnum MultiFloorCall => new MitsubishiElsgwCommandEnum(2, "MULTI_FLOOR_CALL", "多楼层派梯(02h)");
    }
}