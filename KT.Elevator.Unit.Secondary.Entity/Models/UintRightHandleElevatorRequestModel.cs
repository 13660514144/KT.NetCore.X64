using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Secodary.Entity.Models
{
    /// <summary>
    /// 根据卡号派梯参数
    /// </summary>
    public class UintRightHandleElevatorRequestModel
    {
        /// <summary>
        /// 通行标记
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 通行类型
        /// </summary>
        public string AccessType { get; set; }

        /// <summary>
        /// 通行设备类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string FloorId { get; set; }

        /// <summary>
        /// 派梯设备
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }
    }
}
