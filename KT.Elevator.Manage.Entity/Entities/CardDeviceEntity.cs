using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Manage.Entity.Entities
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
        /// 区分同一卡种不同设备,如 S122读卡器、M31二维码扫描仪
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 卡类型
        /// 区卡种,如IC、QR
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 关联边缘处理器
        /// </summary>
        public ProcessorEntity Processor { get; set; }

        /// <summary>
        /// 派梯设备
        /// </summary>
        public HandElevatorDeviceEntity HandElevatorDevice { get; set; }

        /// <summary>
        /// 串口配置
        /// </summary>
        public SerialConfigEntity SerialConfig { get; set; }
    }
}
