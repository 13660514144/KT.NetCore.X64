using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Models
{
    /// <summary>
    /// Transmission Assistant Header
    /// </summary>
    public class MitsubishiElipTransmissionAssistantHeader : MitsubishiElipSerializer
    {
        /// <summary>
        /// Command
        /// </summary>
        public byte Command { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<byte> Datas { get; set; }

        protected override void Read()
        {
            Command = ReadByte();
            Datas = ReadEndBytes().ToList();
        }

        protected override void Write()
        {
            Datas = Datas == null ? new List<byte>() : Datas;

            WriteByte(Command);
            WriteBytes(Datas.ToArray());
        }
    }
}
