using KT.Common.Data.Models;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Service.Entities
{
    /// <summary>
    /// 边缘处理器楼层设置
    /// </summary>
    [Table("PROCESSOR_FLOOR")]
    public class ProcessorFloorEntity : BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 关联边缘处理器
        /// </summary>
        public string ProcessorId { get; set; }

        /// <summary>
        /// 关联边缘处理器
        /// </summary>
        public ProcessorEntity Processor { get; set; }

        /// <summary>
        /// 关联楼层
        /// </summary>
        public string FloorId { get; set; }

        /// <summary>
        /// 关联楼层
        /// </summary>
        public FloorEntity Floor { get; set; }
    }
}
