using KT.Quanta.Service.Devices.Kone.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Models
{
    /// <summary>
    /// Status response
    /// The message type of dop access denied message MSG_ACCESS_CONTROL (0x8003) and it uses koneSubHeader1:
    /// 9 (DOP_MESSAGE)
    /// </summary>
    public class KoneEliDopMessageResponse : KoneSerializer
    {
        /// <summary>
        /// ID of the DOP
        /// </summary>
        public byte DopId { get; set; }

        /// <summary>
        /// Floor of the DOP ( floor minimum value is 1 )
        /// </summary>
        public ushort DopFloorId { get; set; }

        /// <summary>
        /// DOP message id : 
        /// 1 : access denied message
        /// other id:s are currently not supported.
        /// </summary>
        public ushort DopMessageId { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            DopId = ReadByte();
            DopFloorId = ReadUnsignedShort();
            DopMessageId = ReadUnsignedShort();
        }

        protected override void Write(bool isLittleEndianess)
        {
            WriteByte(DopId);
            WriteUnsignedShort(DopFloorId);
            WriteUnsignedShort(DopMessageId);
        }
    }
}
