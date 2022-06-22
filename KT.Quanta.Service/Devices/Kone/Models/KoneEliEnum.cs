using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    public class KoneEliMessageTypeEnum : BaseEnum
    {
        public KoneEliMessageTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }
        /// <summary>
        /// 目的层呼梯程序 0x8003=32,771
        /// </summary>
        public static KoneEliMessageTypeEnum MSG_ACCESS_CONTROL => new KoneEliMessageTypeEnum(0x8003, "MSG_ACCESS_CONTROL", "目的层呼梯程序");

        /// <summary>
        /// 心跳包 0x8002=32770
        /// </summary>
        public static KoneEliMessageTypeEnum MSG_HEARTBEAT => new KoneEliMessageTypeEnum(0x8002, "MSG_HEARTBEAT", "心跳包");
    }

    public class KoneEliApplicationIdEnum : BaseEnum
    {
        public KoneEliApplicationIdEnum(int code, string value, string text) : base(code, value, text)
        {

        }
        /// <summary>
        /// 多楼层呼梯程序 0x8002=32770 
        /// </summary>
        public static KoneEliApplicationIdEnum APP_ACCESS_CONTROL => new KoneEliApplicationIdEnum(0x8002, "APP_ACCESS_CONTROL", "多楼层呼梯程序");
    }

    public class KoneEliApplicationMsgTypeEnum : BaseEnum
    {
        public KoneEliApplicationMsgTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static KoneEliApplicationMsgTypeEnum DOP_GLOBAL_DEFAULT_MASK => new KoneEliApplicationMsgTypeEnum(1, "DOP_GLOBAL_DEFAULT_MASK", "发送Global Mask");
        public static KoneEliApplicationMsgTypeEnum COP_GLOBAL_DEFAULT_MASK => new KoneEliApplicationMsgTypeEnum(2, "COP_GLOBAL_DEFAULT_MASK", "发送COP Mask");
        public static KoneEliApplicationMsgTypeEnum DOP_DEFAULT_MASK => new KoneEliApplicationMsgTypeEnum(3, "DOP_DEFAULT_MASK", "发送DOP Mask");
        public static KoneEliApplicationMsgTypeEnum DOP_DEFAULT_MASK_WITH_CALL_TYPES => new KoneEliApplicationMsgTypeEnum(10, "DOP_DEFAULT_MASK_WITH_CALL_TYPES", "Dop Mask");
        public static KoneEliApplicationMsgTypeEnum OPEN_DOP => new KoneEliApplicationMsgTypeEnum(4, "OPEN_DOP", "打开DOP");
        public static KoneEliApplicationMsgTypeEnum OPEN_DOP_WITH_CALL_TYPES => new KoneEliApplicationMsgTypeEnum(11, "OPEN_DOP_WITH_CALL_TYPES", "打开DOP");
        public static KoneEliApplicationMsgTypeEnum COP_DEFAULT_MASK => new KoneEliApplicationMsgTypeEnum(5, "COP_DEFAULT_MASK", "COP Mask");
        public static KoneEliApplicationMsgTypeEnum OPEN_COP => new KoneEliApplicationMsgTypeEnum(6, "OPEN_COP", "打开COP");
        public static KoneEliApplicationMsgTypeEnum DOP_CLOSED => new KoneEliApplicationMsgTypeEnum(7, "DOP_CLOSED", "断开DOP连接");
        public static KoneEliApplicationMsgTypeEnum COP_CLOSED => new KoneEliApplicationMsgTypeEnum(8, "COP_CLOSED", "断开COP连接");
        public static KoneEliApplicationMsgTypeEnum DISCONNECT => new KoneEliApplicationMsgTypeEnum(102, "DISCONNECT", "断开连接");
        public static KoneEliApplicationMsgTypeEnum STATUS_RESPONSE => new KoneEliApplicationMsgTypeEnum(150, "STATUS_RESPONSE", "状态返回");
        public static KoneEliApplicationMsgTypeEnum DOP_MESSAGE => new KoneEliApplicationMsgTypeEnum(9, "DOP_MESSAGE", "DOP消息");
    }

    public class KoneEliMessageByteOrderEnum : BaseEnum
    {
        public KoneEliMessageByteOrderEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        /// <summary>
        /// This message is big-endian 0x80=128
        /// </summary>
        public static KoneEliMessageByteOrderEnum BYTE_ORDER_BIG_ENDIAN => new KoneEliMessageByteOrderEnum(0x80, "BYTE_ORDER_BIG_ENDIAN", "big-endian");

        /// <summary>
        /// This message is little-endian 0x81=129
        /// </summary>
        public static KoneEliMessageByteOrderEnum BYTE_ORDER_LITTLE_ENDIAN => new KoneEliMessageByteOrderEnum(0x81, "BYTE_ORDER_LITTLE_ENDIAN", "little-endian");

        /// <summary>
        /// Byte order of zero value is intentionally specified as undefined.The message is invalid
        /// </summary>
        public static KoneEliMessageByteOrderEnum BYTE_ORDER_UNDEFINED => new KoneEliMessageByteOrderEnum(0, "BYTE_ORDER_UNDEFINED", "invalid message");
    }

    public class KoneEliDisconnectReasonEnum : BaseEnum
    {
        public KoneEliDisconnectReasonEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        /// <summary>
        /// Unknown reason
        /// </summary>
        public static KoneEliDisconnectReasonEnum UNKNOWN_REASON => new KoneEliDisconnectReasonEnum(0, "UNKNOWN_REASON", "Unknown reason");

        /// <summary>
        /// Host shutting down
        /// </summary>
        public static KoneEliDisconnectReasonEnum HOST_SHUTTING_DOWN => new KoneEliDisconnectReasonEnum(1, "HOST_SHUTTING_DOWN", "Host shutting down");

        /// <summary>
        /// Session end
        /// </summary>
        public static KoneEliDisconnectReasonEnum SESSION_END => new KoneEliDisconnectReasonEnum(2, "SESSION_END", "Session end");

        /// <summary>
        /// No heartbeat
        /// </summary>
        public static KoneEliDisconnectReasonEnum NO_HEARTBEAT => new KoneEliDisconnectReasonEnum(3, "NO_HEARTBEAT", "No heartbeat");

        /// <summary>
        /// Too many invalid messages
        /// </summary>
        public static KoneEliDisconnectReasonEnum TOO_MANY_INVALID_MESSAGES => new KoneEliDisconnectReasonEnum(4, "TOO_MANY_INVALID_MESSAGES", "Too many invalid messages");

        /// <summary>
        /// Bandwidth abuse
        /// </summary>
        public static KoneEliDisconnectReasonEnum BANDWIDTH_ABUSE => new KoneEliDisconnectReasonEnum(5, "BANDWIDTH_ABUSE", "Bandwidth abuse");

    }

    public class KoneEliIsTimeoutExceededEnum : BaseEnum
    {
        public KoneEliIsTimeoutExceededEnum()
        {
        }

        public KoneEliIsTimeoutExceededEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static KoneEliIsTimeoutExceededEnum No => new KoneEliIsTimeoutExceededEnum(0, "No", "");
        public static KoneEliIsTimeoutExceededEnum Yes => new KoneEliIsTimeoutExceededEnum(1, "Yes", "楼层选择超时");
        public static KoneEliIsTimeoutExceededEnum AccessDenied => new KoneEliIsTimeoutExceededEnum(2, "ACCESS_DENIED", "楼层无权限");

    }

    public class KoneEliStatusEnum : BaseEnum
    {
        public KoneEliStatusEnum()
        {
        }

        public KoneEliStatusEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static KoneEliStatusEnum OK => new KoneEliStatusEnum(0, "OK", "OK");
        public static KoneEliStatusEnum BackupGroup => new KoneEliStatusEnum(1, "BACKUP_GROUP", "Backup group.");
        public static KoneEliStatusEnum IllegalDopId => new KoneEliStatusEnum(2, "ILLEGAL_DOP_ID", "Illegal DOP ID");
        public static KoneEliStatusEnum IllegalDopFloor => new KoneEliStatusEnum(3, "ILLEGAL_DOP_FLOOR", "Illegal DOP floor");
        public static KoneEliStatusEnum IllegalNumberOfFloors => new KoneEliStatusEnum(4, "ILLEGAL_NUMBER_OF_FLOORS", "Illegal number of floors");
        public static KoneEliStatusEnum IllegalTimeout => new KoneEliStatusEnum(5, "ILLEGAL_TIMEOUT", "Illegal timeout");
        public static KoneEliStatusEnum IllegalNumberOfCalls => new KoneEliStatusEnum(6, "ILLEGAL_NUMBER_OF_CALLS", "Illegal number of calls");
        public static KoneEliStatusEnum IllegalSystemState => new KoneEliStatusEnum(7, "ILLEGAL_SYSTEM_STATE", "Illegal system state.");
        public static KoneEliStatusEnum IllegalValue => new KoneEliStatusEnum(8, "ILLEGAL_VALUE", "Illegal value.");
        public static KoneEliStatusEnum MessageNotAccepted => new KoneEliStatusEnum(9, "MESSAGE_NOT_ACCEPTED", "Message not accepted.");

    }

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

    public class KoneEliMaskRecoredOperateEnum : BaseEnum
    {
        public KoneEliMaskRecoredOperateEnum()
        {
        }

        public KoneEliMaskRecoredOperateEnum(int code, string value, string text) : base(code, value, text)
        {
        }
        public static KoneEliMaskRecoredOperateEnum Send => new KoneEliMaskRecoredOperateEnum(1, "SEND", "发送");
        public static KoneEliMaskRecoredOperateEnum Receive => new KoneEliMaskRecoredOperateEnum(2, "RECEIVE", "接收");
    }
}
