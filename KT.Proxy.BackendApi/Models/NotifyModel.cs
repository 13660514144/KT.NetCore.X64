using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 通知
    /// </summary>
    public class NotifyModel
    {
        /// <summary>
        /// 大厦
        /// </summary>
        public List<string> EdificeIds { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public List<string> NoticeTypes { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }

        public NotifyModel()
        {
            EdificeIds = new List<string>();
            NoticeTypes = new List<string>();
        }
    }
}
