using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Data.Models
{
    /// <summary>
    /// 分页数据结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageData<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Totals { get; set; }

        /// <summary>
        /// 分页列表
        /// </summary>
        public List<T> List { get; set; }
    }
}
