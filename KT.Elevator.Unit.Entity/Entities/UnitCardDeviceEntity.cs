using KT.Common.Data.Models;
using KT.Device.Unit.CardReaders.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Unit.Entity.Entities
{
    /// <summary>
    /// 读卡器设备信息
    /// </summary>
    [Table("CARD_DEVICE")]
    public class UnitCardDeviceEntity : BaseEntity, ICardDeviceModel
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public string BrandModel { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 串口配置Id
        /// </summary>
        public string SerialConfigId { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int Baudrate { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int Databits { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public int Stopbits { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        public int Parity { get; set; }

        /// <summary>
        /// 数据读取超时
        /// </summary>
        public int ReadTimeout { get; set; }

        /// <summary>
        /// 编码方式
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// 派梯设备Id
        /// </summary>
        public string HandleDeviceId { get; set; }
    }
}
