namespace KT.Quanta.Common.Models
{
    /// <summary>
    /// 边缘处理器Socket数据模型
    /// </summary>
    public class SeekSocketModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 服务端Ip地址
        /// </summary>
        public string ServerIp { get; set; }

        /// <summary>
        /// 服务端端口
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// 客户端Ip地址
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// 客户端口
        /// </summary>
        public int ClientPort { get; set; }
    }
}
