using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Models
{
    /// <summary>
    /// Structure of communicaton data(Access control system to E-LIP
    /// In case of Automatic registration
    /// command 0x10(fixed)
    /// 
    /// 12,65,0,0,16,48,48,48,51,48,48,52,49,48,1,0:0C 41 00 00 10 30 30 30 33 30 30 34 31 30 01 00
    /// 12,65,0,0,16,48,48,49,51,48,51,57,51,48,2,0:0C 41 00 00 10 30 30 31 33 30 33 39 33 30 02 00
    /// </summary>
    public class MitsubishiElipAutomaticAccessRequest : MitsubishiElipSerializer
    {
        public MitsubishiElipAutomaticAccessRequest()
        {
            SequenceNumber = MitsubishiElipHelper.GetSequenceNumber();
        }

        /// <summary>
        /// Swiped card reader number(4bytes,char[4])
        /// 
        /// "0001" to "0255"
        /// </summary>
        public uint SwipedCardReaderNumber { get; set; }

        /// <summary>
        /// Destination floor(3bytes,char[3])
        /// 
        /// "001" to "255"
        /// Building floor' may differ from th actual floor name.
        /// The bottom floor in a building is always 'Building floor 001'
        /// If it;s just the card read floor,
        /// E-LIP send to GC panel as it is ,but GC panel cannot assign the car
        /// </summary>
        public uint DestinationFloor { get; set; }

        /// <summary>
        /// Door opening(1byte,char[1])
        /// 
        /// 1:Front side,
        /// 2:rear side,
        /// 3:both side
        /// </summary>
        public byte DoorOpening { get; set; } = 3;

        /// <summary>
        /// Attribution(1byte,char[1])
        /// 
        /// 0:General,
        /// 1:Handicapped person
        /// 2:VIP
        ///  </summary>
        public byte Attribution { get; set; } = 0;

        /// <summary>
        /// Sequence Number
        /// 0x00 to 0xFF
        /// increase per a packet sending
        /// </summary>
        public byte SequenceNumber { get; set; }

        /// <summary>
        /// Reserve(0)
        /// </summary>
        public byte Reserve1 { get; set; } = 0;

        protected override void Read()
        {
            SwipedCardReaderNumber = ReadCharStringUnsignedInt(4);
            DestinationFloor = ReadCharStringUnsignedInt(3);
            DoorOpening = ReadCharStringByte();
            Attribution = ReadCharStringByte();
            SequenceNumber = ReadByte();
            Reserve1 = ReadByte();
        }

        protected override void Write()
        {
            WriteUnsignedIntStringChar(SwipedCardReaderNumber, 4);
            WriteUnsignedIntStringChar(DestinationFloor, 3);
            WriteByteStringChar(DoorOpening);
            WriteByteStringChar(Attribution);
            WriteByte(SequenceNumber);
            WriteByte(Reserve1);
        }
    }
}
