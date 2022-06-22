using KT.Quanta.Service.Devices.Kone.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Models
{
    /// <summary>
    /// call_type_data structure: 
    /// demo real data      :00 FF 01   13 00 01 04 00 03 00 00 03 0A 00 00 03 0B 00 00 03 0C 00 00 03
    /// own real error data :00 FF 01   04 00 03 00 00 03 0B 00 00 03 0A 00 00 03 0C 00 00 03
    /// </summary>
    public class KoneEliOpenAccessForDopCallTypeData : KoneSerializer
    {
        /// <summary>
        /// Call type number which selects the lift system functionality
        /// </summary>
        public byte CallType { get; set; }

        /// <summary>
        ///  Reserved, value must be 255.
        /// </summary>
        public byte CallTypeOverride { get; set; } = 255;

        /// <summary>
        /// The number of attributes for the call type.
        /// 
        /// "Destinations" Attribute (id = 1)
        /// Type                        Name of the field           Explanation
        /// uint8                       attribute_id = 1            Attribute id number for Destinations
        /// uint16                      number_of_opens             Number of open events.
        /// uint32[number_of_opens]     open_events                 bit 0-15: allowed destination floor (uint16)
        ///                                                         bit 16-23 : reserved
        ///                                                         bit 24 : allowed destination front (1=yes, 0=no)
        ///                                                         bit 25 : allowed destination rear (1=yes, 0=no)
        ///                                                         bit 26 - 31: reserved
        /// 
        /// "Lifts" attribute (id = 2)
        /// Type                        Name of the field           Explanation
        /// uint8                       attribute_id = 2            Attribute id number lifts attribute. 
        /// uint8                       number_of_lifts             The number of lift ids which are allowed for call. If zero the 
        ///                                                         all lifts are allowed.
        /// uint8[number of lifts]      lift_id                     Allowed lift id of lift group for the call. Minimum value is 
        ///                                                         one (value >= 1).
        /// </summary>
        public byte NumberOfAttributes { get; set; }

        /// <summary>
        /// Length of the attribute_data in bytes
        /// </summary>
        public ushort AttributeDataLenght { get; private set; }

        /// <summary>
        /// Data of the attribute. See attribute definitions below
        /// 
        /// Allowed Call type attribute combinations:
        /// number of call types    Attributes          Allowe      Functionality
        /// 0                       -                   yes         The call type of the next call from DOP is changed.
        /// 0                       Destinations        yes         The call type of the next call from DOP is 
        ///                                                         changed. The call destination floor is also 
        ///                                                         checked.
        /// 0                       Lifts               yes         The call type of the next call from DOP is 
        ///                                                         changed. The allowed lifts for call 
        ///                                                         allocation are defined.
        /// 0                       Destinations,       yes         he call type of the next call from DOP is 
        ///                         Lifts                           changed. The call destination floor is 
        ///                                                         checked and allowed lifts for the call are 
        ///                                                         defined.
        /// >=1                     -                   no          This combination is not valid.
        /// >=1                     Lifts               yes         The allowed lifts for the next user 
        ///                                                         selected call type is defined
        /// >=1                     Destinations        yes         The destination floor is checked for the 
        ///                                                         next user selected call type.
        /// >=1                     Destinations,       yes         The destination floor is checked and 
        ///                         Lifts                           allowed lifts of allocation is defined for 
        ///                                                         the next user selected call type.      
        ///                                                 
        /// Note: one attribute can exist once for one call type! 
        /// </summary>
        public List<byte> AttributeDatas { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            CallType = ReadByte();
            CallTypeOverride = ReadByte();
            NumberOfAttributes = ReadByte();
            AttributeDataLenght = ReadUnsignedShort();
            if (NumberOfAttributes == 1)
            {
                var data = ReadObject<KoneEliOpenAccessForDopCallTypeAttributeDestinationData>(isLittleEndianess);
                AttributeDatas = data.GetBytes(isLittleEndianess).ToList();
            }
            else if (NumberOfAttributes == 2)
            {
                var data = ReadObject<KoneEliOpenAccessForDopCallTypeAttributeLiftData>(isLittleEndianess);
                AttributeDatas = data.GetBytes(isLittleEndianess).ToList();
            }
            else
            {
                throw new AggregateException($"通力电梯找不到NumberOfAttributes:{NumberOfAttributes} ");
            }
        }

        protected override void Write(bool isLittleEndianess)
        {
            if (AttributeDatas != null)
            {
                AttributeDataLenght = (ushort)AttributeDatas.Count;
            }

            WriteByte(CallType);
            WriteByte(CallTypeOverride);
            WriteByte(NumberOfAttributes);
            WriteUnsignedShort(AttributeDataLenght);
            WriteBytes(AttributeDatas.ToArray());
        }
    }
}



///// <summary>
///// The number of attributes for the call type.
///// </summary>
//public byte NumberOfAttributes { get; set; }

///// <summary>
///// Length of the attribute_data in bytes
///// </summary>
//public ushort AttributeDataLenght { get; set; }

///// <summary>
///// Data of the attribute. See attribute definitions below
///// 
///// Allowed Call type attribute combinations:
///// number of call types    Attributes          Allowe      Functionality
///// 0                       -                   yes         The call type of the next call from DOP is changed.
///// 0                       Destinations        yes         The call type of the next call from DOP is 
/////                                                         changed. The call destination floor is also 
/////                                                         checked.
///// 0                       Lifts               yes         The call type of the next call from DOP is 
/////                                                         changed. The allowed lifts for call 
/////                                                         allocation are defined.
///// 0                       Destinations,       yes         he call type of the next call from DOP is 
/////                         Lifts                           changed. The call destination floor is 
/////                                                         checked and allowed lifts for the call are 
/////                                                         defined.
///// >=1                     -                   no          This combination is not valid.
///// >=1                     Lifts               yes         The allowed lifts for the next user 
/////                                                         selected call type is defined
///// >=1                     Destinations        yes         The destination floor is checked for the 
/////                                                         next user selected call type.
///// >=1                     Destinations,       yes         The destination floor is checked and 
/////                         Lifts                           allowed lifts of allocation is defined for 
/////                                                         the next user selected call type.      
/////                                                 
///// Note: one attribute can exist once for one call type! 
///// </summary>
//public List<byte> AttributeDatas { get; set; }
