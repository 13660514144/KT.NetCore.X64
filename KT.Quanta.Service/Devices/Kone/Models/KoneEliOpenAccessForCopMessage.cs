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
    /// Open Access for COP Message 
    /// The message type of open access for COP message is MSG_ACCESS_CONTROL
    /// (0x8003) and it uses koneSubHeader1:
    /// </summary>

    public class KoneEliOpenAccessForCopMessage : KoneEliSubHeader1Request
    {
        public KoneEliOpenAccessForCopMessage()
        {
            ApplicationId = (ushort)KoneEliApplicationIdEnum.APP_ACCESS_CONTROL.Code;
            //Header
            MessageType = (ushort)KoneEliMessageTypeEnum.MSG_ACCESS_CONTROL.Code;

            //SubHeader
            ApplicationMsgType = (ushort)KoneEliApplicationMsgTypeEnum.OPEN_COP.Code;
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
        /// The maximum time the COP is kept open after accepting 
        /// this message.After open_timeout the COP is locked
        /// (returned to its default access state) again even if no calls
        /// were given.
        /// The unit is milliseconds (ms).
        /// 0x0000 uses the system default timeout (10000ms).
        /// The minimum timeout is 1000ms.
        /// The maximum timeout is 30000ms.
        /// </summary>
        public ushort OpenTimeOut { get; set; }

        /// <summary>
        /// The UserID of the access transaction
        /// </summary>
        public Int128 UserId { get; set; }

        /// <summary>
        /// Number of open events.
        /// </summary>
        public ushort NumberOfOpens { get; private set; }

        /// <summary>
        /// bit 0-15: allowed destination floor (uint16)
        /// bit 16-23 : reserved
        /// bit 24 : allowed destination front(1=yes, 0=no)
        /// bit 25 : allowed destination rear(1=yes, 0=no)
        /// bit 26 - 31: reserved
        /// </summary>
        public List<KoneEliOpenAccessForCopMessageOpenEventData> OpenEvents { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            ElevatorId = ReadByte();
            GroupId = ReadByte();
            OpenTimeOut = ReadUnsignedShort();
            UserId = ReadBytes(16).ToInt128(asLittleEndian: true);
            NumberOfOpens = ReadUnsignedShort();
            OpenEvents = ReadList<KoneEliOpenAccessForCopMessageOpenEventData>(NumberOfOpens, isLittleEndianess);
        }

        protected override void Write(bool isLittleEndianess)
        {
            NumberOfOpens = (ushort)OpenEvents.Count;

            base.Write(isLittleEndianess);

            WriteByte(ElevatorId);
            WriteByte(GroupId);
            WriteUnsignedShort(OpenTimeOut);
            WriteBytes(UserId.ToBytes(asLittleEndian: true));
            WriteUnsignedShort(NumberOfOpens);
            WriteList(OpenEvents);
        }
    }
}
