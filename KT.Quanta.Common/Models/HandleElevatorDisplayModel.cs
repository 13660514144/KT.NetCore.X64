namespace KT.Quanta.Common.Models
{
    /// <summary>
    /// 派梯结果
    /// </summary>
    public class HandleElevatorDisplayModel
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 派梯楼层名称
        /// </summary>
        public string DestinationFloorName { get; set; }

        /// <summary>
        /// 派梯电梯名称
        /// </summary>
        public string ElevatorName { get; set; }

        /// <summary>
        /// 物理楼层(宝盾派梯显示屏)
        /// </summary>
        public int PhysicsFloor { get; set; }

        /// <summary>
        /// 电梯编号(宝盾派梯显示屏)
        /// </summary>
        public int ElevatorNumber { get; set; }
    }
}
