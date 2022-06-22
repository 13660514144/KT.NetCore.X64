using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.SerialDevice.Common.Models
{
    /// <summary>
    /// 接收原始数据
    /// </summary>
    public class SerialMessageModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 接收数据
        /// </summary>
        public byte[] Datas { get; set; }
    }
}
