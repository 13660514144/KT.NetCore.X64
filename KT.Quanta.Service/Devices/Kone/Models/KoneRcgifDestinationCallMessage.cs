using BigMath;
using BigMath.Utils;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Kone.Helpers;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    /// <summary>
    /// The message type of destination call message is MSG_DESTINATION_CALL (0x8000) 
    /// and it uses koneSubHeader1,lenght 31
    ///  
    /// applicationMsgType: 1 (DESTINATION_CALL)
    /// majorVersion: 1
    /// minorVersion: 0
    /// messageID: Unique message ID by the sender point of view.
    ///     0x00000000 is undefined and shall not be used as 
    ///     messageID by any application.
    /// responseID: If this message is a response to another message, the
    ///     other message’s ID is set to this field.ResponseID of
    ///     value 0x00000000 is defined and means that the
    ///     message is not a response to another message.
    ///timeStamp: The time when the message is sent.Time stamp type is 
    ///     windows FILETIME (unit
    /// 
    /// 
    /// Please note: All data fields must be set, otherwise call may be reject. Defaults can be 
    /// applied most of fields when give a basic call.Values in following fields must specific for 
    /// each call:
    ///       source_floor 
    ///       destination_floor
    ///       terminal_id
    /// kone: 81 38 00 00 00 00 80 00 80 01 00 01 00 23 00 00 00 00 00 00 00 40 A6 FB D3 D7 25 D7 01 0C 00 15 00 01 A2 DB 11 0E 00 00 00 00 00 00 00 00 00 00 00 00 3F 01 0C 00 01 00
    /// owner:81 38 00 00 00 00 80 00 80 01 00 01 00 1B 00 00 00 00 00 00 00 E0 BB 7F A3 D8 25 D7 01 0C 00 15 00 01 A2 DB 11 0E 00 00 00 00 00 00 00 00 00 00 00 00 3F 01 0C 00 01 00
    /// </summary>

    public class KoneRcgifDestinationCallMessage : KoneRcgifSubHeader1Request
    {
        public KoneRcgifDestinationCallMessage()
        {
            MessageSize = 56;//64;//31 + 20 + 9;

            ApplicationId = (ushort)KoneRcgifApplicationIdEnum.APP_DESTINATION_CALL.Code;
            //Header
            MessageType = (ushort)KoneRcgifMessageTypeEnum.MSG_DESTINATION_CALL.Code;

            //SubHeader
            ApplicationMsgType = (ushort)KoneRcgifApplicationMsgTypeEnum.DESTINATION_CALL.Code;
            MajorVersion = 1;
            MinorVersion = 0;

            //Unique message ID by the sender point of view. 
            //0x00000000 is undefined and shall not be used as  messageID by any application
            //TODOO
            MessageId = KoneEliHelper.GetMessageId();

            //If this message is a response to another message, the 
            //other message’s ID is set to this field.ResponseID of 
            //value 0x00000000 is defined and means that the message 
            //is not a response to another message.
            //TODOO
            //ResponseId = KoneHelper.GetResponseId();
        }

        /// <summary>
        /// Identification of passenger terminal, part of addressing of 
        /// the source of a call(see also chapter 2.1.1Giving a
        /// Destination Call).
        /// </summary>
        public ushort TerminalId { get; set; }

        /// <summary>
        /// Destination call type.
        /// 0 = Normal call(default)
        /// 1 = Handicap call
        /// 2 = Priority call
        /// 3 = Empty car call
        /// 4 = Space allocation call
        /// 10 = Touch and go(bypass GCAC locking’s)
        /// 11 = Touch and go handicap(bypass GCAC locking’s)
        /// 12 = Touch and go priority(bypass GCAC locking’s)
        /// 5 – 9, 13-30 = Reserved call types
        /// Default = 0
        /// </summary>
        public ushort DestCallType { get; set; } = 20;

        /// <summary>
        /// Meaning: 0 = call is OFF (for cancelling), 1 = call is ON Default = 1
        /// </summary>
        public byte CallState { get; set; } = 1;

        /// <summary>
        /// Uint128 Identifier of the passenger for giving personal service.
        /// Default = 0
        /// </summary>
        public Int128 PassengerId { get; set; }

        /// <summary>
        /// The floor where the call comes from. Part of addressing of 
        /// the source of a call.
        /// </summary>
        public byte SourceFloor { get; set; }

        /// <summary>
        /// The side where the passenger will travel.
        /// Meaning: 0 = front side, 1 = back side, 255 = side is 
        /// undefined.
        /// Default = 255
        /// </summary>
        public byte SourceSide { get; set; } = 0;

        /// <summary>
        /// The floor where the passenger will travel.
        /// </summary>
        public byte DestinationFloor { get; set; }

        /// <summary>
        /// The side of the elevator where the passenger will travel. 
        /// Meaning: 0 = front side, 1 = back side, 255 = side is 
        /// undefined.
        /// Default = 255
        /// </summary>
        public byte DestinationSide { get; set; } = 0;

        /// <summary>
        /// Number of passengers coming along with the call. 
        /// Default = 1
        /// </summary>
        public byte NumberOfPassengers { get; set; } = 1;

        /// <summary>
        /// Servable lift mask. LSB is lift 1. MSB is lift 8.
        /// Default = 0xFFH
        /// 
        /// lifts:1,2,3,4,5,6,7,8
        /// </summary>
        public List<int> Lifts { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            TerminalId = ReadUnsignedShort();
            DestCallType = ReadUnsignedShort();
            CallState = ReadByte();
            PassengerId = ReadBytes(16).ToInt128(asLittleEndian: true);
            SourceFloor = ReadByte();
            SourceSide = ReadByte();
            DestinationFloor = ReadByte();
            DestinationSide = ReadByte();
            NumberOfPassengers = ReadByte();
            Lifts = ByteBitUtil.GetOneByteBitValue(ReadByte());
        }

        protected override void Write(bool isLittleEndianess)
        {
            base.Write(isLittleEndianess);

            WriteUnsignedShort(TerminalId);
            WriteUnsignedShort(DestCallType);
            WriteByte(CallState);
            WriteBytes(PassengerId.ToBytes(asLittleEndian: true));
            WriteByte(SourceFloor);
            WriteByte(SourceSide);
            WriteByte(DestinationFloor);
            WriteByte(DestinationSide);
            WriteByte(NumberOfPassengers);
            WriteByte(ByteBitUtil.SetOneByteBitValue(Lifts));
        }
    }
}
