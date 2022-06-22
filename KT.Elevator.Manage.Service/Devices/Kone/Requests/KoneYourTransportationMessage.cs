using Org.BouncyCastle.Pkix;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Kone.Requests
{
    /// <summary>
    /// applicationMsgType: 2 (YOUR_TRANSPORTATION)
    /// MajorVersion: 1
    /// MinorVersion: 1
    /// MessageID: Unique message ID by the sender point of view.
    ///         0x00000000 is undefined and shall not be used as 
    ///         messageID by any application.
    /// ResponseID: If this message is a response to another message, the
    ///         other message’s ID is set to this field.ResponseID of
    ///         value 0x00000000 is defined and means that the
    ///         message is not a response to another message.
    /// Timestamp: The time when the message is sent.Time stamp type is 
    ///         windows FILETIME (units of 100ns since January 1,1601 UTC ) as local time
    /// </summary>
    public class KoneYourTransportationMessage : KoneSubHeader1Request
    {
        public KoneYourTransportationMessage()
        {
            //Header
            MessageType = KoneMessageTypeEnum.MSG_DESTINATION_CALL.SocketValue;

            //SubHeader
            ApplicationMsgType = KoneApplicationMsgTypeEnum.YOUR_TRANSPORTATION.SocketValue;
            MajorVersion = 1;
            MinorVersion = 1;

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
        ///  Identification of passenger terminal, part of addressing of  the source of a call.
        /// </summary>
        public ushort TerminalId { get; set; }

        /// <summary>
        /// Destination call type.
        /// </summary>
        public ushort DestCallType { get; set; }

        /// Feedback to the passenger. Meaning: 
        /// 0 = No error,
        /// 1 = The floor does not exist,
        /// 2 = Enter is not pressed (local),
        /// 3 = The floor is locked,
        /// 4 = The floor is not specified (local),
        /// 5 = The call is given to the same floor (local),
        /// 6 = Communication failure (local),
        /// 7 = Please wait,
        /// 8 = Cannot serve now,
        /// 9 = Backup group,
        /// 50 = The terminal is not in use,
        /// 51 = Invalid device type,
        /// 52 = Invalid side,
        /// 53 = Invalid call state,
        /// 54 = Invalid number of passengers,
        /// 55 = No elevators available,
        /// 56 = access denied
        /// 57 = Invasion no service to floor
        /// 10… 49, 58 … 255 = Other response.
        public byte ResponseCode { get; set; }

        /// <summary>
        /// Uint128 Identifier of the passenger for giving personal service: Default = 0
        /// </summary>
        public BigInteger PassengerId { get; set; }

        /// <summary>
        ///  Identification of serving elevator.Meaning: 
        ///  0 = No serving elevator,
        /// 1 = The 1st elevator in group,
        /// 2 = The 2nd elevator in group,
        /// 3 = The 3rd elevator in group, ..
        /// </summary>
        public byte ServingElevatorId { get; set; }

        /// <summary>
        /// Identification of the serving deck of a multi-deck elevator
        /// car.Meaning:
        /// 1 = The bottom deck,
        /// 2 = The next deck on the bottom deck,
        /// 255 = The deck is undefined,
        /// Default = 255.
        /// </summary>
        public byte ServingDeckId { get; set; }

        /// <summary>
        /// The floor where the call comes from.
        /// </summary>
        public byte SourceFloor { get; set; }

        /// <summary>
        /// The side of the elevator that will serve.
        /// Meaning: 0 = front side, 1 = back side, 255 = undefined.
        /// Default = 255.
        /// </summary>
        public byte SourceSide { get; set; }

        /// <summary>
        /// Identification of the lobby where to departure.Default = 255
        /// (Meaning: 255 = The lobby is undefined).
        /// </summary>
        public byte DepartureLobby { get; set; }

        /// <summary>
        /// The floor where the passenger will travel to.
        /// </summary>
        public byte DestinationFloor { get; set; }

        /// <summary>
        /// The side of the elevator where the passenger will travel to.
        /// Meaning: 0 = front side, 1 = back side, 255 = side is 
        /// undefined.Default = 255.
        /// </summary>
        public byte DestinationSide { get; set; }

        /// <summary>
        /// Estimated time of arrival of an elevator to the source floor.
        /// Units: 100 msec.Default = 65535.
        /// Meaning : 65535 = The time is undefined.
        /// </summary>
        public ushort EstimatedTimeOfElevatorArrival { get; set; }

        /// <summary>
        ///  The floor where the passenger will travel to for transferring to the next elevator.
        /// </summary>
        public byte TransferFloor { get; set; }

        /// <summary>
        /// The side of the elevator from where the passenger will exit
        /// for transferring to the next elevator. (additional information
        /// to the transfer_floor). Meaning: 0 = front side, 1 = back
        /// side, 255 = side is undefined.Default = 255.
        /// </summary>
        public byte TransferSide { get; set; }

        /// <summary>
        /// Identification of the lobby of the transfer floor, where the
        /// passenger should go for her/his next elevator.Default = 255 
        /// (Meaning: 255 = The lobby is undefined).
        /// </summary>
        public byte TransferLobby { get; set; }

        /// <summary>
        /// ID of the visual guidance (e.g. location of the serving 
        /// elevator) that is shown to the passenger.
        /// </summary>
        public byte VisualGuidanceId { get; set; }

        /// <summary>
        /// 7 LSB is the size of acu_data. The MSB is reserved for acu data.
        /// </summary>
        public byte AcuDataSize { get; set; }

        /// <summary>
        /// Acu data.
        /// </summary>
        public byte[] AcuData { get; set; }
    }
}
