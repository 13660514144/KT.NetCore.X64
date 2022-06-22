using KT.Quanta.Service.Devices.Kone.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Models
{
    /// <summary>
    /// call_type_data data structure:
    /// </summary>
    public class KoneEliDopSpecificDefaultAccessMaskMessageCallTypeData : KoneSerializer
    {
        /// <summary>
        /// 0-200 : specifies the call type number
        /// </summary>
        public byte CallType { get; set; }

        /// <summary>
        /// Number of floors in the mask
        /// </summary>
        public ushort NumberOfFloors { get; set; }

        /// <summary>
        /// uint32[number_of_floors] //0b00000011=3
        /// bit 0 : allowed destination front (1=yes, 0=no)
        /// bit 1 : allowed destination rear(1=yes, 0=no)
        /// bit 2 -31 : reserved
        /// </summary>
        public List<uint> AccessMasks { get; private set; }

        protected override void Read(bool isLittleEndianess)
        {
            CallType = ReadByte();
            NumberOfFloors = ReadUnsignedShort();
            AccessMasks = ReadUnsignedInts(NumberOfFloors);
        }

        protected override void Write(bool isLittleEndianess)
        {
            WriteByte(CallType);
            WriteUnsignedShort(NumberOfFloors);

            //Mask楼层前后门权限
            AccessMasks = new List<uint>();
            for (int i = 0; i < NumberOfFloors; i++)
            {
                //0b00000011=3
                AccessMasks.Add(3);
            }
            WriteUnsignedInts(AccessMasks);
        }
    }
}
