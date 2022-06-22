using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models
{
    /// <summary>
    /// transmission packet header
    /// </summary>
    public class MitsubishiElsgwTransmissionHeader : MitsubishiElsgwSerializer
    {
        public MitsubishiElsgwTransmissionHeader()
        {
            Assistant = new MitsubishiElsgwTransmissionAssistantHeader();
        }

        /// <summary>
        /// Identifier(1730h=5936)
        /// </summary>
        public ushort Identifier { get; set; } = 5936;

        /// <summary>
        /// Data Length
        /// 
        /// Byte size of transmission packet data
        /// </summary>
        public ushort DataLength { get; set; }

        /// <summary>
        /// Address Device Type
        /// 
        /// Set the device type of address
        /// 01h:ELSGW:Elevator system device
        /// 11h:ACS:Security system device
        /// 22h:BSS:Operation display device
        /// FFh:All system
        /// </summary>
        public byte AddressDeviceType { get; set; } = 1;

        /// <summary>
        /// Address Device Number
        /// 
        /// -Set deivce number of address(1-127)
        /// -If system type is ELSGW,set elevator bank number(1-4)
        /// -If system type is all system,set FFh
        /// </summary>
        public byte AddressDeviceNumber { get; set; } = 1;

        /// <summary>
        /// Sender Device Type
        /// 
        /// Set the device type of sender
        /// 01h:ELSGW:Elevator system device
        /// 11h=17:ACS:Security system device
        /// 22h:BSS:Operation display device
        /// FFh:All system
        /// </summary>
        public byte SenderDeviceType { get; set; } = 17;

        /// <summary>
        /// Sender Device Number
        /// 
        /// .Set device number of sender(1-127)
        /// .If system type is ELSGW,set elevator bank number(1)
        /// </summary>
        public byte SenderDeviceNumber { get; set; } = 1;

        /// <summary>
        /// Reserve byte[4] (00h)
        /// </summary>
        public uint Reserve { get; set; } = 0;

        /// <summary>
        /// 数据
        /// </summary>
        public MitsubishiElsgwTransmissionAssistantHeader Assistant { get; set; }

        protected override void Read()
        {
            Identifier = ReadUnsignedShort();
            DataLength = ReadUnsignedShort();
            AddressDeviceType = ReadByte();
            AddressDeviceNumber = ReadByte();
            SenderDeviceType = ReadByte();
            SenderDeviceNumber = ReadByte();
            Reserve = ReadUnsignedInt();
            Assistant = ReadObject<MitsubishiElsgwTransmissionAssistantHeader>(DataLength);
        }

        protected override void Write()
        {
            WriteUnsignedShort(Identifier);
            WriteUnsignedShort(DataLength);
            WriteByte(AddressDeviceType);
            WriteByte(AddressDeviceNumber);
            WriteByte(SenderDeviceType);
            WriteByte(SenderDeviceNumber);
            WriteUnsignedInt(Reserve);
            WriteObject(Assistant);
        }
    }
}
