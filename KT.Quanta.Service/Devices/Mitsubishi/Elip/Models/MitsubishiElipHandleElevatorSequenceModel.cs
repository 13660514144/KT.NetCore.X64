using System.Net;

namespace KT.Quanta.Service.Devices.Common
{
    public class MitsubishiElipHandleElevatorSequenceModel
    {
        /// <summary>
        /// 消息Id(派梯结束回调主键)
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 来源序列号
        /// </summary>
        public byte SourceSequenceNumber { get; set; }

        /// <summary>
        /// 设备id（派梯设备，用于回调）
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        /// <summary>
        /// 楼层名称（目的楼层）
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 来源地址（派梯转发时用于答复）
        /// </summary>
        public EndPoint EndPoint { get; set; }

        /// <summary>
        /// 派梯类型（区分派梯来源）
        /// </summary>
        public string HandleElevatorType { get; set; }

        /// <summary>
        /// 派梯模式(单、多楼层派梯)
        /// </summary>
        public string HandleElevatorMode { get; set; }
    }
}
