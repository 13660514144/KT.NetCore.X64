using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Models
{
    /// <summary>
    /// Communication header
    /// </summary>
    public class MitsubishiElipCommunicationHeader : MitsubishiElipSerializer
    {
        public MitsubishiElipCommunicationHeader()
        {
            Assistant = new MitsubishiElipTransmissionAssistantHeader();
        }

        /// <summary>
        /// Data Length
        /// 
        /// Byte size of communication packet data
        /// </summary>
        public byte DataLength { get; set; }

        /// <summary>
        /// Versin 
        /// 
        /// "A"(fixed)
        /// </summary>
        public char Version { get; set; } = 'A';

        /// <summary>
        /// Reserve 1
        /// </summary>
        public byte Reserve1 { get; set; } = 0;

        /// <summary>
        /// Reserve 2
        /// </summary>
        public byte Reserve2 { get; set; } = 0;

        /// <summary>
        /// 数据
        /// </summary>
        public MitsubishiElipTransmissionAssistantHeader Assistant { get; set; }

        protected override void Read()
        {
            DataLength = ReadByte();
            Version = ReadChar();
            Reserve1 = ReadByte();
            Reserve2 = ReadByte();
            Assistant = ReadObject<MitsubishiElipTransmissionAssistantHeader>(DataLength);
        }

        protected override void Write()
        {
            WriteByte(DataLength);
            WriteChar(Version);
            WriteByte(Reserve1);
            WriteByte(Reserve2);
            WriteObject(Assistant);
        }
    }
}
