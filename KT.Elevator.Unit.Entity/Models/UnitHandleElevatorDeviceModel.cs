using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Unit.Entity.Entities
{
    /// <summary>
    /// 派梯设备，二次派梯一体机数据，边缘处理器直接写在卡设备信息里
    /// </summary>
    public class UnitHandleElevatorDeviceModel
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
        public List<UnitFloorEntity> Floors { get; set; }

        /// <summary>
        /// 设备所在楼层
        /// </summary>
        public string DeviceFloorId { get; set; }
    }
}
