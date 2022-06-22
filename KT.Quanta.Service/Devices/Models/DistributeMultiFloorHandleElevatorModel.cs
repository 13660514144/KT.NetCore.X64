using KT.Elevator.Unit.Entity.Models;
using System.Collections.Generic;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public class DistributeMultiFloorHandleElevatorModel
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
        public string HandleElevatorDeviceId { get; set; }

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
        public List<ElevatorFloorModel> DestinationFloors { get; set; }

        /// <summary>
        /// 卡号，用于日立
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 读卡器id,包括ic卡、二维码、摄像头等录入设备，用于边缘处理器显示
        /// </summary>
        public string CardDeviceId { get; set; }
         
        /// <summary>
        /// 权限id,用于通力关联dop呼梯类型
        /// </summary>
        public string PassRightId { get; set; }
    }
}