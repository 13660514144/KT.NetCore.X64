using KT.Quanta.Service.Devices.Kone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    /// <summary>
    /// Message header structure for koneSubHeader3
    /// </summary>
    public class KoneEliSubHeader3Response : KoneSerializer
    {
        /// <summary>
        /// The time when the message is sent.Time stamp type is 
        /// windows FILETIME(units of 100ns since January 1,
        /// 1601 UTC ) as local time
        /// </summary>
        public virtual ulong TimeStamp { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            TimeStamp = ReadUnsignedLong();
        }

        protected override void Write(bool isLittleEndianess)
        {
            WriteUnsignedLong(TimeStamp);
        }
    }
}
