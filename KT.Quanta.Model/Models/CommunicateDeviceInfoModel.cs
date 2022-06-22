using KT.Common.Core.Utils;
using KT.Quanta.Service.Models;

namespace KT.Quanta.Service.Dtos
{
    /// <summary>
    /// 通信设备信息
    /// </summary>
    public class CommunicateDeviceInfoModel : BaseQuantaModel
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 通信设备
        /// <see cref="KT.Quanta.Common.Enums.CommunicateDeviceTypeEnum"/>
        /// </summary>
        public string CommunicateDeviceType { get; set; }

        /// <summary>
        /// 通信方式
        /// <see cref="KT.Quanta.Common.Enums.CommunicateModeTypeEnum"/>
        /// </summary>
        public string CommunicateModeType { get; set; }

        /// <summary>
        /// 通信连接id
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public long LastLoginTime { get; set; }

        /// <summary>
        /// 最后在线时间
        /// </summary>
        public long LastOnlineTime { get; set; }

        /// <summary>
        /// 失败重连
        /// </summary>
        public int ReloginTimes { get; set; }

        public CommunicateDeviceInfoModel()
        {
            Id = IdUtil.NewId();
            LastOnlineTime = DateTimeUtil.UtcNowMillis();
        }
    }
}
