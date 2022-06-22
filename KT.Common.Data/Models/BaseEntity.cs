using KT.Common.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace KT.Common.Data.Models
{
    public class BaseEntity
    {
        /// <summary>
        /// Id主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 新增人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string Editor { get; set; }

        /// <summary>
        /// 新增时间
        /// </summary>
        public long CreatedTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public long EditedTime { get; set; }
         
        public BaseEntity()
        {
            Id = IdUtil.NewId();
            EditedTime = DateTimeUtil.UtcNowMillis();
        }
    }
}
