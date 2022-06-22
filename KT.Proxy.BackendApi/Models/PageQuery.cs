namespace KT.Proxy.BackendApi.Models
{
    public class PageQuery
    {
        /// <summary>
        /// 分页当前页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页最大条数
        /// </summary>
        public int Size { get; set; }
    }
}