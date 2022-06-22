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
    /// DOP Specific Default Access Mask Message 
    /// The message type of DOP specific default access mask message is 
    /// MSG_ACCESS_CONTROL(0x8003) and it uses koneSubHeader1:
    /// 1、2、3 front
    /// 
    /// 81 30 00 00 00 02 80 03 80 03 00 01 01 06 00 00 00 00 00 00 00 90 15 C3 EF CF 0D D7 01 03 00 01 01 00 03 00 01 00 00 00 01 00 00 00 01 00 00 00
    /// 
    /// 81 C4 01 00 00 02 80 03 80 03 00 01 01 0B 00 00 00 00 00 00 00 D0 9B CA 26 D0 0D D7 01 01 00 01 01 00 68 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00
    /// </summary>

    public class KoneEliDopSpecificDefaultAccessMaskMessage : KoneEliSubHeader1Request
    {
        public KoneEliDopSpecificDefaultAccessMaskMessage()
        {
            AccessMasks = new List<uint>();

            ApplicationId = (ushort)KoneEliApplicationIdEnum.APP_ACCESS_CONTROL.Code;
            //Header
            MessageType = (ushort)KoneEliMessageTypeEnum.MSG_ACCESS_CONTROL.Code;

            //SubHeader
            ApplicationMsgType = (ushort)KoneEliApplicationMsgTypeEnum.DOP_DEFAULT_MASK.Code;
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
        /// 
        /// 工具类 01:断开连接  02:连接 03:所有
        /// </summary>
        public byte SystemState { get; set; }

        /// <summary>
        /// Reserved
        /// </summary>
        public byte Reserved { get; set; }

        /// <summary>
        /// ID of the DOP
        /// </summary>
        public byte DopId { get; set; }

        /// <summary>
        /// Floor of the DOP ( floor minimum value is 1 )
        /// </summary>
        public ushort DopFloorId { get; set; }

        /// <summary>
        /// Number of floors in the mask
        /// </summary>
        public ushort NumberOfFloors { get; private set; }

        /// <summary>
        /// uint32[number_of_floors] //0b00000011=3
        /// bit 0 : allowed destination front (1=yes, 0=no)
        /// bit 1 : allowed destination rear(1=yes, 0=no)
        /// bit 2-31 : reserved
        /// </summary>
        public List<uint> AccessMasks { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            SystemState = ReadByte();
            Reserved = ReadByte();
            DopId = ReadByte();
            DopFloorId = ReadUnsignedShort();
            NumberOfFloors = ReadUnsignedShort();
            AccessMasks = ReadUnsignedInts(NumberOfFloors);
        }

        protected override void Write(bool isLittleEndianess)
        {
            NumberOfFloors = (ushort)AccessMasks.Count;

            base.Write(isLittleEndianess);

            WriteByte(SystemState);
            WriteByte(Reserved);
            WriteByte(DopId);
            WriteUnsignedShort(DopFloorId);
            WriteUnsignedShort(NumberOfFloors);
            WriteUnsignedInts(AccessMasks);
        }
    }
}
