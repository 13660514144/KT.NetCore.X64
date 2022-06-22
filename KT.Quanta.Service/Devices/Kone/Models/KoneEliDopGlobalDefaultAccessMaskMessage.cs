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
    /// DOP Global Default Access Mask Message
    /// The message type of DOP global default access mask message is
    /// MSG_ACCESS_CONTROL(0x8003) and it uses koneSubHeader1:
    /// 
    /// floor id 5,59,60 front and rear total 63
    /// 81 1D 01 00 00 02 80 03 80 01 00 01 01 07 00 00 00 00 00 00 00 60 DC 72 7D AC 2A D7 01 01 00 3F 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 09 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 1B 00 00 00 0B 00 00 00 00 00 00 00 00 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 09 00 00 00 00 00 00 00 12 00 00 00 12 00 00 00 12 00 00 00 12 00 00 00 12 00 00 00
    /// /// </summary>

    public class KoneEliDopGlobalDefaultAccessMaskMessage : KoneEliSubHeader1Request
    {
        public KoneEliDopGlobalDefaultAccessMaskMessage()
        {
            AccessMasks = new List<uint>();

            ApplicationId = (ushort)KoneEliApplicationIdEnum.APP_ACCESS_CONTROL.Code;
            //Header
            MessageType = (ushort)KoneEliMessageTypeEnum.MSG_ACCESS_CONTROL.Code;

            //SubHeader
            ApplicationMsgType = (ushort)KoneEliApplicationMsgTypeEnum.DOP_GLOBAL_DEFAULT_MASK.Code;
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

            AccessMasks = new List<uint>();
        }

        /// <summary>
        /// bit 0 : disconnected state
        /// bit 1 : connected state
        /// bit 2-7 : reserved
        /// </summary>
        public byte SystemState { get; set; }

        /// <summary>
        /// Reserved
        /// </summary>
        public byte Reserved { get; set; }

        /// <summary>
        /// Number of floors in the mask
        /// </summary>
        public ushort NumberOfFloors { get; private set; }

        /// <summary>
        /// uint32[number_of_floors] //0b00011011=27
        /// bit 0 : allowed destination front (1=yes, 0=no)
        /// bit 1 : allowed destination rear(1=yes, 0=no)
        /// bit 2 : reserved
        /// bit 3 : allowed source front(1=yes, 0=no)
        /// bit 4 : allowed source rear(1=yes, 0=no)
        /// bit 5-31 : reserved
        /// </summary>
        public List<uint> AccessMasks { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            SystemState = ReadByte();
            Reserved = ReadByte();
            NumberOfFloors = ReadUnsignedShort();
            AccessMasks = ReadUnsignedInts(NumberOfFloors);
        }

        protected override void Write(bool isLittleEndianess)
        {
            NumberOfFloors = (ushort)AccessMasks.Count;

            base.Write(isLittleEndianess);

            WriteByte(SystemState);
            WriteByte(Reserved);
            WriteUnsignedShort(NumberOfFloors);
            WriteUnsignedInts(AccessMasks);
        }
    }
}
