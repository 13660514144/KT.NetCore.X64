using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using KT.Turnstile.Common.Enums;

namespace KT.Turnstile.Entity.Entities
{
    /// <summary>
    /// 读卡器设备信息
    /// </summary>
    [Table("CARD_DEVICE")]
    public class CardDeviceEntity : BaseEntity
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备类型
        /// 关联 <see cref="CardDeviceTypeEnum"/> 区分同一卡种不同设备,如 S122读卡器、M31二维码扫描仪
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 卡类型
        /// 关联 <see cref="CardTypeEnum"/> 区卡种,如IC、QR
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 设备描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 继电器输出口
        /// </summary>
        public int RelayDeviceOut { get; set; }

        /// <summary>
        /// 关联边缘处理器
        /// </summary>
        public ProcessorEntity Processor { get; set; }

        /// <summary>
        /// 关联继电器
        /// </summary>
        public RelayDeviceEntity RelayDevice { get; set; }

        /// <summary>
        /// 串口配置
        /// </summary>
        public SerialConfigEntity SerialConfig { get; set; }

        /// <summary>
        /// 派梯设备id
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }
    }
}
