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
    /// </summary>
    public class KoneEliDisconnectMessageResponse : KoneSerializer // KoneEliSubHeader1Response
    {
        public KoneEliDisconnectMessageResponse()
        {

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
            //base.Read();

            Reason = ReadUnsignedShort();
        }

        protected override void Write(bool isLittleEndianess)
        {
            //base.Write();

            WriteUnsignedShort(Reason);
        }
    }
}









