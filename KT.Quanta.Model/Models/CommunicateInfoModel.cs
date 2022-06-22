using KT.Quanta.Service.Models;

namespace KT.Quanta.Service.Dtos
{
    /// <summary>
    /// 通信设备信息
    /// </summary>
    public class CommunicateInfoModel : BaseQuantaModel
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
    }
}
