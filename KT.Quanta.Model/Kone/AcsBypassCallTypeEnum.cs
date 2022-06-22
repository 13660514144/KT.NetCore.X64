using KT.Common.Core.Enums;

namespace KT.Quanta.Model.Kone
{
    /// <summary>
    /// ACS-Bypass call types (for RCGIF)
    /// 20 = Normal call(default)
    /// 21 = Handicap call
    /// 22 = Priority call
    /// 23 = Empty car call
    /// 24 = Space allocation call
    /// </summary>
    public class AcsBypassCallTypeEnum : BaseEnum
    {
        public AcsBypassCallTypeEnum()
        {
        }

        public AcsBypassCallTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static AcsBypassCallTypeEnum NormalCall => new AcsBypassCallTypeEnum(20, "NORMAL_CALL", "Normal call(default)");
        public static AcsBypassCallTypeEnum HandicapCall => new AcsBypassCallTypeEnum(21, "HANDICAP_CALL", "Handicap call");
        public static AcsBypassCallTypeEnum PriorityCall => new AcsBypassCallTypeEnum(22, "PRIORITY_CALL", "Priority call");
        public static AcsBypassCallTypeEnum EmptyCarCall => new AcsBypassCallTypeEnum(23, "EMPTY_CAR_CALL", "Empty car call");
        public static AcsBypassCallTypeEnum SpaceAllocationCall => new AcsBypassCallTypeEnum(24, "SPACE_ALLOCATION_CALL", "Space allocation call");
    }
}
