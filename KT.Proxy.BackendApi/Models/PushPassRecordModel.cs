using KT.Common.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 事件上传数据模型
    /// </summary>
    public class PushPassRecordModel
    {
        /// <summary>
        /// 人脸通行时的抓拍图片
        /// </summary>
        public byte[] File { get; set; }

        /// <summary>
        /// 设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
        /// </summary>
        public string EquipmentId { get; set; }

        /// <summary>
        /// 扩展字段，在人脸摄像头时，该字段是人脸服务器的token
        /// </summary>
        public string Extra { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string EquipmentType { get; set; }

        /// <summary>
        /// 通行类型
        /// </summary>
        public string AccessType { get; set; }

        /// <summary>
        /// 通行码，IC卡、二维码、人脸ID
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 通行时间，2019-11-06 15:20:45
        /// </summary>
        public string AccessDate { get; set; }

        /// <summary>
        /// 出入方向
        /// </summary>
        public string WayType { get; set; }

        /// <summary>
        /// 备注，用于填写第三方特别信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 人体温度，null：未开启检查体温功能
        /// </summary>
        public decimal? Temperature { get; set; }

        /// <summary>
        /// 是否戴口罩，true：戴口罩、false：未戴口罩、null：未开启检查口罩功能
        /// </summary>
        public bool? Mask { get; set; }
    }
}
