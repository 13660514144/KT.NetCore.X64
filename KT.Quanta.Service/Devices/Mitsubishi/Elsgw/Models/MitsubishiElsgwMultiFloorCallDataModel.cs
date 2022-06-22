using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models
{
    /// <summary>
    /// Elevator's call data (When accessible elevator destination floor is multi floors)
    /// 
    /// 1730 0018 01 01 11 01 00000000 02 16 000B 01 01 10 00 0003 0000 01 00 00 00 00 01 04 00 00 FF 00 00
    /// 
    /// </summary>
    public class MitsubishiElsgwMultiFloorCallDataModel : MitsubishiElsgwSerializer
    {
        public MitsubishiElsgwMultiFloorCallDataModel()
        {
            SequenceNumber = MitsubishiElsgwHelper.GetSequenceNumber();
        }

        ///// <summary>
        ///// Command Number(02h)
        ///// </summary>
        //public byte CommandNumber { get; set; } = 2;

        ///// <summary>
        ///// Data Length
        ///// 
        ///// Number of byte excluding command number and command data lenth (excluding padding)
        ///// </summary>
        //public byte DataLength { get; set; }

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
        public ushort DeviceNumber { get; set; } = 0;

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
        public byte VerificationLocation { get; set; } = 1;//4;

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
        public byte CallButtonAttribute { get; set; } = 16;

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
        /// Reserve2(0)
        /// </summary>
        public ushort Reserve2 { get; set; } = 0;

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
        /// Reserve3(0)
        /// </summary>
        public byte Reserve3 { get; set; } = 0;

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
        /// Sequence Number(00h-FFh)
        /// 
        /// Sequence number should be increment every time sending data form ACS,The net to FFh is 00h.
        /// </summary>
        public byte SequenceNumber { get; set; }

        /// <summary>
        /// Front Destination Floor Data Length 
        /// 
        /// Set data length of front destination floor (0-32)
        /// </summary>
        public byte FrontDestinationFloorDataLength { get; set; }

        /// <summary>
        /// Front Destination Floor Data Length 
        /// 
        /// Set data length of rear destination floor (0-32)
        /// </summary>
        public byte RearDestinationFloorDataLength { get; set; }

        /// <summary>
        /// Front Destination Floor
        /// </summary>
        public List<byte> FrontDestinationFloors { get; set; }

        /// <summary>
        /// Rear Destination Floor
        /// </summary>
        public List<byte> RearDestinationFloors { get; set; }

        ///// <summary>
        ///// Padding byte[1-3] [Frame Encoder add padding]
        ///// The data length of padding should be set to ensure the total size of transmission packet data to multiple of 4.(Set "0" figure)
        ///// </summary>
        //public List<byte> Padding { get; set; }

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
            Reserve2 = ReadUnsignedShort();
            BoardingFrontRear = ReadByte();
            Reserve3 = ReadByte();
            ElevatorCallAttribute = ReadByte();
            NonstopOperatin = ReadByte();
            CallRegistrationMode = ReadByte();
            SequenceNumber = ReadByte();
            FrontDestinationFloorDataLength = ReadByte();
            RearDestinationFloorDataLength = ReadByte();

            var sourceFrontDestinationFloors = ReadBytes(FrontDestinationFloorDataLength).ToList();
            FrontDestinationFloors = ToRealFloors(sourceFrontDestinationFloors);

            var sourceRearDestinationFloors = ReadBytes(RearDestinationFloorDataLength).ToList();
            RearDestinationFloors = ToRealFloors(sourceRearDestinationFloors);

            //Padding = ReadEndBytes().ToList();
        }

        protected override void Write()
        {
            var sourceFrontDestinationFloors = ToSourceFloors(FrontDestinationFloors);
            var sourceRearDestinationFloors = ToSourceFloors(RearDestinationFloors);

            FrontDestinationFloorDataLength = (byte)sourceFrontDestinationFloors.Count;
            RearDestinationFloorDataLength = (byte)sourceRearDestinationFloors.Count;

            //WriteByte(CommandNumber);
            //WriteByte(DataLength);
            WriteUnsignedShort(DeviceNumber);
            WriteByte(VerificationType);
            WriteByte(VerificationLocation);
            WriteByte(CallButtonAttribute);
            WriteByte(Reserve1);
            WriteUnsignedShort(BoardingFloor);
            WriteUnsignedShort(Reserve2);
            WriteByte(BoardingFrontRear);
            WriteByte(Reserve3);
            WriteByte(ElevatorCallAttribute);
            WriteByte(NonstopOperatin);
            WriteByte(CallRegistrationMode);
            WriteByte(SequenceNumber);
            WriteByte(FrontDestinationFloorDataLength);
            WriteByte(RearDestinationFloorDataLength);

            WriteBytes(sourceFrontDestinationFloors.ToArray());
            WriteBytes(sourceRearDestinationFloors.ToArray());

            //WriteBytes(Padding.ToArray());
        }
    }
}
