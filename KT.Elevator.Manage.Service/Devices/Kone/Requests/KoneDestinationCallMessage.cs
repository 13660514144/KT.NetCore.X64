using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Kone.Requests
{
    /// <summary>
    /// The message type of destination call message is MSG_DESTINATION_CALL (0x8000) 
    /// and it uses koneSubHeader1:
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
    /// </summary>

    public class KoneDestinationCallMessage : KoneSubHeader1Request
    {
        public KoneDestinationCallMessage()
        {
            //Header
            MessageType = KoneMessageTypeEnum.MSG_DESTINATION_CALL.SocketValue;

            //SubHeader
            ApplicationMsgType = KoneApplicationMsgTypeEnum.DESTINATION_CALL.SocketValue;
            MajorVersion = 1;
            MinorVersion = 0;

            //Unique message ID by the sender point of view. 
            //0x00000000 is undefined and shall not be used as  messageID by any application
            //TODOO
            MessageId = KoneHelper.GetNewMessageId();

            //If this message is a response to another message, the 
            //other message’s ID is set to this field.ResponseID of 
            //value 0x00000000 is defined and means that the message 
            //is not a response to another message.
            //TODOO
            ResponseId = 0;

            //The time when the message is sent.Time stamp type is 
            //windows FILETIME(units of 100ns since January 1, 1601 UTC ) as local time
            TimeStamp = 0;
        }

        /// <summary>
        /// Identification of passenger terminal, part of addressing of 
        /// the source of a call(see also chapter 2.1.1Giving a
        /// Destination Call).
        /// </summary>
        public uint TerminalId { get; set; }

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
        public uint DestCallType { get; set; } = 0;

        /// <summary>
        /// Meaning: 0 = call is OFF (for cancelling), 1 = call is ON Default = 1
        /// </summary>
        public byte CallState { get; set; } = 1;

        /// <summary>
        /// Uint128 Identifier of the passenger for giving personal service.
        /// Default = 0
        /// </summary>
        public BigInteger PassengerId { get; set; } = 0;

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
        public byte SourceSide { get; set; } = 255;

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
        public byte DestinationSide { get; set; } = 255;

        /// <summary>
        /// Number of passengers coming along with the call. 
        /// Default = 1
        /// </summary>
        public byte NumberOfPassengers { get; set; } = 1;

        /// <summary>
        /// Servable lift mask. LSB is lift 1. MSB is lift 8.
        /// Default = 0xFFH
        /// </summary>
        public byte Lifts { get; set; } = 255;
    }
}
