namespace KT.Quanta.Service.Models
{
    /// <summary>
    /// 电梯服务映射
    /// </summary>
    public class ElevatorServerMapSettings
    {
        /// <summary>
        /// 设备型号
        /// </summary>
        public string BrandModel { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 模块类型
        /// </summary>
        public string ModuleType { get; set; }
    }
}