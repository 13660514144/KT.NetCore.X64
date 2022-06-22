using BigMath;
using BigMath.Utils;
using KT.Quanta.Service.Devices.Kone.Helpers;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    /// <summary>
    /// Access Closed for COP Message 
    /// The message type of access closed for COP message is MSG_ACCESS_CONTROL
    /// (0x8003) and it uses koneSubHeader1:
    /// </summary>

    public class KoneEliAccessClosedForCopMessage : KoneEliSubHeader1Request
    {
        public KoneEliAccessClosedForCopMessage()
        {
            ApplicationId = (ushort)KoneEliApplicationIdEnum.APP_ACCESS_CONTROL.Code;
            //Header
            MessageType = (ushort)KoneEliMessageTypeEnum.MSG_ACCESS_CONTROL.Code;

            //SubHeader
            ApplicationMsgType = (ushort)KoneEliApplicationMsgTypeEnum.COP_CLOSED.Code;
            MajorVersion = 1;
            MinorVersion = 1;

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
        /// ID of the elevator
        /// </summary>
        public byte ElevatorId { get; set; }

        /// <summary>
        /// elevator group id
        /// </summary>
        public byte GroupId { get; set; }

        /// <summary>
        /// Boolean, 1 if the timeout was exceeded, 0 otherwise.
        /// </summary>
        public byte IsTimeoutExceeded { get; set; }

        /// <summary>
        /// Given destination floor
        /// </summary>
        public ushort DestinationFloor { get; set; }

        /// <summary>
        /// Reserved
        /// </summary>
        public byte Reserved { get; set; }

        /// <summary>
        /// The UserID of the access transaction
        /// </summary>
        public Int128 UserId { get; set; }

        /// <summary>
        /// bit 0 : destination front (1=yes, 0=no)
        /// bit 1 : destination rear(1=yes, 0=no)
        /// bit 2 : destination undefined(1=yes, 0=no)
        /// bit 3 : source front(1=yes, 0=no)
        /// bit 4 : source rear(1=yes, 0=no)
        /// bit 5 : source undefined(1=yes, 0=no)
        /// bit 6-7 : reserved
        /// </summary>
        public byte Sides { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            ElevatorId = ReadByte();
            GroupId = ReadByte();
            IsTimeoutExceeded = ReadByte();
            DestinationFloor = ReadUnsignedShort();
            Reserved = ReadByte();
            UserId = ReadBytes(16).ToInt128(asLittleEndian: true);
            Sides = ReadByte();
        }

        protected override void Write(bool isLittleEndianess)
        {
            base.Write(isLittleEndianess);

            WriteByte(ElevatorId);
            WriteByte(GroupId);
            WriteByte(IsTimeoutExceeded);
            WriteUnsignedShort(DestinationFloor);
            WriteByte(Reserved);
            WriteBytes(UserId.ToBytes(asLittleEndian: true));
            WriteByte(Sides);
        }
    }
}
