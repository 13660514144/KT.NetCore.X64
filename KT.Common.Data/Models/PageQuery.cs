using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Data.Models
{
    /// <summary>
    /// 分页数据结构
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    public class PageQuery<TQuery>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public TQuery Query { get; set; }
    }
}
