using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Data.Enums
{
    public class ServerTypeEnum : BaseEnum
    {
        public ServerTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }
        public static ServerTypeEnum ELEVATOR { get; } = new ServerTypeEnum(1, "ELEVATOR", "梯控服务");
    }
}
