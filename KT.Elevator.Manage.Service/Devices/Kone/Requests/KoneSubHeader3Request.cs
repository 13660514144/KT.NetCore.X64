using KT.Elevator.Manage.Service.Devices.Kone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Kone.Requests
{
    /// <summary>
    /// Message header structure for koneSubHeader3
    /// </summary>
    public class KoneSubHeader3Request : KoneHeaderRequest
    {
        /// <summary>
        /// The time when the message is sent.Time stamp type is 
        /// windows FILETIME(units of 100ns since January 1,
        /// 1601 UTC ) as local time
        /// </summary>
        public virtual ulong TimeStamp { get; set; }
    }
}
