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
    /// Access Closed for DOP Message 
    /// The message type of access closed for DOP message is MSG_ACCESS_CONTROL
    /// (0x8003) and it uses koneSubHeader1:
    /// 81 3D 00 00 00 02 80 03 80 07 00 01 01 23 00 00 00 6A 00 00 00 A0 3E CC 77 85 33 D7 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 20 D3 D3 DB 7D 33 D7 01
    /// 81 3D 00 00 00 02 80 03 80 07 00 01 01 23 00 00 00 6A 00 00 00 A0 3E CC 77 85 33 D7 01 
    /// 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 20 D3 D3 DB 7D 33 D7 01
    /// </summary>

    public class KoneEliAccessClosedForDopMessage : KoneEliSubHeader1Request
    {
        public KoneEliAccessClosedForDopMessage()
        {
            ApplicationId = (ushort)KoneEliApplicationIdEnum.APP_ACCESS_CONTROL.Code;
            //Header
            MessageType = (ushort)KoneEliMessageTypeEnum.MSG_ACCESS_CONTROL.Code;

            //SubHeader
            ApplicationMsgType = (ushort)KoneEliApplicationMsgTypeEnum.DOP_CLOSED.Code;
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

            //The time when the message is sent.Time stamp type is 
            //windows FILETIME(units of 100ns since January 1, 1601 UTC ) as local time
            DataTimestamp = KoneHelper.GetLocalFileTime();
        }
        /// <summary>
        /// ID of the DOP
        /// </summary>
        public byte DopId { get; set; }

        /// <summary>
        /// Floor of the DOP ( floor minimum value is 1 )
        /// </summary>
        public ushort DopFloorId { get; set; }

        /// <summary>
        /// 0 = No,
        /// 1 = Yes, 
        /// 2 = Access denied
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
        /// bit 0 : destination front (1=yes, 0=no)
        /// bit 1 : destination rear (1=yes, 0=no)
        /// bit 2 : destination undefined (1=yes, 0=no)
        /// bit 3 : source front (1=yes, 0=no)
        /// bit 4 : source rear (1=yes, 0=no)
        /// bit 5 : source undefined (1=yes, 0=no)
        /// bit 6-7 : reserved
        /// </summary>
        public byte Sides { get; set; }

        /// <summary>
        /// The UserID of the access transaction
        /// </summary>
        public Int128 UserId { get; set; }

        /// <summary>
        /// Time stamp of the given call. See Time Stamp declaration .3.2.1
        /// </summary>
        public ulong DataTimestamp { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            //base.Read(isLittleEndianess);

            DopId = ReadByte();
            DopFloorId = ReadUnsignedShort();
            IsTimeoutExceeded = ReadByte();
            DestinationFloor = ReadUnsignedShort();
            Reserved = ReadByte();
            Sides = ReadByte();
            UserId = ReadBytes(16).ToInt128(asLittleEndian: true);
            DataTimestamp = ReadUnsignedLong();
        }

        protected override void Write(bool isLittleEndianess)
        {
            base.Write(isLittleEndianess);

            WriteByte(DopId);
            WriteUnsignedShort(DopFloorId);
            WriteByte(IsTimeoutExceeded);
            WriteUnsignedShort(DestinationFloor);
            WriteByte(Reserved);
            WriteByte(Sides);
            WriteBytes(UserId.ToBytes(asLittleEndian: true));
            WriteUnsignedLong(DataTimestamp);
        }
    }
}
