using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Kone.Requests
{
    /// <summary>
    /// The message type of heartbeat message is MSG_HEARTBEAT (0x8002) and it uses 
    /// koneSubHeader3:
    /// 
    /// timeStamp: The time when the message is sent.Time stamp type is 
    ///          windows FILETIME(units of 100ns since January 1,
    ///          1601 UTC ) as local time
    /// 
    /// </summary>
    public class KoneHeartbeatMessage : KoneSubHeader3Request
    {
        public KoneHeartbeatMessage()
        {
            MessageType = 8002;
        }
    }
}
