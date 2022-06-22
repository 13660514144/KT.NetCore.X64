using KT.Common.Core.Enums;

namespace KT.Quanta.Model.Kone
{
    /// <summary>
    /// Standard call types (for GCAC) 
    /// 0 = Normal call(default)       
    /// 1 = Handicap call              
    /// 2 = Priority call              
    /// 3 = Empty car call             
    /// 4 = Space allocation call      
    /// </summary>   
    public class StandardCallTypeEnum : BaseEnum
    {
        public StandardCallTypeEnum()
        {
        }

        public StandardCallTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static StandardCallTypeEnum NormalCall => new StandardCallTypeEnum(0, "NORMAL_CALL", "Normal call(default)");
        public static StandardCallTypeEnum HandicapCall => new StandardCallTypeEnum(1, "HANDICAP_CALL", "Handicap call");
        public static StandardCallTypeEnum PriorityCall => new StandardCallTypeEnum(2, "PRIORITY_CALL", "Priority call");
        public static StandardCallTypeEnum EmptyCarCall => new StandardCallTypeEnum(3, "EMPTY_CAR_CALL", "Empty car call");
        public static StandardCallTypeEnum SpaceAllocationCall => new StandardCallTypeEnum(4, "SPACE_ALLOCATION_CALL", "Space allocation call");

    }
}
