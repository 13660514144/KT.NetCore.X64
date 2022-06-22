using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.IdReader.Common
{
    /// <summary>
    /// 阅读器配置
    /// </summary>
    public class IdReaderSettings
    {
        /// <summary>
        /// 阅读器记录Debug
        /// </summary>
        public bool IsLogDebugIdReader { get; set; }

        /// <summary>
        /// 读取时间间隔（秒）
        /// </summary>
        public int ReadeTimeSecond { get; set; } = 1;
    }
}
