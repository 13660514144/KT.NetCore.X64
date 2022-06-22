using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Model.Settings
{
    public class AppSettings
    {
        /// <summary>
        /// 收发信息端口，与http端口相同
        /// </summary>
        public int Port { get; set; } = 22260;

        /// <summary>
        /// 错误重试次数
        /// </summary>
        public int ErrorRetimes { get; set; } = 5;

        /// <summary>
        /// 错误销毁次数，累计错误到一定次数不再执行重试操作
        /// </summary>
        public int ErrorDestroyRetimes { get; set; } = 10;

        /// <summary>
        /// 检查是否在线时间
        /// 间隔时间内检查一次，边缘处理器不在线不会自动重连，在检查确认离线后才自动重连
        /// </summary>
        public int CheckOnlineTime { get; set; } = 10000;

        /// <summary>
        /// 服务消息头
        /// </summary>
        public string SeekHeader { get; set; }
    }
}
