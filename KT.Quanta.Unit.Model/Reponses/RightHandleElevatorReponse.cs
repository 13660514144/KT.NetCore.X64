using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;

namespace KT.Quanta.Unit.Model.Reponses
{
    /// <summary>
    /// 根据卡号派梯参数
    /// </summary>
    public class RightHandleElevatorReponse : QuantaSerializer
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

        protected override void Read()
        {
            Sign = ReadString();
            AccessType = ReadString();
            DeviceType = ReadString();
            DeviceId = ReadString();
            FloorId = ReadString();
            HandleElevatorDeviceId = ReadString();
        }

        protected override void Write()
        {
            WriteString(Sign);
            WriteString(AccessType);
            WriteString(DeviceType);
            WriteString(DeviceId);
            WriteString(FloorId);
            WriteString(HandleElevatorDeviceId);
        }
    }
}
