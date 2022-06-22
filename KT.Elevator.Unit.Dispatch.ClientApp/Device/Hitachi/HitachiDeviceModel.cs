using KT.Common.Data.Models;
using KT.Quanta.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Unit.CardReaders.Models
{
    public class HitachiDeviceModel : ICardDeviceModel
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public string BrandModel { get; set; } = BrandModelEnum.HITACHI_DFRS.Value;

        /// <summary>
        /// 卡类型
        /// </summary>
        public string DeviceType { get; set; } = DeviceTypeEnum.ELEVATOR_SERVER.Value;

        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 串口波特率
        /// </summary>
        public int Baudrate { get; set; }

        /// <summary>
        /// 串口数据位
        /// </summary>
        public int Databits { get; set; }

        /// <summary>
        /// 串口停止位
        /// </summary>
        public int Stopbits { get; set; }

        /// <summary>
        /// 串口校验位
        /// </summary>
        public int Parity { get; set; }

        /// <summary>
        /// 串口数据读取超时
        /// </summary>
        public int ReadTimeout { get; set; }

        /// <summary>
        /// 串口编码方式
        /// </summary>
        public string Encoding { get; set; }
    }
}
