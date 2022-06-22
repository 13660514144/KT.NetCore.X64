using KT.Common.Data.Models;
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
    [Table("HANDLE_ELEVATOR_INPUT_DEVICE")]
    public class UnitHandleElevatorInputDeviceEntity : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string AccessType { get; set; }

        /// <summary>
        /// 设备类型
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
        public string HandDeviceId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string HandDeviceRealId { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string HandDeviceType { get; set; }

        /// <summary>
        /// 品牌型号
        /// </summary>
        public string HandDeviceEquipmentType { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string HandDeviceIp { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public int HandDevicePort { get; set; }

        /// <summary>
        /// 通信类型，如TCP、UDP
        /// </summary>
        public string HandDeviceCommunicateType { get; set; }
    }
}
