using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Models
{
    /// <summary>
    /// 心跳
    /// </summary>
    public class MitsubishiElipHeartbeatModel : MitsubishiElipSerializer
    {
        /// <summary>
        /// Reserve1
        /// </summary>
        public byte Reserve1 { get; set; }

        /// <summary>
        /// Reserve2
        /// </summary>
        public byte Reserve2 { get; set; }

        /// <summary>
        /// Reserve3
        /// </summary>
        public byte Reserve3 { get; set; }

        protected override void Read()
        {
            Reserve1 = ReadByte();
            Reserve2 = ReadByte();
            Reserve3 = ReadByte();
        }

        protected override void Write()
        {
            WriteByte(Reserve1);
            WriteByte(Reserve2);
            WriteByte(Reserve3);
        }
    }
}
