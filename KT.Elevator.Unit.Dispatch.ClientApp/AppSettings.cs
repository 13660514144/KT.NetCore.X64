namespace KT.Elevator.Unit.Dispatch.ClientApp
{
    /// <summary>
    /// 应用基本配置
    /// 
    /// "Name": "二次派梯一体机",
    /// "Port": 23262,
    /// "PageSize": 28,
    /// "DeviceType": "ELEVATOR_SECONDARY",
    /// "HandleResultOutSecondTime": 30 
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 名称,后台获取设备时传回
        /// </summary>
        public string Name { get; set; } = "电梯边缘处理器";

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 24263;

        /// <summary>
        /// 设备类型
        /// </summary>
        public string StartWithIp { get; set; } = "192.168.0.";

        /// <summary>
        /// 开门命令
        /// </summary>
        public string ServerIp { get; set; } = "192.168.0.183";

        /// <summary>
        /// 端口
        /// </summary>
        public int ServerPort { get; set; } = 24260;

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; } = "ELEVATOR_SERVER";

        /// <summary>
        /// 派梯结果返回有效时间（秒）
        /// 超时未返回本次通行失效，记录不上传
        /// </summary>
        public decimal HandleResultOutSecondTime { get; set; } = 20;

        /// <summary>
        /// 自动返回首页
        /// </summary>
        public decimal SuccessReturnSecondTime { get; set; } = 5;

        /// <summary>
        /// 派梯logo
        /// </summary>
        public string LogoPath { get; set; } = "Resources/Images/logo.png";

        /// <summary>
        /// 标题
        /// </summary>
        public string SystemName { get; set; } = "康塔日立电梯派梯服务";

        /// <summary>
        /// 副标题
        /// </summary>
        public string SystemSecondName { get; set; } = "欢迎您";

        /// <summary>
        /// 是否全屏
        /// </summary>
        public bool IsFullScreen { get; set; } = true;
    }
}
