using BigMath;
using BigMath.Utils;
using KT.Quanta.Service.Devices.Kone.Helpers;
using KT.Quanta.Service.Devices.Kone.Models;
using System.Collections.Generic;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    /// <summary>
    /// Open Access for DOP with Calltype Message
    /// The message type of open access for DOP message is MSG_ACCESS_CONTROL
    /// (0x8003) and it uses koneSubHeader1:
    /// 
    /// calltype:0 lifts:0 distinations:3、10、11、12楼 
    /// demo data      :81 4B 00 00 00 02 80 03 80   0B 00 01 01 03 00 00 00 00 00 00 00 40 7E 19 81 F8 EE D6 01   01 01 00 98 3A 31 00 00 00 00 0A 00 00 0A 0C 00 00 00 0E 00 00 00 00 FF 01 13 00   01 04 00 03 00 00 03 0A 00 00 03 0B 00 00 03 0C 00 00 03
    /// own       data :81 4B 00 00 00 02 80 03 80   0B 00 01 01 01 00 00 00 00 00 00 00 30 1F A8 59 0A EF D6 01   01 01 00 98 3A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 FF 01 13 00   01 04 00 03 00 00 03 0B 00 00 03 0A 00 00 03 0C 00 00 03 
    /// demo real data      :01 01 00 98 3A  31 00 00 00 00 0A 00 00 0A 0C 00 00 00 0E 00 00 00   00 FF 01 13 00 01 04 00 03 00 00 03 0A 00 00 03 0B 00 00 03 0C 00 00 03
    /// own real error data :01 01 00 98 3A  00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01   00 FF 01 04 00 03 00 00 03 0B 00 00 03 0A 00 00 03 0C 00 00 03
    /// response data:81 1F 00 00 00 02 80 03 80 96 00 01 01 1D 00 00 00 03 00 00 00 D0 B8 35 81 F8 EE D6 01 00 00
    /// own response :81 1F 00 00 00 02 80 03 80 96 00 01 01 25 00 00 00 00 00 00 00 B0 A5 29 BD 00 EF D6 01 00 00
    /// 
    /// calltype:1 lifts:0 distinations:3、10、11、12楼 
    /// demo data:81 4B 00 00 00 02 80 03 80 0B 00 01 01 06 00 00 00 00 00 00 00 C0 E3 94 5B F9 EE D6 01 01 01 00 98 3A 31 00 00 00 00 0A 00 00 0A 0C 00 00 00 0E 00 00 00 01 FF 01 13 00 01 04 00 03 00 00 03 0A 00 00 03 0B 00 00 03 0C 00 00 03
    /// 
    /// response data:81 1F 00 00 00 02 80 03 80 96 00 01 01 20 00 00 00 06 00 00 00 F0 A4 B2 5B F9 EE D6 01 00 00
    /// 
    /// 
    /// calltype:0 lifts:2、3 distinations:3、10、11、12楼 
    /// demo data:81 4F 00 00 00 02 80 03 80 0B 00 01 01 07 00 00 00 00 00 00 00 60 64 63 B7 F9 EE D6 01 01 01 00 98 3A 31 00 00 00 00 0A 00 00 0A 0C 00 00 00 0E 00 00 00 00 FF 02 17 00 01 04 00 03 00 00 03 0A 00 00 03 0B 00 00 03 0C 00 00 03 02 02 02 03
    /// 
    /// response data:81 1F 00 00 00 02 80 03 80 96 00 01 01 21 00 00 00 07 00 00 00 E0 CA 86 B7 F9 EE D6 01 00 00
    /// 
    /// 
    /// calltype:1 lifts:2、3 distinations:3、10、11、12楼 
    /// demo data:81 4F 00 00 00 02 80 03 80 0B 00 01 01 08 00 00 00 00 00 00 00 C0 D3 A6 0D FA EE D6 01 01 01 00 98 3A 31 00 00 00 00 0A 00 00 0A 0C 00 00 00 0E 00 00 00 01 FF 02 17 00 01 04 00 03 00 00 03 0A 00 00 03 0B 00 00 03 0C 00 00 03 02 02 02 03
    /// 
    /// response data:81 1F 00 00 00 02 80 03 80 96 00 01 01 22 00 00 00 08 00 00 00 E0 6D C4 0D FA EE D6 01 00 00
    ///
    /// 
    /// 14、15楼前门
    /// 81 3C 00 00 00 02 80 03 80 04 00 01 01 02 00 00 00 00 00 00 00 00 83 9F 76 AA 38 D7 01 01 17 00 98 3A A2 20 BC 1C 00 00 00 00 00 00 00 00 00 00 00 00 02 00 0E 00 00 02 0F 00 00 02
    /// 80 00 00 00 3C 80 02 80 03 00 04 01 01 00 00 00 01 00 00 00 00 01 D7 38 A9 FD 2F 1A 30 01 00 17 3A 98 00 00 00 00 00 00 00 00 00 00 00 00 1C BC 20 A2 00 02 02 00 00 0E 02 00 00 0F
    /// </summary>

    public class KoneEliOpenAccessForDopWithCallTypeMessage : KoneEliSubHeader1Request
    {
        public KoneEliOpenAccessForDopWithCallTypeMessage()
        {
            ApplicationId = (ushort)KoneEliApplicationIdEnum.APP_ACCESS_CONTROL.Code;
            //Header
            MessageType = (ushort)KoneEliMessageTypeEnum.MSG_ACCESS_CONTROL.Code;

            //SubHeader
            ApplicationMsgType = (ushort)KoneEliApplicationMsgTypeEnum.OPEN_DOP_WITH_CALL_TYPES.Code;
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
        /// 
        /// the demo is default 15000ms
        /// </summary>
        public ushort OpenTimeOut { get; set; } = 15000;

        /// <summary>
        /// The UserID of the access transaction
        /// demo value 00000e0000000c0a00000a0000000031
        /// </summary>
        public Int128 UserId { get; set; }

        /// <summary>
        /// 0 : The access control selects the call type value for the 
        /// call..The next DCS call shall have call_type, defined in 
        /// call_type data, and use the attributes if defined.The call is 
        /// checked against defined attributes. 1-200 : The user selects the call type from DOP panel.The
        /// next dcs call with user selected call type is checked against
        /// the access control set of call types with attributes.
        /// </summary>
        public byte NumberOfCallTypes { get; private set; }

        /// <summary>
        /// call_type_data[number_of_call_types]
        /// See below for call_type_data structure
        /// </summary>
        public List<KoneEliOpenAccessForDopCallTypeData> CallTypeDatas { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            DopId = ReadByte();
            DopFloorId = ReadUnsignedShort();
            OpenTimeOut = ReadUnsignedShort();
            UserId = ReadBytes(16).ToInt128(asLittleEndian: true);
            NumberOfCallTypes = ReadByte();
            CallTypeDatas = ReadList<KoneEliOpenAccessForDopCallTypeData>(NumberOfCallTypes, isLittleEndianess);
        }

        protected override void Write(bool isLittleEndianess)
        {
            if (CallTypeDatas != null)
            {
                NumberOfCallTypes = (byte)CallTypeDatas.Count;
            }

            base.Write(isLittleEndianess);

            WriteByte(DopId);
            WriteUnsignedShort(DopFloorId);
            WriteUnsignedShort(OpenTimeOut);
            WriteBytes(UserId.ToBytes(asLittleEndian: true));
            WriteByte(NumberOfCallTypes);
            WriteList(CallTypeDatas);
        }
    }
}
