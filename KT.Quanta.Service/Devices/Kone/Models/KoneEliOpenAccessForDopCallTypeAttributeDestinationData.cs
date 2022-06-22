using KT.Quanta.Service.Devices.Kone.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Models
{
    /// <summary>
    /// "Destinations" Attribute (id = 1)
    /// Type                        Name of the field           Explanation
    /// uint8                       attribute_id = 1            Attribute id number for Destinations
    /// uint16                      number_of_opens             Number of open events.
    /// uint32[number_of_opens]     open_events                 bit 0-15: allowed destination floor (uint16)
    ///                                                         bit 16-23 : reserved
    ///                                                         bit 24 : allowed destination front (1=yes, 0=no)
    ///                                                         bit 25 : allowed destination rear (1=yes, 0=no)
    ///                                                         bit 26 - 31: reserved
    /// </summary>
    public class KoneEliOpenAccessForDopCallTypeAttributeDestinationData : KoneSerializer
    {
        public KoneEliOpenAccessForDopCallTypeAttributeDestinationData()
        {
            OpenEvents = new List<KoneEliOpenAccessForDopMessageOpenEventData>();
        }

        /// <summary>
        /// Attribute id number for Destinations
        /// </summary>
        public byte AttributeId { get; set; } = 1;

        /// <summary>
        /// Number of open events.
        /// </summary>
        public ushort NumberOfOpens { get; private set; }

        /// <summary>
        /// uint32[number_of_opens]
        /// 
        /// bit 0-15: allowed destination floor (uint16)
        /// bit 16-23 : reserved
        /// bit 24 : allowed destination front(1=yes, 0=no)
        /// bit 25 : allowed destination rear(1=yes, 0=no)
        /// bit 26 - 31: reserved
        /// </summary>
        public List<KoneEliOpenAccessForDopMessageOpenEventData> OpenEvents { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            AttributeId = ReadByte();
            NumberOfOpens = ReadUnsignedShort();
            OpenEvents = ReadList<KoneEliOpenAccessForDopMessageOpenEventData>(NumberOfOpens, isLittleEndianess);
        }

        protected override void Write(bool isLittleEndianess)
        {
            NumberOfOpens = (ushort)OpenEvents.Count;

            WriteByte(AttributeId);
            WriteUnsignedShort(NumberOfOpens);
            WriteList(OpenEvents);
        }

    }
}
