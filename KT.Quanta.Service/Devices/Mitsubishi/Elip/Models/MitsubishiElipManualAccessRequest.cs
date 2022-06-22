using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Models
{
    /// <summary>
    /// Structure of communicaton data(Access control system to E-LIP
    /// In case of Manual registration
    /// command 0x20(fixed)
    /// 
    /// 4C 41 00 00 20 30 30 30 33 {楼层} 30 01 00
    /// 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
    /// 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64
    /// 
    /// 读卡器3，楼层4、8、11楼
    /// 4C 41 00 00 20 30 30 30 33 40 40 10 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 30 01 00
    /// 
    /// 读卡器3，楼层3、4楼
    /// 4C 41 00 00 20 30 30 30 33 50 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 30 01 00 00 00 00 00 
    /// 
    /// 读卡器12，楼层25、26、27楼
    /// 4C 41 00 00 20 30 30 31 32 00 00 00 00 00 00 15 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 30 04 00 00 00 00 00
    /// 
    /// 01 02 04 08 10 20 40 80 
    /// </summary>
    public class MitsubishiElipManualAccessRequest : MitsubishiElipSerializer
    {
        public MitsubishiElipManualAccessRequest()
        {
            SequenceNumber = MitsubishiElipHelper.GetSequenceNumber();

            AccesibleFloors = new List<byte>();
        }

        /// <summary>
        /// Swiped card reader number(4bytes,char[4])
        /// 
        /// "0001" to "0255"
        /// </summary>
        public uint SwipedCardReaderNumber { get; set; }

        /// <summary>
        /// Accesible floors(byte[64])
        /// 
        ///  Refer to the following Structure of "Accessible Floors" Data.
        /// </summary>
        public List<byte> AccesibleFloors { get; set; }

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

        /// <summary>
        /// Reserve(0)
        /// </summary>
        public byte Reserve2 { get; set; } = 0;

        /// <summary>
        /// Reserve(0)
        /// </summary>
        public byte Reserve3 { get; set; } = 0;

        /// <summary>
        /// Reserve(0)
        /// </summary>
        public byte Reserve4 { get; set; } = 0;

        /// <summary>
        /// Reserve(0)
        /// </summary>
        public byte Reserve5 { get; set; } = 0;

        protected override void Read()
        {
            SwipedCardReaderNumber = ReadCharStringUnsignedInt(4);

            var soruceAccesibleFloors = ReadBytes(64);
            AccesibleFloors = ToRealFloors(soruceAccesibleFloors.ToList());

            Attribution = ReadCharStringByte();
            SequenceNumber = ReadByte();
            Reserve1 = ReadByte();
            Reserve2 = ReadByte();
            Reserve3 = ReadByte();
            Reserve4 = ReadByte();
            Reserve5 = ReadByte();
        }

        protected override void Write()
        {
            WriteUnsignedIntStringChar(SwipedCardReaderNumber, 4);

            var sourceAccesibleFloors = ToSourceFloors(AccesibleFloors);
            WriteBytes(sourceAccesibleFloors.ToArray());

            WriteByteStringChar(Attribution);
            WriteByte(SequenceNumber);
            WriteByte(Reserve1);
            WriteByte(Reserve2);
            WriteByte(Reserve3);
            WriteByte(Reserve4);
            WriteByte(Reserve5);
        }
    }
}
