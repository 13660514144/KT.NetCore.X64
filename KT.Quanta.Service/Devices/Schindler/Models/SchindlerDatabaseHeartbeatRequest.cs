using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    /// <summary>
    /// "I am Alive" Message
    /// If no data transfer occurrs for 160 seconds, the third-party system sends an I am alive message.
    /// The I am alive message has no parameters.
    /// The server system acknowledges this message with the function type 01.
    /// The acknowledgement (ACK) message has no parameters.
    /// This message can be used to detect a broken link.
    /// I am alive message (function type 00):
    /// </summary>
    public class SchindlerDatabaseHeartbeatRequest : SchindlerTelegramHeaderRequest
    {
        /// <summary>
        /// 构造函数初始数据
        /// </summary>
        public SchindlerDatabaseHeartbeatRequest()
        {
            FunctionType = SchindlerDatabaseMessageTypeEnum.HEARTBEAT_REQUEST.Code;
        }
    }
}
