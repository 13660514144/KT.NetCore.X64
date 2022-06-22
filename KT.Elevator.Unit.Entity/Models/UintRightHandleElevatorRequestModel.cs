using Newtonsoft.Json;

namespace KT.Elevator.Unit.Entity.Models
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
        /// 楼层，如果存在，直接目的层派梯，如果为空或为0，根据权限派梯
        /// </summary>
        [JsonProperty("floorId")]
        public string DestinationFloorId { get; set; }

        /// <summary>
        /// 派梯设备
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        public string CommBox { get; set; }
    }
}
