using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;
using KT.Quanta.Unit.Model.Requests;
using System.Collections.Generic;

namespace KT.Quanta.Unit.Model.Reponses
{
    /// <summary>
    /// 派梯设备，二次派梯一体机数据，边缘处理器直接写在卡设备信息里
    /// </summary>
    public class HandleElevatorDeviceReponse : QuantaSerializer
    {
        /// <summary>
        /// Id主键，用于当前派梯设备id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 人脸AppID
        /// </summary>
        public string FaceAppId { get; set; }

        /// <summary>
        /// 人脸SDK KEY
        /// </summary>
        public string FaceSdkKey { get; set; }

        /// <summary>
        /// 人脸激活码
        /// </summary>
        public string FaceActivateCode { get; set; }

        /// <summary>
        /// 关联电梯组Id
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 可去楼层
        /// </summary>
        public List<FloorRequest> Floors { get; set; }

        /// <summary>
        /// 设备所在楼层
        /// </summary>
        public string DeviceFloorId { get; set; }

        protected override void Read()
        {
            Id = ReadString();
            FaceAppId = ReadString();
            FaceSdkKey = ReadString();
            FaceActivateCode = ReadString();
            ElevatorGroupId = ReadString();
            Floors = ReadList<FloorRequest>();
            DeviceFloorId = ReadString();
        }

        protected override void Write()
        {
            WriteString(Id);
            WriteString(FaceAppId);
            WriteString(FaceSdkKey);
            WriteString(FaceActivateCode);
            WriteString(ElevatorGroupId);
            WriteList(Floors);
            WriteString(DeviceFloorId);
        }
    }
}
