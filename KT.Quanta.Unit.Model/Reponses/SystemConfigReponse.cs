
using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;

namespace KT.Quanta.Unit.Model.Reponses
{
    public class SystemConfigReponse : QuantaSerializer
    {
        /// <summary>
        /// 服务器ip
        /// </summary>
        public string ServerIp { get; set; }

        /// <summary>
        /// 服务器端口
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// 服务器事件上传地址
        /// </summary>
        public string PushAddress { get; set; }

        /// <summary>
        /// 最后同步数据时间
        /// </summary>
        public long LastSyncTime { get; set; }

        /// <summary>
        /// 错误重试次数
        /// </summary>
        public int ErrorRetimes { get; set; }

        /// <summary>
        /// 错误销毁次数，累计错误到一定次数不再执行重试操作
        /// </summary>
        public int ErrorDestroyRetimes { get; set; }

        /// <summary>
        /// 设备所在楼层
        /// </summary>
        public string DeviceFloorId { get; set; }

        /// <summary>
        /// Id主键，用于当前派梯设备id
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

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

        public SystemConfigReponse()
        {
            ErrorRetimes = 5;
            ErrorDestroyRetimes = 20;
        }

        protected override void Read()
        {
            ServerIp = ReadString();
            ServerPort = ReadInt();
            PushAddress = ReadString();
            LastSyncTime = ReadLong();
            ErrorRetimes = ReadInt();
            ErrorDestroyRetimes = ReadInt();
            DeviceFloorId = ReadString();
            HandleElevatorDeviceId = ReadString();
            FaceAppId = ReadString();
            FaceSdkKey = ReadString();
            FaceActivateCode = ReadString();
            ElevatorGroupId = ReadString();
        }

        protected override void Write()
        {
            WriteString(ServerIp);
            WriteInt(ServerPort);
            WriteString(PushAddress);
            WriteLong(LastSyncTime);
            WriteInt(ErrorRetimes);
            WriteInt(ErrorDestroyRetimes);
            WriteString(DeviceFloorId);
            WriteString(HandleElevatorDeviceId);
            WriteString(FaceAppId);
            WriteString(FaceSdkKey);
            WriteString(FaceActivateCode);
            WriteString(ElevatorGroupId);
        }
    }
}
