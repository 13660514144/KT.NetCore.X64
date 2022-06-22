using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Unit.Entity.Enums
{
    public class TurnstileShowTypeEnum : BaseEnum
    {
        public TurnstileShowTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static TurnstileShowTypeEnum EnterPass = new TurnstileShowTypeEnum(1, "ENTER_PASS", "入");
        public static TurnstileShowTypeEnum LeavePass = new TurnstileShowTypeEnum(2, "LEAVE_PASS", "出");
        public static TurnstileShowTypeEnum NoRight = new TurnstileShowTypeEnum(3, "NO_RIGHT", "无权限");
        public static TurnstileShowTypeEnum HandleElevator = new TurnstileShowTypeEnum(4, "HANDLE_ELEVATOR", "派梯");
    }
}
