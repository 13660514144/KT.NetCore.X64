using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Quanta.Service.Entities
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
        public string BrandModel { get; set; }

        /// <summary>
        /// 卡类型
        /// 区卡种,如IC、QR
        /// </summary>
        public string CardDeviceType { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 关联边缘处理器
        /// </summary>
        public string ProcessorId { get; set; }

        /// <summary>
        /// 关联边缘处理器
        /// </summary>
        public ProcessorEntity Processor { get; set; }

        /// <summary>
        /// 派梯设备
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        /// <summary>
        /// 派梯设备
        /// </summary>
        public HandleElevatorDeviceEntity HandleElevatorDevice { get; set; }

        /// <summary>
        /// 串口配置
        /// </summary>
        public string SerialConfigId { get; set; }

        /// <summary>
        /// 串口配置
        /// </summary>
        public SerialConfigEntity SerialConfig { get; set; }

        /// <summary>
        /// 关联继电器
        /// </summary>
        public string RelayDeviceId { get; set; }

        /// <summary>
        /// 关联继电器
        /// </summary>
        public RelayDeviceEntity RelayDevice { get; set; }

        /// <summary>
        /// 继电器输出口
        /// </summary>
        public int RelayDeviceOut { get; set; }
    }
}
