using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Ports;
using System.Text;

namespace KT.Turnstile.Entity.Entities
{
    /// <summary>
    /// 串口读取信息
    /// 串口配置为公用配置，同一型号设备共用一条配置信息，非公用字段放设备信息中
    /// </summary>
    [Table("SERIAL_CONFIG")]
    public class SerialConfigEntity : BaseEntity
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }

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
    }
}
