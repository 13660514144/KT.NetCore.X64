using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Turnstile.Entity.Entities
{
    /// <summary>
    /// 边缘处理器
    /// 多边缘处理器时会选择一个作为主节点向其它节点分发数据
    /// 或另安装一个服务程序作为中间服务分发数据
    /// </summary>
    [Table("PROCESSOR")]
    public class ProcessorEntity : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

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
