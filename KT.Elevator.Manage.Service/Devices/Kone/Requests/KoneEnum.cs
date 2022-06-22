using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Kone.Requests
{
    public class KoneMessageTypeEnum : BaseEnum
    {
        public KoneMessageTypeEnum(int code, ushort socketValue, string value, string text) : base(code, value, text)
        {
            SocketValue = socketValue;
        }

        public static KoneMessageTypeEnum MSG_DESTINATION_CALL { get; } = new KoneMessageTypeEnum(1, 8000, "MSG_DESTINATION_CALL", "目的层呼梯");
        public static KoneMessageTypeEnum MSG_HEARTBEAT { get; } = new KoneMessageTypeEnum(2, 8002, "MSG_HEARTBEAT", "心跳包");

        public ushort SocketValue { get; set; }
    }
    public class KoneApplicationMsgTypeEnum : BaseEnum
    {
        public KoneApplicationMsgTypeEnum(int code, ushort socketValue, string value, string text) : base(code, value, text)
        {
            SocketValue = socketValue;
        }

        public static KoneApplicationMsgTypeEnum DESTINATION_CALL { get; } = new KoneApplicationMsgTypeEnum(1, 1, "DESTINATION_CALL", "呼叫电梯发送数据");
        public static KoneApplicationMsgTypeEnum YOUR_TRANSPORTATION { get; } = new KoneApplicationMsgTypeEnum(2, 2, "YOUR_TRANSPORTATION", "呼叫电梯返回结果");


        public ushort SocketValue { get; set; }
    }
}
