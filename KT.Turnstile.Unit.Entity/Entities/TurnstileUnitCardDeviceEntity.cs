using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using KT.Device.Unit.CardReaders.Models;

namespace KT.Turnstile.Unit.Entity.Entities
{
    /// <summary>
    /// 读卡器设备信息
    /// </summary>
    [Table("CARD_DEVICE")]
    public class TurnstileUnitCardDeviceEntity : BaseEntity, ICardDeviceModel
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        [Column("ProductType")]
        public string BrandModel { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 继电器IP地址
        /// </summary>
        public string RelayCommunicateType { get; set; }

        /// <summary>
        /// 继电器IP地址
        /// </summary>
        public string RelayDeviceIp { get; set; }

        /// <summary>
        /// 继电器端口
        /// </summary>
        public int RelayDevicePort { get; set; }

        /// <summary>
        /// 继电器输出口
        /// </summary>
        public int RelayDeviceOut { get; set; }

        /// <summary>
        /// 继电器开门命令
        /// </summary>
        public string RelayOpenCmd { get; set; }

        /// <summary>
        /// 派梯设备Id
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }



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
