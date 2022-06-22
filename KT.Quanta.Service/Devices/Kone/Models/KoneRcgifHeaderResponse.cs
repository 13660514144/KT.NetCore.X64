using KT.Quanta.Service.Devices.Kone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Requests
{
    public class KoneRcgifHeaderResponse : KoneRcgifHeaderRequest
    {
        /// <summary>
        /// 数据结果bytes
        /// </summary>
        public byte[] Datas { get; set; }

        protected override void Read(bool isLittleEndianess)
        {
            base.Read(isLittleEndianess);

            Datas = ReadEndBytes();
        }

        protected override void Write(bool isLittleEndianess)
        {
            base.Write(isLittleEndianess);
            WriteBytes(Datas);
        }
    }
}
