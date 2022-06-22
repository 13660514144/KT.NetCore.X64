using KT.Quanta.Service.Devices.Kone.Requests;
using System.Net;

namespace KT.Quanta.Service.Devices.Kone.Models
{
    public class KoneEliSequenceModel
    {
        /// <summary>
        /// key
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public KoneEliSequenceTypeEnum Type { get; set; }

        /// <summary>
        /// Dop Id
        /// </summary>
        public string DopId { get; set; }

        /// <summary>
        /// 消息id（序列号）
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        /// <summary>
        /// 楼层名称（目的楼层）
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 读卡器id,包括ic卡、二维码、摄像头等录入设备，用于边缘处理器显示
        /// </summary>
        public string CardDeviceId { get; set; }

        /// <summary>
        /// 发送的数据
        /// </summary>
        public object Data { get; set; }
    }
}
