using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;

namespace KT.Quanta.Unit.Model.Requests
{
    public class PassRightDetailRequest : QuantaSerializer
    {
        /// <summary>
        /// 楼层Id，可能重复，不能作为主键
        /// </summary>
        public string FloorId { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 真实楼层Id
        /// </summary>
        public string RealFloorId { get; set; }

        /// <summary>
        /// 是否公共楼层
        /// </summary>
        public bool IsPublic { get; set; }

        protected override void Read()
        {
            FloorId = ReadString();
            FloorName = ReadString();
            RealFloorId = ReadString();
            IsPublic = ReadBoolean();
        }

        protected override void Write()
        {
            WriteString(FloorId);
            WriteString(FloorName);
            WriteString(RealFloorId);
            WriteBoolean(IsPublic);
        }
    }
}
