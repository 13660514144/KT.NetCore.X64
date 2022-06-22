using System;
using System.Collections.Generic;
using System.Text;

namespace KT.TestTool.TestApp.Settings
{
    /// <summary>
    /// 闸机服务配置
    /// </summary>
    public class SocketSettings
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 缓冲池大小
        /// </summary>
        public long Size { get; set; }
    }
}
