using KT.Quanta.Service.Devices.Kone.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Models
{
    /// <summary>
    /// "Lifts" attribute (id = 2)
    /// Type                        Name of the field           Explanation
    /// uint8                       attribute_id = 2            Attribute id number lifts attribute. 
    /// uint8                       number_of_lifts             The number of lift ids which are allowed for call. If zero the 
    ///                                                         all lifts are allowed.
    /// uint8[number of lifts]      lift_id                     Allowed lift id of lift group for the call. Minimum value is 
    ///                                                         one (value >= 1).
    /// </summary>
    public class KoneEliOpenAccessForDopCallTypeAttributeLiftData : KoneSerializer
    {
        /// <summary>
        /// Attribute id number lifts attribute.
        /// </summary>
        public byte AttributeId { get; set; } = 2;

        /// <summary>
        /// The number of lift ids which are allowed for call. If zero the 
        /// all lifts are allowed.
        /// </summary>
        public byte NumberOfLifts { get; private set; }

        /// <summary>
        /// uint8[number oflifts]
        /// 
        /// Allowed lift id of lift group for the call. Minimum value is 
        /// one(value >= 1).
        /// </summary>
        public List<byte> LiftIds { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            AttributeId = ReadByte();
            NumberOfLifts = ReadByte();
            LiftIds = ReadBytes(NumberOfLifts).ToList();
        }

        protected override void Write(bool isLittleEndianess)
        {
            NumberOfLifts = (byte)LiftIds.Count;

            WriteByte(AttributeId);
            WriteByte(NumberOfLifts);
            WriteBytes(LiftIds.ToArray());
        }
    }
}
