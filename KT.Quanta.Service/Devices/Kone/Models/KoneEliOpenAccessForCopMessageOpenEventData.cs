using BigMath;
using BigMath.Utils;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    /// <summary>
    /// Open Access For Cop Message Open Event Data
    /// bit 0-15: allowed destination floor (uint16)
    /// bit 16-23 : reserved
    /// bit 24 : allowed destination front(1=yes, 0=no)
    /// bit 25 : allowed destination rear(1=yes, 0=no)
    /// bit 26 - 31: reserved
    /// </summary>

    public class KoneEliOpenAccessForCopMessageOpenEventData : KoneSerializer
    {
        public KoneEliOpenAccessForCopMessageOpenEventData()
        {

        }

        /// <summary>
        /// bit 0-15: allowed destination floor (uint16)
        /// </summary>
        public ushort DestinationFloor { get; set; }

        /// <summary>
        /// bit 16-23 : reserved
        /// </summary>
        public byte Reserved { get; set; }

        /// <summary>
        /// 0b00000011 = 3
        /// bit 24 : allowed destination front(1=yes, 0=no)
        /// bit 25 : allowed destination rear(1=yes, 0=no)
        /// bit 26 - 31: reserved
        /// </summary>
        public byte OpenAccess { get; set; } = 3;

        protected override void Read(bool isLittleEndianess)
        {
            DestinationFloor = ReadUnsignedShort();
            Reserved = ReadByte();
            OpenAccess = ReadByte();
        }

        protected override void Write(bool isLittleEndianess)
        {
            WriteUnsignedShort(DestinationFloor);
            WriteByte(Reserved);
            WriteByte(OpenAccess);
        }
    }
}
