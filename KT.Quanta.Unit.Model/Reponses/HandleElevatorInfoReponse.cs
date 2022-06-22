using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KT.Quanta.Unit.Model.Reponses
{
    /// <summary>
    /// 用于记录最后一次派梯信息
    /// </summary>
    public class HandleElevatorInfoReponse : HandleElevatorReponse
    {
        public HandleElevatorInfoReponse()
        {
            HandleElevatorRights = new List<ElevatorUnitHandleElevatorRightReponse>();
        }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string DestinationFloorName { get; set; }

        /// <summary>
        /// 拥有的权限，未通行前必须使用这权限值
        /// </summary>
        public List<ElevatorUnitHandleElevatorRightReponse> HandleElevatorRights { get; set; }

        /// <summary>
        /// 通行的权限
        /// </summary>
        public ElevatorUnitHandleElevatorRightReponse HandleElevatorRight { get; set; }

        /// <summary>
        /// 卡类型，IC_CARD、QR_CODE...
        /// </summary>
        public string AccessType { get; set; }

        /// <summary>
        /// 通行设备Id 
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 设备类型,IC、QR...
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 通行时间
        /// </summary>
        public long PassTime { get; set; }

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


        protected override void Read()
        {
            base.Read();

            DestinationFloorName = ReadString();
            HandleElevatorRights = ReadList<ElevatorUnitHandleElevatorRightReponse>();
            HandleElevatorRight = ReadObject<ElevatorUnitHandleElevatorRightReponse>();
            AccessType = ReadString();
            DeviceId = ReadString();
            DeviceType = ReadString();
            PassTime = ReadLong();
            FaceImage = ReadBytes();
            FaceImageSize = ReadLong();
            Extra = ReadString();
            WayType = ReadString();
            Remark = ReadString(); 
        }

    protected override void Write()
        {
            base.Write();

            WriteString(DestinationFloorName);
            WriteList(HandleElevatorRights);
            WriteObject(HandleElevatorRight);
            WriteString(AccessType);
            WriteString(DeviceId);
            WriteString(DeviceType);
            WriteLong(PassTime);
            WriteBytes(FaceImage);
            WriteLong(FaceImageSize);
            WriteString(Extra);
            WriteString(WayType);
            WriteString(Remark); 
        }

    }

    /// <summary>
    /// 权限
    /// </summary>
    public class ElevatorUnitHandleElevatorRightReponse : QuantaSerializer
    {
        /// <summary>
        /// 通行权限
        /// </summary>
        public string PassRightId { get; set; }

        /// <summary>
        /// 通行标记，人脸Id、IC卡号...
        /// </summary>
        public string PassRightSign { get; set; }

        protected override void Read()
        {
            PassRightId = ReadString();
            PassRightSign = ReadString();
        }

        protected override void Write()
        {
            WriteString(PassRightId);
            WriteString(PassRightSign);
        }
    }
}
