using System.Collections.Generic;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 访客导入详情查询
    /// </summary>
    public class VisitorImportDetailQuery : PageQuery
    {
        /// <summary>
        /// 记录Id
        /// </summary>
        public string ImportId { get; set; }

        /// <summary>
        /// 姓名(支持模糊搜索)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// IC卡
        /// </summary>
        public string IcCard { get; set; }

        /// <summary>
        /// 状态，成功 ：true、失败：false、全部传空
        /// </summary>
        public string Status { get; set; }
    }
}