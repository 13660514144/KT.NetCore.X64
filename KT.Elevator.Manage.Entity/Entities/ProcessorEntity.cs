using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Manage.Entity.Entities
{
    /// <summary>
    /// 边缘处理器
    /// </summary>
    [Table("PROCESSOR")]
    public class ProcessorEntity : BaseEntity
    {
        /// <summary>
        /// 边缘处理器名称
        /// </summary>        
        public string Name { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 品牌型号
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public FloorEntity Prosition { get; set; }

        /// <summary>
        /// 最后同步数据时间
        /// </summary>
        public long SyncDataTime { get; set; }

        /// <summary>
        /// 是否有推送错误数据
        /// </summary>
        public bool HasDistributeError { get; set; }

        /// <summary>
        /// 边缘处理器自动生成Key
        /// </summary>
        public string ProcessorKey { get; set; }
    }
}
