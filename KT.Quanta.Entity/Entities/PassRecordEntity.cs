using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Service.Entities
{
    /// <summary>
    /// 通行记录
    /// </summary>
    [Table("PASS_RECORD")]
    public class PassRecordEntity : BaseEntity
    {
        /// <summary>
        /// 设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
        /// </summary>
        public string DeviceId { get; set; }

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
        public string PassRightSign { get; set; }

        /// <summary>
        /// 通行时间，2019-11-06 15:20:45
        /// </summary>
        public string PassLocalTime { get; set; }

        /// <summary>
        /// 通行时间
        /// </summary>
        public long PassTime { get; set; }

        /// <summary>
        /// 通行权限Id
        /// </summary>
        public string PassRightId { get; set; }

        /// <summary>
        /// 通行人脸图片
        /// </summary>
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

        /// <summary>
        /// 人体温度
        /// </summary>
        public decimal? Temperature { get; set; }

        /// <summary>
        /// 是否带口罩
        /// </summary>
        public bool? IsMask { get; set; }

    }
}
