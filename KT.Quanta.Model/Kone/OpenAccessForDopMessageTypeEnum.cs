using KT.Common.Core.Enums;

namespace KT.Quanta.Model.Kone
{
    public class OpenAccessForDopMessageTypeEnum : BaseEnum
    {
        public OpenAccessForDopMessageTypeEnum()
        {
        }

        public OpenAccessForDopMessageTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static OpenAccessForDopMessageTypeEnum NORMAL => new OpenAccessForDopMessageTypeEnum(1, "NORMAL", "Normal");
        public static OpenAccessForDopMessageTypeEnum WITH_CALL_TYPE => new OpenAccessForDopMessageTypeEnum(2, "WITH_CALL_TYPE", "With call type");

    }
}
