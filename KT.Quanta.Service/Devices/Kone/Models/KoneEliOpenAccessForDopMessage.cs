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
    /// Open Access for DOP Message 
    /// The message type of open access for DOP message is MSG_ACCESS_CONTROL
    /// (0x8003) and it uses koneSubHeader1:
    /// 
    /// 3、10、11、12楼
    /// error data     :81 34 00 00 00 03 80 03 80 04 00 01 01 01 00 00 00 00 00 00 00 80 FE 2A A4 4A EE D6 01 01 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    /// demo ture data :81 44 00 00 00 02 80 03 80 04 00 01 01 02 00 00 00 00 00 00 00 40 88 50 66 4B EE D6 01 01 01 00 98 3A 31 00 00 0C 00 0F 00 0C 0E 00 00 00 00 00 0D 00 04 00 03 00 00 03 0A 00 00 03 0B 00 00 03 0C 00 00 03
    /// own true data  :81 44 00 00 00 02 80 03 80 04 00 01 01 02 00 00 00 00 00 00 00 30 6C 75 CE 4E EE D6 01 01 01 00 98 3A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 04 00 03 00 00 03 0B 00 00 03 0A 00 00 03 0C 00 00 03 
    /// error data:01 01 00 00 00   00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00   00 00 
    /// ture data :01 01 00 98 3A   31 00 00 0C 00 0F 00 0C 0E 00 00 00 00 00 0D 00   04 00 03 00 00 03 0A 00 00 03 0B 00 00 03 0C 00 00 03
    /// 
    /// -ACS发送数据
    /// 81 3C 00 00 00 02 80 03 80 04 00 01 01 1E 00 00 00 00 00 00 00 10 6D DD 6A 14 20 D7 01 01 17 00 98 3A A2 20 BC 1C 00 00 00 00 00 00 00 00 00 00 00 00 02 00 0E 00 00 02 0F 00 00 02
    /// -通力模拟器发送数据
    /// 81 3C 00 00 00 02 80 03 80 04 00 01 01 08 00 00 00 00 00 00 00 E0 46 40 4C 12 20 D7 01 01 17 00 98 3A A2 20 BC 1C 00 00 00 00 00 00 00 00 00 00 00 00 02 00 0E 00 00 02 0F 00 00 02
    /// 
    /// 81 34 00 00 00 02 80 03 80   04 00 01 01 1F 00 00 00 00 00 00 00 E0 91 3F C7 27 22 D7 01   04 17 00 98 3A D2 26 D0 0D 00 00 00 00 00 00 00 00 00 00 00 00 00 00
    /// </summary>

    public class KoneEliOpenAccessForDopMessage : KoneEliSubHeader1Request
    {
        public KoneEliOpenAccessForDopMessage()
        {
            ApplicationId = (ushort)KoneEliApplicationIdEnum.APP_ACCESS_CONTROL.Code;
            //Header
            MessageType = (ushort)KoneEliMessageTypeEnum.MSG_ACCESS_CONTROL.Code;

            //SubHeader
            ApplicationMsgType = (ushort)KoneEliApplicationMsgTypeEnum.OPEN_DOP.Code;
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
        /// ID of the DOP
        /// </summary>
        public byte DopId { get; set; }

        /// <summary>
        /// Floor of the DOP ( floor minimum value is 1 )
        /// </summary>
        public ushort DopFloorId { get; set; }

        /// <summary>
        /// The maximum time the DOP is kept open after accepting
        /// this message.After open_timeout the DOP is locked
        /// (returned to its default access state) again even if no calls were given.
        /// The unit is milliseconds (ms).
        /// 0x0000 uses the system default timeout (10000ms).
        /// The minimum timeout is 1000ms.
        /// The maximum timeout is 30000ms
        /// </summary>
        public ushort OpenTimeOut { get; set; } = 15000;

        /// <summary>
        /// The UserID of the access transaction
        /// </summary>
        public Int128 UserId { get; set; }

        /// <summary>
        /// Number of open events.
        /// </summary>
        public ushort NumberOfOpens { get; private set; }

        /// <summary>
        /// uint32[number_of_opens]
        /// bit 0-15: allowed destination floor (uint16)
        /// bit 16-23 : reserved
        /// bit 24 : allowed destination front(1=yes, 0=no)
        /// bit 25 : allowed destination rear(1=yes, 0=no)
        /// bit 26 - 31: reserved
        /// </summary>
        public List<KoneEliOpenAccessForDopMessageOpenEventData> OpenEvents { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            DopId = ReadByte();
            DopFloorId = ReadUnsignedShort();
            OpenTimeOut = ReadUnsignedShort();
            UserId = ReadBytes(16).ToInt128(asLittleEndian: true);
            NumberOfOpens = ReadUnsignedShort();
            OpenEvents = ReadList<KoneEliOpenAccessForDopMessageOpenEventData>(NumberOfOpens, isLittleEndianess);
        }

        protected override void Write(bool isLittleEndianess)
        {
            if (OpenEvents != null)
            {
                NumberOfOpens = (ushort)OpenEvents.Count;
            }

            base.Write(isLittleEndianess);

            WriteByte(DopId);
            WriteUnsignedShort(DopFloorId);
            WriteUnsignedShort(OpenTimeOut);
            WriteBytes(UserId.ToBytes(asLittleEndian: true));
            WriteUnsignedShort(NumberOfOpens);
            WriteList(OpenEvents);
        }
    }
}
