namespace KT.Quanta.Service.Devices.Quanta.Models
{
    /// <summary>
    /// 派梯显示
    /// </summary>
    public class ElevatorDisplayModel
    {
        /// <summary>
        /// 闸机终端id
        /// </summary>
        public ushort TerminalId { get; set; }

        /// <summary>
        /// 目地楼层
        /// </summary>
        public string DestinationFloor { get; set; }

        /// <summary>
        /// 电梯id
        /// </summary>
        public string ElevatorId { get; set; }

        /// <summary>
        /// 消息Id
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 远程设备id
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        /// <summary>
        /// 读卡器id,包括ic卡、二维码、摄像头等录入设备，用于边缘处理器显示
        /// </summary>
        public string CardDeviceId { get; set; }
    }
}
