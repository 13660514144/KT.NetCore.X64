using KT.Common.Core.Enums;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients
{
    public class MitsubishiElipCommandEnum : BaseEnum
    {
        public MitsubishiElipCommandEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static MitsubishiElipCommandEnum Heartbeat => new MitsubishiElipCommandEnum(17, "HEARTBEAT", "心跳包(0x11)");
        public static MitsubishiElipCommandEnum HandleResult => new MitsubishiElipCommandEnum(144, "HANDLE_RESULT", "派梯结果(0x90)");
        public static MitsubishiElipCommandEnum AutomaticAccess => new MitsubishiElipCommandEnum(16, "AUTOMATIC_ACCESS", "直接派梯(0x10)");
        public static MitsubishiElipCommandEnum ManualAccess => new MitsubishiElipCommandEnum(32, "MANUAL_ACCESS", "多楼层派梯(0x20)");
    }
}