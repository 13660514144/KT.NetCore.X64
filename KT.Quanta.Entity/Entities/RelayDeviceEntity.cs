using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Quanta.Service.Entities
{
    /// <summary>
    /// 继电器设备信息
    /// </summary>
    [Table("RELAY_DEVICE")]
    public class RelayDeviceEntity : BaseEntity
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备类型,如泥人继电器、巨人继电器等
        /// <see cref="RelayDeviceTypeEnum"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 通信类型，如TCP、UDP
        /// <see cref="RelayCommunicateTypeEnum"/>
        /// </summary>
        public string CommunicateType { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 设备描述
        /// </summary>
        public string Remark { get; set; }
    }
}
