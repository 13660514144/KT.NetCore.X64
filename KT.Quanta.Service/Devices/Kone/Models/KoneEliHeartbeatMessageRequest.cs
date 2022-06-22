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
    /// 
    /// error data : 129,17,0,0,0,3,128,2,128,16,175,161,165,65,238,214,1:81 11 00 00 00 03 80 02 80 10 AF A1 A5 41 EE D6 01 
    /// ture data  : 129,17,0,0,0,2,128,2,128,208,24,36,13,66,238,214,1  :81 11 00 00 00 02 80 02 80 D0 18 24 0D 42 EE D6 01
    /// </summary>
    public class KoneEliHeartbeatMessageRequest : KoneEliSubHeader3Request
    {
        public KoneEliHeartbeatMessageRequest()
        {
            ApplicationId = (ushort)KoneEliApplicationIdEnum.APP_ACCESS_CONTROL.Code;
            //Header
            MessageType = (ushort)KoneEliMessageTypeEnum.MSG_HEARTBEAT.Code; 
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
