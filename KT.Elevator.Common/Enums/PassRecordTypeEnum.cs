using System;
using KT.Common.Core.Enums;

namespace KT.Elevator.Common.Enums
{
    public class PassRecordTypeEnum : BaseEnum
    {
        public PassRecordTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static PassRecordTypeEnum MULTIPLE_TIMES_PASS { get; } = new PassRecordTypeEnum(1, "MULTIPLE_TIMES_PASS", "多进多出正常通行");
        public static PassRecordTypeEnum ONE_TIMES_PASS { get; } = new PassRecordTypeEnum(1, "ONE_TIMES_PASS", "一进一出正常通行");
        public static PassRecordTypeEnum REPEAT_PASS { get; } = new PassRecordTypeEnum(3, "REPEAT_PASS", "指定时间重复通行");
        public static PassRecordTypeEnum CARD_NOT_RIGHT { get; } = new PassRecordTypeEnum(2, "CARD_NOT_RIGHT", "卡无权限");
        public static PassRecordTypeEnum TIME_OUT { get; } = new PassRecordTypeEnum(3, "TIME_OUT", "过期");
        public static PassRecordTypeEnum PASS_AISLE_NOT_RIGHT { get; } = new PassRecordTypeEnum(3, "PASS_AISLE_NOT_RIGHT", "通道无权限");
        public static PassRecordTypeEnum CARD_TYPE_NOT_RIGHT { get; } = new PassRecordTypeEnum(2, "CARD_TYPE_NOT_RIGHT", "卡类型无权限");
        public static PassRecordTypeEnum ALREADY_PASSED { get; } = new PassRecordTypeEnum(3, "ALREADY_PASSED", "已经通行，不能再通行");
        public static PassRecordTypeEnum UNKNOWN { get; } = new PassRecordTypeEnum(3, "UNKNOWN", "未知权限");

    }
}
