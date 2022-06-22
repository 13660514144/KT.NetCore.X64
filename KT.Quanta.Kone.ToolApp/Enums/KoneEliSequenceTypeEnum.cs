using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Enums
{

    public class KoneEliSequenceTypeEnum : BaseEnum
    {
        public KoneEliSequenceTypeEnum()
        {
        }

        public KoneEliSequenceTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }
        public static KoneEliSequenceTypeEnum DopGlobalDefaultMask => new KoneEliSequenceTypeEnum(1, "DOP_GLOBAL_DEFAULT_MASK", "Dop global default mask");
        public static KoneEliSequenceTypeEnum DopSepcificDefaultMask => new KoneEliSequenceTypeEnum(2, "DOP_SEPCIFIC_DEFAULT_MASK", "Dop sepcific default mask");
        public static KoneEliSequenceTypeEnum OpenAccessForDop => new KoneEliSequenceTypeEnum(3, "OPEN_ACCESS_FOR_DOP", "Open access for dop");
        public static KoneEliSequenceTypeEnum OpenAccessForDopWithCallType => new KoneEliSequenceTypeEnum(4, "OPEN_ACCESS_FOR_DOP_WITH_CALL_TYPE", "Open access for dop with call type");
    }

}
