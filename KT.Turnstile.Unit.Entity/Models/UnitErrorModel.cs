using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Unit.Entity.Models
{
    /// <summary>
    /// 分发错误记录，边缘处理器重新连接时继续分发
    /// </summary>
    public class UnitErrorModel
    {
        /// <summary>
        /// 推送类型 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Json格式分发数据
        /// </summary>
        public string DataContent { get; set; }

        /// <summary>
        /// 为上传的数据更新时间，用于排序
        /// </summary>
        public long EditTime { get; set; }

    }
}
