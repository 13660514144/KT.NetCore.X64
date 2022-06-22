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
    /// Open Access For Dop Message Open Event Data
    /// bit 0-15: allowed destination floor (uint16)
    /// bit 16-23 : reserved
    /// bit 24 : allowed destination front(1=yes, 0=no)
    /// bit 25 : allowed destination rear(1=yes, 0=no)
    /// bit 26 - 31: reserved
    /// 3、10、11、12楼
    /// 04 00 03 00 00 03 0A 00 00 03 0B 00 00 03 0C 00 00 03
    /// 
    /// 14、15楼前门
    /// 81 3C 00 00 00 02 80 03 80 04 00 01 01 02 00 00 00 00 00 00 00 00 83 9F 76 AA 38 D7 01 01 17 00 98 3A A2 20 BC 1C 00 00 00 00 00 00 00 00 00 00 00 00 02 00 0E 00 00 02 0F 00 00 02
    /// 80 00 00 00 3C 80 02 80 03 00 04 01 01 00 00 00 01 00 00 00 00 01 D7 38 A9 FD 2F 1A 30 01 00 17 3A 98 00 00 00 00 00 00 00 00 00 00 00 00 1C BC 20 A2 00 02 02 00 00 0E 02 00 00 0F
    /// </summary>

    public class KoneEliOpenAccessForDopMessageOpenEventData : KoneSerializer
    {
        public KoneEliOpenAccessForDopMessageOpenEventData()
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

        ///// <summary>
        ///// 0b00000011 = 3
        ///// bit 24 : allowed destination front(1=yes, 0=no)
        ///// bit 25 : allowed destination rear(1=yes, 0=no)
        ///// bit 26 - 31: reserved
        ///// </summary>
        //public byte OpenAccess { get; set; } = 3;

        public bool IsFront { get; set; }
        public bool IsRear { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            DestinationFloor = ReadUnsignedShort();
            Reserved = ReadByte();
            var openAccess = Convert.ToInt32(ReadByte()).ToString().TrimStart('0').PadLeft(2, '0');
            if (openAccess.Substring(0, 1) == "1")
            {
                IsRear = true;
            }
            else
            {
                IsRear = false;
            } 
            if (openAccess.Substring(1, 1) == "1")
            {
                IsFront = true;
            }
            else
            {
                IsFront = false;
            }
        }

        protected override void Write(bool isLittleEndianess)
        {
            if (isLittleEndianess)
            {
                WriteUnsignedShort(DestinationFloor);
                WriteByte(Reserved);
                var front = IsFront ? "1" : "0";
                var rear = IsRear ? "1" : "0";
                var bitValue = $"000000{rear}{front}";
                WriteByte((byte)Convert.ToInt32(bitValue, 2));
            }
            else
            {
                var front = IsFront ? "1" : "0";
                var rear = IsRear ? "1" : "0";
                var bitValue = $"000000{rear}{front}";
                WriteByte((byte)Convert.ToInt32(bitValue, 2));
                WriteByte(Reserved);
                WriteUnsignedShort(DestinationFloor);
            } 
        }
    }
}
