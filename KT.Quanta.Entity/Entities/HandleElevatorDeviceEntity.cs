using KT.Common.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KT.Quanta.Service.Entities
{
    /// <summary>
    /// 派梯设备
    /// </summary>
    [Table("HANDLE_ELEVATOR_DEVICE")]
    public class HandleElevatorDeviceEntity : BaseEntity
    {
        /// <summary>
        /// 派梯设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 品牌型号
        /// </summary>
        public string BrandModel { get; set; }

        /// <summary>
        /// 通信类型，如TCP、UDP
        /// </summary>
        public string CommunicateType { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; }

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
        /// 所在位置
        /// </summary>
        public string FloorId { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public FloorEntity Floor { get; set; }

        /// <summary>
        /// 关联电梯组
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 关联电梯组
        /// </summary>
        public ElevatorGroupEntity ElevatorGroup { get; set; }

        //关联边缘处理器
        public string ProcessorId { get; set; }

        //关联边缘处理器
        public ProcessorEntity Processor { get; set; }
        
        /// <summary>
        /// 派梯终端，具体的COM口，对应到日立通讯箱 2021.05.25
        /// </summary>        
        public string CommBox { get; set; }
    }
}
