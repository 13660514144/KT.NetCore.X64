using KT.Quanta.Service.Devices.Kone.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Models
{
    /// <summary>
    /// Status response
    /// The message type of status response message MSG_ACCESS_CONTROL (0x8003) and it uses koneSubHeader1:
    /// 150 (STATUS_RESPONSE)
    /// 
    /// 81 1F 00 00 00 02 80 03 80 96 00 01 01 39 02 00 00 07 00 00 00 80 77 B6 88 AC 2A D7 01 00 00
    /// </summary>
    public class KoneEliStatusResponse : KoneSerializer
    {
        /// <summary>
        /// 0 = OK
        /// 1 = Backup group.
        /// 2 = Illegal DOP ID
        /// 3 = Illegal DOP floor
        /// 4 = Illegal number of floors
        /// 5 = Illegal timeout
        /// 6 = Illegal number of calls
        /// 7 = Illegal system state.
        /// 8 = Illegal value.
        /// 9 = Message not accepted.
        /// </summary>
        public ushort ResponseCode { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            ResponseCode = ReadUnsignedShort();
        }

        protected override void Write(bool isLittleEndianess)
        {
            WriteUnsignedShort(ResponseCode);
        }
    }
}
