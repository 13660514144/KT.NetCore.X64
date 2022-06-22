using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;

namespace KT.Quanta.Unit.Model.Reponses
{
    /// <summary>
    /// 派梯参数
    /// </summary>
    public class HandleElevatorReponse : QuantaSerializer
    {
        /// <summary>
        /// 来源楼层
        /// </summary>
        public string SourceFloorId { get; set; }

        /// <summary>
        /// 派梯楼层
        /// </summary>
        public string DestinationFloorId { get; set; }

        /// <summary>
        /// 派梯设备
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        protected override void Read()
        {
            SourceFloorId = ReadString();
            DestinationFloorId = ReadString();
            HandleElevatorDeviceId = ReadString();
        }

        protected override void Write()
        {
            WriteString(SourceFloorId);
            WriteString(DestinationFloorId);
            WriteString(HandleElevatorDeviceId);
        }
    }
}
