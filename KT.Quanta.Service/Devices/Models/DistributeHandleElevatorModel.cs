using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Common
{
    /// <summary>
    /// 派梯
    /// </summary>
    public class DistributeHandleElevatorModel
    {
        /// <summary>
        /// 消息Key
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string SequenceId { get; set; }

        /// <summary>
        /// 设备Key,区分设备的唯一Id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 真实设备Id
        /// </summary>
        public string RealDeviceId { get; set; }

        /// <summary>
        /// 来源楼层
        /// </summary>
        public ElevatorFloorModel SourceFloor { get; set; }

        /// <summary>
        /// 目地楼层
        /// </summary>
        public ElevatorFloorModel DestinationFloor { get; set; }

        /// <summary>
        /// 目地楼层名称，用于返回数据结果
        /// </summary>
        public string DestinationFloorName { get; set; }

        /// <summary>
        /// 人员Id，用于迅达
        /// </summary>
        public long PersonId { get; set; }

        /// <summary>
        /// 是否根据人派梯，用于迅达
        /// </summary>
        public bool IsCallByPerson { get; set; }

        /// <summary>
        /// 卡号，用于日立
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 权限id,用于通力关联dop呼梯类型
        /// </summary>
        public string PassRightId { get; set; }
    }
}
