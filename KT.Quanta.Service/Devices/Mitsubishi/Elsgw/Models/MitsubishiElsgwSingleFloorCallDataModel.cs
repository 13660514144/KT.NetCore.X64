using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Helpers;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models
{
    /// <summary>
    /// Elevator's call data (When accessible elevator destination floor is sigle floor)
    /// 样例：1730 0014 01 01 11 01 00000000 01 12 0003 01 04 00 00 0002 0004 01 01 00 00 00 00 00 00
    /// 实操：1730 0014 01 01 11 01 00000000 01 12 0003 01 04 00 00 0001 0008 01 01 00 00 00 01 00 00
    /// </summary>
    public class MitsubishiElsgwSingleFloorCallDataModel : MitsubishiElsgwSerializer
    {
        public MitsubishiElsgwSingleFloorCallDataModel()
        {
            SequenceNumber = MitsubishiElsgwHelper.GetSequenceNumber();
        }

        ///// <summary>
        ///// Command Number(01h)
        ///// </summary>
        //public byte CommandNumber { get; set; } = 1;

        ///// <summary>
        ///// Data Length(18)
        ///// </summary>
        //public byte DataLength { get; set; } = 18;

        /// <summary>
        /// Device Number
        /// 
        /// Set device number (card-reader etc.)(1-9999)
        /// When not specified,set 0
        /// Maxinum connection is 1024 devices.
        /// 
        /// For integrated with smartphone system,the BSS is assigned the deivce number,"9900".
        /// this deivce number should not duplicate whth gate C/R and HOP C/R.
        /// (the BSS is considered to be equivalent to the C/R and the smartphone is considered to be equivalent to a card.
        /// The smartphone that makes a request to the elevator is identified by the sequence number)
        /// </summary>
        public ushort DeviceNumber { get; set; } = 3;

        /// <summary>
        /// Verification Type
        /// 
        /// 1:verification at elevator lobby
        /// 2:verification in car
        /// </summary>
        public byte VerificationType { get; set; } = 1;

        /// <summary>
        /// Verification Location
        /// 
        /// In case verification type is 1,set following.
        /// 1:Elevator lobby
        /// 2:Entrance
        /// 3:Room
        /// 4:Security gate
        /// In case verification type is 2,set car number
        /// </summary>
        public byte VerificationLocation { get; set; } = 4;

        /// <summary>
        /// Hall call button riser attribute/car button attribute
        /// 
        /// In case verification type is 1,set corresponding hall call button riser attribute.
        /// 0:"A" button riser,
        /// 2:"B" button riser,
        /// ......,
        /// 15:"O" button riser,
        /// 16:Auto
        /// In case verification type is 2,set car button attribute
        /// 1:Normal passenger(Front)
        /// 2:Handicapped passenger(Front)
        /// 3:Normal passenger(Rear),
        /// 4:Handicapped Passenger(Rear)
        /// </summary>
        public byte CallButtonAttribute { get; set; } = 0;

        /// <summary>
        /// Reserve(0)
        /// </summary>
        public byte Reserve1 { get; set; } = 0;

        /// <summary>
        /// Boarding Floor
        /// 
        /// In case verification type is 1,set boarding floor by building floor data (1-255).
        /// In case verification type is 2,set 0.
        /// </summary>
        public ushort BoardingFloor { get; set; }

        /// <summary>
        /// Destination Floor
        /// </summary>
        public ushort DestinationFloor { get; set; }

        /// <summary>
        /// Boarding Front/Rear
        /// 
        /// In case verification type is 1,set front or rear at boarding floor.
        /// 1:Front,
        /// 2:Rear.
        /// In case verification type is 2,set 0.
        /// </summary>
        public byte BoardingFrontRear { get; set; } = 1;

        /// <summary>
        /// Destination Front/Rear
        /// 
        /// In case verification type is 1,set front or rear at destination floor.
        /// 1:Front,
        /// 2:Rear.
        /// In case verification type is 2,set 0.
        /// </summary>
        public byte DestinationFrontRear { get; set; } = 1;

        /// <summary>
        /// Elevator's call attribute
        /// 
        /// Set elevator's call attribute 
        /// 0:Normal passenger,
        /// 1:Handicapped passenger,
        /// 2:VIP passenger,
        /// 3:Management passenger
        /// </summary>
        public byte ElevatorCallAttribute { get; set; } = 0;

        /// <summary>
        /// Nonstop Operatin
        /// 
        /// Set 1 when nonstop operation is to be enable.Not enabled,set 0.
        /// </summary>
        public byte NonstopOperatin { get; set; } = 0;

        /// <summary>
        /// Call Registration Mode
        /// 
        /// Call registration mode for hall call button
        /// 0:Automatic
        /// 1:Unlock restriction for hall call button
        /// 2:Unlock restriction for hall call button and car call button 
        /// 3:Autmatic registration for hall call button
        /// 4:Automatic registration for hall call button and unlock restriction for car call button
        /// 5:Automatic registration for ahll call button and car call button
        ///     (Only accessible eelvator destination floor is single floor.)
        /// Call registration model for car call button
        /// 0:Automatic
        /// 1:Unlock restriction for car call button
        /// 2:Automatic registration for car call button(Only accessible elevator destination floor is single floor.)
        /// </summary>
        public byte CallRegistrationMode { get; set; } = 0;

        /// <summary>
        /// Sequence Number
        /// Sequence number should be increment every time sending data form ACS,The net to FFh is 00h.
        /// </summary>
        public byte SequenceNumber { get; set; }

        /// <summary>
        /// Reserve(0)
        /// </summary>
        public byte Reserve2 { get; set; } = 0;

        /// <summary>
        /// Reserve(0)
        /// </summary>
        public byte Reserve3 { get; set; } = 0;

        protected override void Read()
        {
            //CommandNumber = ReadByte();
            //DataLength = ReadByte();
            DeviceNumber = ReadUnsignedShort();
            VerificationType = ReadByte();
            VerificationLocation = ReadByte();
            CallButtonAttribute = ReadByte();
            Reserve1 = ReadByte();
            BoardingFloor = ReadUnsignedShort();
            DestinationFloor = ReadUnsignedShort();
            BoardingFrontRear = ReadByte();
            DestinationFrontRear = ReadByte();
            ElevatorCallAttribute = ReadByte();
            NonstopOperatin = ReadByte();
            CallRegistrationMode = ReadByte();
            SequenceNumber = ReadByte();
            Reserve2 = ReadByte();
            Reserve3 = ReadByte();
        }

        protected override void Write()
        {
            //WriteByte(CommandNumber);
            //WriteByte(DataLength);
            WriteUnsignedShort(DeviceNumber);
            WriteByte(VerificationType);
            WriteByte(VerificationLocation);
            WriteByte(CallButtonAttribute);
            WriteByte(Reserve1);
            WriteUnsignedShort(BoardingFloor);
            WriteUnsignedShort(DestinationFloor);
            WriteByte(BoardingFrontRear);
            WriteByte(DestinationFrontRear);
            WriteByte(ElevatorCallAttribute);
            WriteByte(NonstopOperatin);
            WriteByte(CallRegistrationMode);
            WriteByte(SequenceNumber);
            WriteByte(Reserve2);
            WriteByte(Reserve3);
        }
    }
}
