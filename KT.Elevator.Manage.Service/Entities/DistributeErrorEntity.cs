using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Manage.Service.Entities
{
    /// <summary>
    /// 分发错误记录，边缘处理器重新连接时继续分发
    /// </summary>
    [Table("DISTRIBUTE_ERROR")]
    public class DistributeErrorEntity : BaseEntity
    {
        /// <summary>
        /// 推送设备Key
        /// </summary>
        public string DeviceKey { get; set; }

        /// <summary>
        /// 数据分发地址
        /// </summary>
        public string PartUrl { get; set; }

        /// <summary>
        /// 分发数据类名
        /// </summary>
        public string DataModelName { get; set; }

        /// <summary>
        /// 分发数据Id
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        /// 分发错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 分发错误次数
        /// </summary>
        public int ErrorTimes { get; set; }

        /// <summary>
        /// Json格式分发数据
        /// </summary>
        public string DataContent { get; set; }

        /// <summary>
        /// 推送类型
        /// <see cref="KT.Elevator.Common.Enums.DistributeTypeEnum"/>
        /// </summary>
        public string Type { get; set; }
    }
}
