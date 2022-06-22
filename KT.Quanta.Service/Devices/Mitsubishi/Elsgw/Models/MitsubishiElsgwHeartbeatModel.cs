using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models
{
    /// <summary>
    /// 心跳
    /// </summary>
    public class MitsubishiElsgwHeartbeatModel : MitsubishiElsgwSerializer
    {
        ///// <summary>
        ///// Command Number(F1h=241)
        ///// </summary>
        //public byte CommandNumber { get; set; }

        ///// <summary>
        ///// Data Length(6)
        ///// </summary>
        //public byte DataLength { get; set; }

        /// <summary>
        /// Having Data Towards Elevator
        /// 
        /// When using Data1 or Data2,set1
        /// Don't use Data1 or Data2,set0
        /// </summary>
        public byte HavingDataTowardsElevator { get; set; } = 0;

        /// <summary>
        /// Data1
        /// </summary>
        public byte Data1 { get; set; } = 0;

        /// <summary>
        /// Data2
        /// </summary>
        public byte Data2 { get; set; } = 0;

        ///// <summary>
        ///// Reserve1
        ///// </summary>
        //public byte Reserve1 { get; set; }

        ///// <summary>
        ///// Reserve2
        ///// </summary>
        //public byte Reserve2 { get; set; }

        ///// <summary>
        ///// Reserve3
        ///// </summary>
        //public byte Reserve3 { get; set; }

        protected override void Read()
        {
            //CommandNumber = ReadByte();
            //DataLength = ReadByte();
            HavingDataTowardsElevator = ReadByte();
            Data1 = ReadByte();
            Data2 = ReadByte();
            //Reserve1 = ReadByte();
            //Reserve2 = ReadByte();
            //Reserve3 = ReadByte();
        }

        protected override void Write()
        {
            //WriteByte(CommandNumber);
            //WriteByte(DataLength);
            WriteByte(HavingDataTowardsElevator);
            WriteByte(Data1);
            WriteByte(Data2);
            //WriteByte(Reserve1);
            //WriteByte(Reserve2);
            //WriteByte(Reserve3);
        }
    }
}
