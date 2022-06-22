using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    /// <summary>
    /// The message type of heartbeat message is MSG_HEARTBEAT (0x8002) and it uses 
    /// koneSubHeader3:
    /// 
    /// timeStamp: The time when the message is sent.Time stamp type is 
    ///          windows FILETIME(units of 100ns since January 1,
    ///          1601 UTC ) as local time
    /// 
    /// There is no message data.
    /// </summary>
    public class KoneRcgifHeartbeatMessageRequest : KoneRcgifSubHeader3Request
    {
        public KoneRcgifHeartbeatMessageRequest()
        {
            MessageSize = 17;

            ApplicationId = (ushort)KoneRcgifApplicationIdEnum.APP_DESTINATION_CALL.Code;
            //Header
            MessageType = (ushort)KoneRcgifMessageTypeEnum.MSG_HEARTBEAT.Code; 
        }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);
        }

        protected override void Write(bool isLittleEndianess)
        {
            base.Write(isLittleEndianess);
        }
    }
}
