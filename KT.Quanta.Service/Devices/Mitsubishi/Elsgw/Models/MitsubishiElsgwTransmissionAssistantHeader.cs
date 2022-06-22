using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models
{
    /// <summary>
    /// Transmission Assistant Header
    /// </summary>
    public class MitsubishiElsgwTransmissionAssistantHeader : MitsubishiElsgwSerializer
    {
        /// <summary>
        /// Command Number(01h)
        /// </summary>
        public byte CommandNumber { get; set; }

        /// <summary>
        /// Data Length
        /// 
        /// Byte size of transmission packet data
        /// </summary>
        public byte DataLength { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<byte> Datas { get; set; }

        protected override void Read()
        {
            CommandNumber = ReadByte();
            DataLength = ReadByte();
            Datas = ReadBytes(DataLength).ToList();
        }

        protected override void Write()
        {
            Datas = Datas == null ? new List<byte>() : Datas;

            WriteByte(CommandNumber);
            WriteByte(DataLength);
            WriteBytes(Datas.ToArray());
        }
    }
}
