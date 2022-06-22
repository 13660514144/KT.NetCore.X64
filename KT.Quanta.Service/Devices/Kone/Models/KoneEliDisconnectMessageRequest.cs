using KT.Quanta.Service.Devices.Kone.Helpers;
using KT.Quanta.Service.Devices.Kone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    /// <summary>
    /// The message type of disconnect message is MSG_DESTINATION_CALL (0x8000) and 
    /// it uses koneSubHeader1:
    /// 
    /// applicationMsgType: 102 (DISCONNECT)
    /// protocolVersion: 1
    /// messageID: Unique message ID by the sender point of view.
    ///         0x00000000 is undefined and shall not be used as 
    ///         messageID by any application.
    /// responseID: If this message is a response to another message, the
    ///         other message’s ID is set to this field.ResponseID of
    ///         value 0x00000000 is defined and means that the
    ///         message is not a response to another message.
    /// timeStamp: The time when the message is sent.Time stamp type is 
    ///         windows FILETIME (units of 100ns since January 1,
    ///         1601 UTC ) as local time
    ///         实际返回结果不包含koneSubHeader1 //KoneEliHeaderRequest//
    /// 模拟器：81 1F 00 00 00 02 80 03 80 66 00 01 01 1F 00 00 00 00 00 00 00 40 A5 1D 59 E8 1E D7 01 01 00
    /// 81 1F 00 00 00 02 80 03 80 66 00 01 01 04 00 00 00 00 00 00 00 80 7D F6 7B E3 1E D7 01 01 00
    /// </summary>
    public class KoneEliDisconnectMessageRequest : KoneEliSubHeader1Request
    {
        public KoneEliDisconnectMessageRequest()
        {
            ApplicationId = (ushort)KoneEliApplicationIdEnum.APP_ACCESS_CONTROL.Code;
            MessageType = (ushort)KoneEliMessageTypeEnum.MSG_ACCESS_CONTROL.Code;
            ApplicationMsgType = (ushort)KoneEliApplicationMsgTypeEnum.DISCONNECT.Code;
            MajorVersion = 1;
            MinorVersion = 1;

            MessageId = KoneEliHelper.GetMessageId();
        }

        /// <summary>
        /// 0 = Unknown reason
        /// 1 = Host shutting down
        /// 2 = Session end
        /// 3 = No heartbeat
        /// 4 = Too many invalid messages
        /// 5 = Bandwidth abuse
        /// 6 = Unsupported protocol version
        /// 7 = configuration mismatch
        /// </summary>
        public ushort Reason { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            Reason = ReadUnsignedShort();
        }

        protected override void Write(bool isLittleEndianess)
        {
            base.Write(isLittleEndianess);

            WriteUnsignedShort(Reason);
        }
    }
}









