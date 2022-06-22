using KT.Common.Netty.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Quanta.Models
{
    /// <summary>
    /// 发送数据对象
    /// </summary>
    public class QuantaSendDataModel
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public string CommunicatorKey { get; set; }

        /// <summary>
        /// 传输数据
        /// </summary>
        public QuantaNettyHeader QuantaNettyHeader { get; set; }
    }
}
