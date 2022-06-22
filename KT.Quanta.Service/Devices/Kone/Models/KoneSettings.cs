using System.Collections.Generic;

namespace KT.Quanta.Service.Devices.Kone.Models
{
    /// <summary>
    /// ELI         Elevator Locking Interface
    /// RCGIF       Remote Call Giving Interface
    /// GCAC        Group Controller Access Control
    /// DOP         Destination Operating Panel
    /// COP         Car Operating Panel
    /// TSDOP       Touch Screen Destination Operating Panel
    /// ACS         Access Control System
    /// KCEGC       Kone Control & Electrification Group Controller
    /// CT          Certification Test
    /// Royal-GC    Group Controllers(KCEGC) with group control functionality enabled
    /// </summary>
    public class KoneSettings
    {
        public KoneSettings()
        {
            Rcgif = new KoneRcgifSettings();
            Eli = new KoneEliSettings();
        }

        /// <summary>
        /// 检查是否在线延迟时间
        /// </summary>
        public decimal CheckOnlineDelaySecondTime { get; set; } = 10;

        /// <summary>
        /// 检查是否在线时间(毫秒)
        /// 间隔时间内检查一次，边缘处理器不在线不会自动重连，在检查确认离线后才自动重连
        /// </summary>
        public decimal CheckOnlineSecondTime { get; set; } = 10;

        /// <summary>
        /// Remote Call Giving Interface
        /// </summary>
        public KoneRcgifSettings Rcgif { get; set; }

        /// <summary>
        /// Elevator Locking Interface
        /// </summary>
        public KoneEliSettings Eli { get; set; }
    }

    /// <summary>
    /// Remote Call Giving Interface
    /// </summary>
    public class KoneRcgifSettings
    {
        /// <summary>
        /// 事件循环组个数
        /// </summary>
        public int EventLoopGroupCount { get; set; } = 2;

        /// <summary>
        /// 接收最大长度，超过则忽略掉接收的内容
        /// </summary>
        public int DiscardMinimumLength { get; set; } = 1024;

        /// <summary>
        /// 是否无心跳重连
        /// </summary>
        public bool IsNoHeartbeatReconnect { get; set; } = true;

        /// <summary>
        /// 断开延迟关闭连接时间（秒）
        /// </summary>
        public int CloseDelaySeconds { get; set; } = 3;

        /// <summary>
        /// 断开重新连接延迟时间（秒）
        /// </summary>
        public int ReconnectDelaySeconds { get; set; } = 3;
    }

    /// <summary>
    /// Elevator Locking Interface
    /// </summary>
    public class KoneEliSettings
    {
        public KoneEliSettings()
        {

        }

        /// <summary>
        /// 事件循环组个数
        /// </summary>
        public int EventLoopGroupCount { get; set; } = 2;

        /// <summary>
        /// 接收最大长度，超过则忽略掉接收的内容
        /// </summary>
        public int DiscardMinimumLength { get; set; } = 1024;

        /// <summary>
        /// 是否无心跳重连
        /// </summary>
        public bool IsNoHeartbeatReconnect { get; set; } = true;

        /// <summary>
        /// 断开延迟关闭连接时间（秒）
        /// </summary>
        public int CloseDelaySeconds { get; set; } = 3;

        /// <summary>
        /// 断开重新连接延迟时间（秒）
        /// </summary>
        public int ReconnectDelaySeconds { get; set; } = 3;

        /// <summary>
        /// 设置mask返回错误重发
        /// </summary>
        public bool IsMaskResponseResend { get; set; } = false;

        /// <summary>
        /// 设置mask返回不重发状态
        /// </summary>
        public List<int> MaskNotResendCodes { get; set; }
    }
}
