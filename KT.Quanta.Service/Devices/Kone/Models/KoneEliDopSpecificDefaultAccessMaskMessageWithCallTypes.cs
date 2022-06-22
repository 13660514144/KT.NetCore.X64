using BigMath;
using BigMath.Utils;
using KT.Quanta.Service.Devices.Kone.Helpers;
using KT.Quanta.Service.Devices.Kone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    /// <summary>
    /// DOP Specific Default Access Mask Message with call types
    /// The message type of DOP specific default access mask message is 
    /// MSG_ACCESS_CONTROL(0x8003) and it uses koneSubHeader1:
    /// </summary>

    public class KoneEliDopSpecificDefaultAccessMaskMessageWithCallTypes : KoneEliSubHeader1Request
    {
        public KoneEliDopSpecificDefaultAccessMaskMessageWithCallTypes()
        {
            ApplicationId = (ushort)KoneEliApplicationIdEnum.APP_ACCESS_CONTROL.Code;
            //Header
            MessageType = (ushort)KoneEliMessageTypeEnum.MSG_ACCESS_CONTROL.Code;

            //SubHeader
            ApplicationMsgType = (ushort)KoneEliApplicationMsgTypeEnum.DOP_DEFAULT_MASK_WITH_CALL_TYPES.Code;
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
        /// bit 0 : disconnected state
        /// bit 1 : connected state
        /// bit 2-7 : reserved
        /// </summary>
        public byte SystemState { get; set; }

        /// <summary>
        /// ID of the DOP
        /// </summary>
        public byte DopId { get; set; }

        /// <summary>
        /// Floor of the DOP ( floor minimum value is 1 )
        /// </summary>
        public ushort DopFloorId { get; set; }

        /// <summary>
        /// 1-200. The number of call types defined call_type_data
        /// </summary>
        public byte NumberOfCallTypes { get; private set; }

        /// <summary>
        /// call_type_data[number_of_call_types]
        /// See below for call_type_data structure
        /// </summary>
        public List<KoneEliDopSpecificDefaultAccessMaskMessageCallTypeData> CallTypeDatas { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            SystemState = ReadByte();
            DopId = ReadByte();
            DopFloorId = ReadUnsignedShort();
            NumberOfCallTypes = ReadByte();
            CallTypeDatas = ReadList<KoneEliDopSpecificDefaultAccessMaskMessageCallTypeData>(NumberOfCallTypes,isLittleEndianess);
        }

        protected override void Write(bool isLittleEndianess)
        {
            NumberOfCallTypes = (byte)CallTypeDatas.Count;

            base.Write(isLittleEndianess);

            WriteByte(SystemState);
            WriteByte(DopId);
            WriteUnsignedShort(DopFloorId);
            WriteByte(NumberOfCallTypes);
            WriteList(CallTypeDatas);
        }
    }
}
