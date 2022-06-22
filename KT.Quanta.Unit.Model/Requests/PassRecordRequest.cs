using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;
using Newtonsoft.Json;

namespace KT.Quanta.Unit.Model.Requests
{
    /// <summary>
    /// 通行记录
    /// </summary> 
    public class PassRecordRequest : QuantaSerializer
    {
        /// <summary>
        /// 设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 通行人脸图片
        /// </summary>
        [JsonIgnore]
        public byte[] FaceImage { get; set; }

        /// <summary>
        /// 人脸图片大小
        /// </summary>
        public long FaceImageSize { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 通行类型
        /// </summary>
        public string AccessType { get; set; }

        /// <summary>
        /// 通行码，IC卡、二维码、人脸ID
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 通行时间，2019-11-06 15:20:45
        /// </summary>
        public string PassLocalTime { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Extra { get; set; }

        /// <summary>
        /// 出入方向
        /// </summary>
        public string WayType { get; set; }

        /// <summary>
        /// 备注，用于填写第三方特别信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 通行时间
        /// </summary>
        public long PassTime { get; set; }

        /// <summary>
        /// 通行权限Id
        /// </summary>
        public string PassRightId { get; set; }

        /// <summary>
        /// 上传设备Id,用于当前输入设备（DeviceId）未知的情况下从服务端数据中查找
        /// </summary>
        public string UnitDeviceId { get; set; }

        /// <summary>
        /// 上传设备类型,用于当前输入设备（DeviceId）未知的情况下从服务端数据中查找
        /// </summary>
        public string UnitDeviceType { get; set; }

        protected override void Read()
        {
            DeviceId = ReadString();
            FaceImage = ReadBytes();
            FaceImageSize = ReadLong();
            DeviceType = ReadString();
            AccessType = ReadString();
            CardNumber = ReadString();
            PassLocalTime = ReadString();
            Extra = ReadString();
            WayType = ReadString();
            Remark = ReadString();
            PassTime = ReadLong();
            PassRightId = ReadString();
            UnitDeviceId = ReadString();
            UnitDeviceType = ReadString();
        }

        protected override void Write()
        {
            WriteString(DeviceId);
            WriteBytes(FaceImage);
            WriteLong(FaceImageSize);
            WriteString(DeviceType);
            WriteString(AccessType);
            WriteString(CardNumber);
            WriteString(PassLocalTime);
            WriteString(Extra);
            WriteString(WayType);
            WriteString(Remark);
            WriteLong(PassTime);
            WriteString(PassRightId);
            WriteString(UnitDeviceId);
            WriteString(UnitDeviceType);
        }
    }
}
