using KT.Quanta.Service.Devices.Kone.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    /// <summary>
    /// The subheader used in most of the messages used in this protocol is koneSubHeader1.length 20 add header total 29
    /// 
    /// 3、10、11、12楼
    /// error data:04 00 01 01 01 00 00 00 00 00 00 00 80 FE 2A A4 4A EE D6 01 01 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
    /// ture data :04 00 01 01 02 00 00 00 00 00 00 00 40 88 50 66 4B EE D6 01 01 01 00 98 3A 31 00 00 0C 00 0F 00 0C 0E 00 00 00 00 00 0D 00 04 00 03 00 00 03 0A 00 00 03 0B 00 00 03 0C 00 00 03
    ///
    /// calltype:0 lifts:0 distinations:3、10、11、12楼 
    /// demo data      :0B 00 01 01 03 00 00 00 00 00 00 00 40 7E 19 81 F8 EE D6 01 
    /// own error data :0B 00 01 01 01 00 00 00 00 00 00 00 E0 DC E0 BC 00 EF D6 01 
    /// </summary>
    public class KoneEliSubHeader1Request : KoneEliHeaderRequest
    {
        public KoneEliSubHeader1Request()
        {
            //The time when the message is sent.Time stamp type is 
            //windows FILETIME(units of 100ns since January 1, 1601 UTC ) as local time
            TimeStamp = KoneHelper.GetLocalFileTime();
        }

        /// <summary>
        /// Type of the message for the application. 0x0000 is 
        /// undefined and shall not be used as 
        /// applicationMsgType by any application.
        /// </summary>
        public virtual ushort ApplicationMsgType { get; set; }

        /// <summary>
        /// Message data protocol major version for the 
        /// application. 0x0000 is undefined and shall not be
        /// used as majorVersion by any application.
        /// Different major versions the protocol are incomptible.
        /// </summary>
        public virtual byte MajorVersion { get; set; }

        /// <summary>
        /// Message data protocol minor version for the 
        /// application.Different minor versions of the same
        /// major version are extensions of lower minor versions,
        /// i.e.the message data after this subheader is only
        /// larger, not changed in the larger minor versions.
        /// </summary>
        public virtual byte MinorVersion { get; set; }

        /// <summary>
        /// Unique message ID by the sender point of view. 
        /// 0x00000000 is undefined and shall not be used as 
        /// messageID by any application.
        /// </summary>
        public virtual uint MessageId { get; set; }

        /// <summary>
        /// If this message is a response to another message, the 
        /// other message’s ID is set to this field.ResponseID of
        /// value 0x00000000 is defined and means that the
        /// message is not a response to another message.
        /// </summary>
        public virtual uint ResponseId { get; set; }

        /// <summary>
        /// The time when the message is sent.Time stamp type is 
        /// windows FILETIME(units of 100ns since January 1,
        /// 1601 UTC ) as local time
        /// </summary>
        public virtual ulong TimeStamp { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            ApplicationMsgType = ReadUnsignedShort();
            MajorVersion = ReadByte();
            MinorVersion = ReadByte();
            MessageId = ReadUnsignedInt();
            ResponseId = ReadUnsignedInt();
            TimeStamp = ReadUnsignedLong();
        }

        protected override void Write(bool isLittleEndianess)
        {
            base.Write(isLittleEndianess);

            WriteUnsignedShort(ApplicationMsgType);
            WriteByte(MajorVersion);
            WriteByte(MinorVersion);
            WriteUnsignedInt(MessageId);
            WriteUnsignedInt(ResponseId);
            WriteUnsignedLong(TimeStamp);
        }
    }
}
