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
    /// COP Specific Default Access Mask Message 
    /// The message type of COP specific default access mask message is 
    /// MSG_ACCESS_CONTROL(0x8003) and it uses koneSubHeader1:
    /// </summary>

    public class KoneEliCopSpecificDefaultAccessMaskMessage : KoneEliSubHeader1Request
    {
        public KoneEliCopSpecificDefaultAccessMaskMessage()
        {
            ApplicationId = (ushort)KoneEliApplicationIdEnum.APP_ACCESS_CONTROL.Code;
            //Header
            MessageType = (ushort)KoneEliMessageTypeEnum.MSG_ACCESS_CONTROL.Code;

            //SubHeader
            ApplicationMsgType = (ushort)KoneEliApplicationMsgTypeEnum.COP_DEFAULT_MASK.Code;
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
        /// Reserved
        /// </summary>
        public byte Reserved { get; set; }

        /// <summary>
        /// ID of the elevator
        /// </summary>
        public byte ElevatorId { get; set; }

        /// <summary>
        /// elevator group id
        /// </summary>
        public byte GroupId { get; set; }

        /// <summary>
        /// Number of floors in the mask. Note: the mask must contain 
        /// the actual floor count of specific lift from bottom floor 
        /// index (1) to top floor index. The Mask can contain more 
        /// floors but no fewer floors compared to lift floor count. In 
        /// general the number of floors is the top floor index of the 
        /// specific lift.
        /// </summary>
        public ushort NumberOfFloors { get; set; }

        /// <summary>
        /// uint32[number_of_floors] //0b00000011 = 3
        /// bit 0 : allowed destination front (1=yes, 0=no)
        /// bit 1 : allowed destination rear (1=yes, 0=no)
        /// bit 2-31 : reserved
        /// </summary>
        public List<uint> AccessMasks { get; private set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            SystemState = ReadByte();
            Reserved = ReadByte();
            ElevatorId = ReadByte();
            GroupId = ReadByte();
            NumberOfFloors = ReadUnsignedShort();
            AccessMasks = ReadUnsignedInts(NumberOfFloors);
        }

        protected override void Write(bool isLittleEndianess)
        {
            base.Write(isLittleEndianess);

            WriteByte(SystemState);
            WriteByte(Reserved);
            WriteByte(ElevatorId);
            WriteByte(GroupId);
            WriteUnsignedShort(NumberOfFloors);

            //Mask楼层前后门权限
            AccessMasks = new List<uint>();
            for (int i = 0; i < NumberOfFloors; i++)
            {
                //0b00000011=3
                AccessMasks.Add(3);
            }
            WriteUnsignedInts(AccessMasks);
        }
    }
}
