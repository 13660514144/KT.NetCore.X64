using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Quanta.Service.Entities
{
    /// <summary>
    /// 输入设备，精简类型
    /// </summary>
    [Table("HANDLE_ELEVATOR_INPUT_DEVICE")]
    public class HandleElevatorInputDeviceEntity : BaseEntity
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 卡类型
        /// 区卡种,如IC_CARD、QR_CARD
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
        /// 派梯设备
        /// </summary>
        public string HandElevatorDeviceId { get; set; }

        /// <summary>
        /// 派梯设备
        /// </summary>
        public HandleElevatorDeviceEntity HandElevatorDevice { get; set; }
    }
}
