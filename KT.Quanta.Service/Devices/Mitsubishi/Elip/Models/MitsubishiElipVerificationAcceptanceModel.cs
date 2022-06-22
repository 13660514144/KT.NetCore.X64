using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Models
{
    /// <summary>
    /// Verification acceptance data 
    /// 
    /// ELSGW has memory of elevator bank number, device number and sequence number 
    /// which are set under elevator's call data and set these data.
    /// </summary>
    public class MitsubishiElipVerificationAcceptanceModel : MitsubishiElipSerializer
    {
        /// <summary>
        /// Sequence Number
        /// 
        /// Set sequence number which is set under elevator's call data.
        /// </summary>
        public byte SequenceNumber { get; set; }

        /// <summary>
        /// Assigned Elevator Car Number(3bytes,char[3])
        /// 
        /// "000" to "255"
        /// ("255 is means 'answer of manual registration' or 'can not assign car'.)
        /// </summary>
        public uint AssignedCarNumber { get; set; }

        /// <summary>
        /// Assigned Bank Number(1byte,cahr[1])
        /// 
        /// "0" to "3"
        /// </summary>
        public byte AssignedBankNumber { get; set; }

        protected override void Read()
        {
            SequenceNumber = ReadByte();
            AssignedCarNumber = ReadCharStringUnsignedInt(3);
            AssignedBankNumber = ReadCharStringByte();
        }

        protected override void Write()
        {
            WriteByte(SequenceNumber);
            WriteUnsignedIntStringChar(AssignedCarNumber, 3);
            WriteByteStringChar(AssignedBankNumber);
        }
    }
}
