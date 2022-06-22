using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models
{
    /// <summary>
    /// Verification acceptance data 
    /// 
    /// ELSGW has memory of elevator bank number, device number and sequence number 
    /// which are set under elevator's call data and set these data.
    /// 
    /// 样例：17 30 00 08 11 01 01 01 00 00 00 00 81 06 00 00 FF FF 01 00
    ///       17 30 00 08 01 01 11 01 00 00 00 00 00 06 00 00 00 02 00 00
    /// </summary>
    public class MitsubishiElsgwVerificationAcceptanceModel : MitsubishiElsgwSerializer
    {
        ///// <summary>
        ///// Command Number(81h=129)
        ///// </summary>
        //public byte CommandNumber { get; set; } = 129;

        ///// <summary>
        ///// Data Length(6)
        ///// </summary>
        //public byte DataLength { get; set; } = 6;

        /// <summary>
        /// Device Number
        /// 
        /// Set device number which is set under elevator's call data(1-9999)
        /// </summary>
        public ushort DeviceNumber { get; set; }

        /// <summary>
        /// Acceptance Status
        /// 
        /// 00h:Automatic registration of elevator's call,
        /// 01h:Unlockrestriction (Can register elevator's call manually),
        /// FFh:Cannot register elevator's call
        /// </summary>
        public byte AcceptanceStatus { get; set; } = 0;

        /// <summary>
        /// Assigned Elevator Car Number
        /// 
        /// In case of elevator's call made at elevator lobby,
        ///     set the assigned elevator car number(1...12,FFh:No assigned elevator car)
        /// In case of elevator's call made in car,set 0
        /// </summary>
        public byte AssignedElevatorCarNumber { get; set; }

        /// <summary>
        /// Sequence Number
        /// 
        /// Set sequence number which is set under elevator's call data.
        /// </summary>
        public byte SequenceNumber { get; set; }

        /// <summary>
        /// Assignment Attribute
        /// 
        /// This data is not used by ACS
        /// </summary>
        public byte AssignmentAttribute { get; set; }

        protected override void Read()
        {
            //CommandNumber = ReadByte();
            //DataLength = ReadByte();
            DeviceNumber = ReadUnsignedShort();
            AcceptanceStatus = ReadByte();
            AssignedElevatorCarNumber = ReadByte();
            SequenceNumber = ReadByte();
            AssignmentAttribute = ReadByte();
        }

        protected override void Write()
        {
            //WriteByte(CommandNumber);
            //WriteByte(DataLength);
            WriteUnsignedShort(DeviceNumber);
            WriteByte(AcceptanceStatus);
            WriteByte(AssignedElevatorCarNumber);
            WriteByte(SequenceNumber);
            WriteByte(AssignmentAttribute);
        }
    }
}
