using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    public class KoneRcgifMessageTypeEnum : BaseEnum
    {
        public KoneRcgifMessageTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }
        /// <summary>
        /// 目的层呼梯 0x8000=32768
        /// </summary>
        public static KoneRcgifMessageTypeEnum MSG_DESTINATION_CALL => new KoneRcgifMessageTypeEnum(0x8000, "MSG_DESTINATION_CALL", "目的层呼梯");

        /// <summary>
        /// 心跳包 0x8002=32770
        /// </summary>
        public static KoneRcgifMessageTypeEnum MSG_HEARTBEAT => new KoneRcgifMessageTypeEnum(0x8002, "MSG_HEARTBEAT", "心跳包");
    }

    public class KoneRcgifApplicationIdEnum : BaseEnum
    {
        public KoneRcgifApplicationIdEnum(int code, string value, string text) : base(code, value, text)
        {

        }
        /// <summary>
        /// 目的层呼梯程序 0x8000=32768
        /// </summary>
        public static KoneRcgifApplicationIdEnum APP_DESTINATION_CALL => new KoneRcgifApplicationIdEnum(0x8000, "APP_DESTINATION_CALL", "目的层呼梯程序");
    }

    public class KoneRcgifApplicationMsgTypeEnum : BaseEnum
    {
        public KoneRcgifApplicationMsgTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static KoneRcgifApplicationMsgTypeEnum DESTINATION_CALL => new KoneRcgifApplicationMsgTypeEnum(1, "DESTINATION_CALL", "呼叫电梯发送数据");
        public static KoneRcgifApplicationMsgTypeEnum YOUR_TRANSPORTATION => new KoneRcgifApplicationMsgTypeEnum(2, "YOUR_TRANSPORTATION", "呼叫电梯返回结果");
        public static KoneRcgifApplicationMsgTypeEnum DISCONNECT => new KoneRcgifApplicationMsgTypeEnum(102, "DISCONNECT", "断开连接");
    }

    public class KoneRcgifMessageByteOrderEnum : BaseEnum
    {
        public KoneRcgifMessageByteOrderEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        /// <summary>
        /// This message is big-endian 0x80=128
        /// </summary>
        public static KoneRcgifMessageByteOrderEnum BYTE_ORDER_BIG_ENDIAN => new KoneRcgifMessageByteOrderEnum(0x80, "BYTE_ORDER_BIG_ENDIAN", "big-endian");

        /// <summary>
        /// This message is little-endian 0x81=129
        /// </summary>
        public static KoneRcgifMessageByteOrderEnum BYTE_ORDER_LITTLE_ENDIAN => new KoneRcgifMessageByteOrderEnum(0x81, "BYTE_ORDER_LITTLE_ENDIAN", "little-endian");

        /// <summary>
        /// Byte order of zero value is intentionally specified as undefined.The message is invalid
        /// </summary>
        public static KoneRcgifMessageByteOrderEnum BYTE_ORDER_UNDEFINED => new KoneRcgifMessageByteOrderEnum(0, "BYTE_ORDER_UNDEFINED", "invalid message");
    }

    public class KoneRcgifDisconnectReasonEnum : BaseEnum
    {
        public KoneRcgifDisconnectReasonEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        /// <summary>
        /// Unknown reason
        /// </summary>
        public static KoneRcgifDisconnectReasonEnum UNKNOWN_REASON => new KoneRcgifDisconnectReasonEnum(0, "UNKNOWN_REASON", "Unknown reason");

        /// <summary>
        /// Host shutting down
        /// </summary>
        public static KoneRcgifDisconnectReasonEnum HOST_SHUTTING_DOWN => new KoneRcgifDisconnectReasonEnum(1, "HOST_SHUTTING_DOWN", "Host shutting down");

        /// <summary>
        /// Session end
        /// </summary>
        public static KoneRcgifDisconnectReasonEnum SESSION_END => new KoneRcgifDisconnectReasonEnum(2, "SESSION_END", "Session end");

        /// <summary>
        /// No heartbeat
        /// </summary>
        public static KoneRcgifDisconnectReasonEnum NO_HEARTBEAT => new KoneRcgifDisconnectReasonEnum(3, "NO_HEARTBEAT", "No heartbeat");

        /// <summary>
        /// Too many invalid messages
        /// </summary>
        public static KoneRcgifDisconnectReasonEnum TOO_MANY_INVALID_MESSAGES => new KoneRcgifDisconnectReasonEnum(4, "TOO_MANY_INVALID_MESSAGES", "Too many invalid messages");

        /// <summary>
        /// Bandwidth abuse
        /// </summary>
        public static KoneRcgifDisconnectReasonEnum BANDWIDTH_ABUSE => new KoneRcgifDisconnectReasonEnum(5, "BANDWIDTH_ABUSE", "Bandwidth abuse");

    }

    /// <summary>
    /// Feedback to the passenger. Meaning: 
    /// 0  = No error
    /// 1  = The floor does not exist
    /// 2  = Enter is not pressed (local)
    /// 3  = The floor is locked
    /// 4  = The floor is not specified (local)
    /// 5  = The call is given to the same floor (local)
    /// 6  = Communication failure (local)
    /// 7  = Please wait
    /// 8  = Cannot serve now
    /// 9  = Backup group
    /// 50 = The terminal is not in use
    /// 51 = Invalid device type
    /// 52 = Invalid side
    /// 53 = Invalid call state
    /// 54 = Invalid number of passengers
    /// 55 = No elevators available
    /// 56 = access denied
    /// 57 = Invasion no service to floor
    /// 10… 49, 58 … 255 = Other response.
    /// </summary>
    public class KoneRcgifResponseCodeEnum : BaseEnum
    {
        public KoneRcgifResponseCodeEnum() : base()
        {

        }
        public KoneRcgifResponseCodeEnum(int code, string value, string text) : base(code, value, text)
        {

        }
        public static KoneRcgifResponseCodeEnum NoError => new KoneRcgifResponseCodeEnum(0, "NO_ERROR", "No error");
        public static KoneRcgifResponseCodeEnum TheFloorDoesNotExist => new KoneRcgifResponseCodeEnum(1, "THE_FLOOR_DOES_NOT_EXIST", "The floor does not exist");
        public static KoneRcgifResponseCodeEnum EnterIsNotPressedLocal => new KoneRcgifResponseCodeEnum(2, "ENTER_IS_NOT_PRESSED_LOCAL", "Enter is not pressed (local)");
        public static KoneRcgifResponseCodeEnum TheFloorIsLocked => new KoneRcgifResponseCodeEnum(3, "THE_FLOOR_IS_LOCKED", "The floor is locked");
        public static KoneRcgifResponseCodeEnum TheFloorIsNotSpecifiedLocal => new KoneRcgifResponseCodeEnum(4, "THE_FLOOR_IS_NOT_SPECIFIED_LOCAL", "The floor is not specified (local)");
        public static KoneRcgifResponseCodeEnum TheCallIsGivenToTheSameFloorLocal => new KoneRcgifResponseCodeEnum(5, "THE_CALL_IS_GIVEN_TO_THE_SAME_FLOOR_LOCAL", "The call is given to the same floor (local)");
        public static KoneRcgifResponseCodeEnum CommunicationFailureLocal => new KoneRcgifResponseCodeEnum(6, "COMMUNICATION_FAILURE_LOCAL", "Communication failure (local)");
        public static KoneRcgifResponseCodeEnum PleaseWait => new KoneRcgifResponseCodeEnum(7, "PLEASE_WAIT", "Please wait");
        public static KoneRcgifResponseCodeEnum CannotServeNow => new KoneRcgifResponseCodeEnum(8, "CANNOT_SERVE_NOW", "Cannot serve now");
        public static KoneRcgifResponseCodeEnum BackupGroup => new KoneRcgifResponseCodeEnum(9, "BACKUP_GROUP", "Backup group");
        public static KoneRcgifResponseCodeEnum TheTerminalIsNotInUse => new KoneRcgifResponseCodeEnum(50, "THE_TERMINAL_IS_NOT_IN_USE", "The terminal is not in use");
        public static KoneRcgifResponseCodeEnum InvalidDeviceType => new KoneRcgifResponseCodeEnum(51, "INVALID_DEVICE_TYPE", "Invalid device type");
        public static KoneRcgifResponseCodeEnum InvalidSide => new KoneRcgifResponseCodeEnum(52, "INVALID_SIDE", "Invalid side");
        public static KoneRcgifResponseCodeEnum InvalidCallState => new KoneRcgifResponseCodeEnum(53, "INVALID_CALL_STATE", "Invalid call state");
        public static KoneRcgifResponseCodeEnum InvalidNumberOfPassengers => new KoneRcgifResponseCodeEnum(54, "INVALID_NUMBER_OF_PASSENGERS", "Invalid number of passengers");
        public static KoneRcgifResponseCodeEnum NoElevatorsAvailable => new KoneRcgifResponseCodeEnum(55, "NO_ELEVATORS_AVAILABLE", "No elevators available");
        public static KoneRcgifResponseCodeEnum AccessDenied => new KoneRcgifResponseCodeEnum(56, "ACCESS_DENIED", "access denied");
        public static KoneRcgifResponseCodeEnum InvasionNoServiceToFloor => new KoneRcgifResponseCodeEnum(57, "INVASION_NO_SERVICE_TO_FLOOR", "Invasion no service to floor");
    }
}
