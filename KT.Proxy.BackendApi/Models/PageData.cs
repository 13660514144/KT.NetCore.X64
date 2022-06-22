using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 分页数据结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageData<T> : BasePageData
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        public List<T> List { get; set; }
    }

    /// <summary>
    /// 分页数据结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePageData
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
    }
}
