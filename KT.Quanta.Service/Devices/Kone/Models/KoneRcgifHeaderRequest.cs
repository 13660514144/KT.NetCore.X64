using KT.Common.Core.Helpers;
using KT.Quanta.Service.Devices.Kone.Requests;
using System;

namespace KT.Quanta.Service.Devices.Kone.Models
{
    /// <summary>
    /// The header consist of the first 9 bytes of the message,length 9
    /// </summary>
    public class KoneRcgifHeaderRequest : KoneSerializer
    {
        public KoneRcgifHeaderRequest()
        {
            MessageByteOrder = (byte)KoneRcgifMessageByteOrderEnum.BYTE_ORDER_LITTLE_ENDIAN.Code;
        }

        /// <summary>
        /// The endianess of packets can be selected according the implementations needs.
        /// 
        /// 0x80 BYTE_ORDER_BIG_ENDIAN This message is big-endian.
        /// 0x81 BYTE_ORDER_LITTLE_ENDIAN This message is little-endian.
        /// 0x00 BYTE_ORDER_UNDEFINED Byte order of zero value is intentionally specified 
        ///     as undefined.The message is invalid.
        /// Not listed here N/A Undefined byte order, invalid message.
        /// </summary>
        public virtual byte MessageByteOrder { get; set; }

        /// <summary>
        /// Message size is the size of the whole message, measured in bytes. 
        /// It includes the header and subheader sizes.
        /// </summary>
        public virtual uint MessageSize { get; set; }

        /// <summary>
        /// Application ID specifies the application, which the message belongs to. 
        /// This destination call giving application shall only listen to messages that have the type
        /// APP_DESTINATION_CALL(0x8000). All messages that have a different application ID shall be ignored.
        /// </summary>
        public virtual ushort ApplicationId { get; set; }

        /// <summary>
        /// Message type identifies the type of the message. 
        /// This application shall only listen to messages MSG_DESTINATION_CALL(0x8000) and MSG_HEARTBEAT(0x8002). 
        /// Other types of messages shall be ignored.
        /// </summary>
        public virtual ushort MessageType { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            MessageByteOrder = ReadByte();
            MessageSize = ReadUnsignedInt();
            ApplicationId = ReadUnsignedShort();
            MessageType = ReadUnsignedShort();
        }

        protected override void Write(bool isLittleEndianess)
        {
            WriteByte(MessageByteOrder);
            WriteUnsignedInt(MessageSize);
            WriteUnsignedShort(ApplicationId);
            WriteUnsignedShort(MessageType);
        }

        public bool IsLittleEndianess()
        {
            if (MessageByteOrder == KoneRcgifMessageByteOrderEnum.BYTE_ORDER_LITTLE_ENDIAN.Code)
            {
                return true;
            }
            else if (MessageByteOrder == KoneRcgifMessageByteOrderEnum.BYTE_ORDER_BIG_ENDIAN.Code)
            {
                return false;
            }
            else
            {
                throw new ArgumentException("Kone MessageByteOrder Error!");
            }
        }
    }
}
