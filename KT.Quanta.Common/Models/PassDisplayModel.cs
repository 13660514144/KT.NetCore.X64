using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Common.Models
{
    public class PassDisplayModel
    {
        /// <summary>
        /// 派梯设备id
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        /// <summary>
        /// 显示设备id
        /// </summary>
        [JsonIgnore]
        public string DisplayDeivceId { get; set; }

        /// <summary>
        /// 显示类型
        /// <see cref="KT.Quanta.Common.Enums.PassDisplayTypeEnum"/>
        /// </summary>
        public string DisplayType { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// 通行方式
        /// </summary>
        public string AccessType { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
