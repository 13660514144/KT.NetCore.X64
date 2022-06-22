using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;

namespace KT.Quanta.Unit.Model.Requests
{
    /// <summary>
    /// 通行权限楼层，
    /// </summary>
    public class FloorRequest : QuantaSerializer
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 梯控楼层Id
        /// </summary>
        public string RealFloorId { get; set; }

        /// <summary>
        /// 是否为公共楼层
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 大厦Id
        /// </summary>
        public string EdificeId { get; set; }

        /// <summary>
        /// 大厦名称
        /// </summary>
        public string EdificeName { get; set; }

        /// <summary>
        /// 关联电梯组
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 关联电梯组
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        protected override void Read()
        {
            Name = ReadString();
            RealFloorId = ReadString();
            IsPublic = ReadBoolean();
            EdificeId = ReadString();
            EdificeName = ReadString();
            ElevatorGroupId = ReadString();
            HandleElevatorDeviceId = ReadString();
        }

        protected override void Write()
        {
            WriteString(Name);
            WriteString(RealFloorId);
            WriteBoolean(IsPublic);
            WriteString(EdificeId);
            WriteString(EdificeName);
            WriteString(ElevatorGroupId);
            WriteString(HandleElevatorDeviceId);
        }
    }
}
