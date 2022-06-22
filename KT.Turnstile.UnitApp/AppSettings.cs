namespace KT.Turnstile.Unit.ClientApp
{
    /// <summary>
    /// 配置信息
    /// 
    /// "Port": 22261,
    /// "RelayOpenCmd": "1,1",
    /// "DeviceType": "ELEVATOR_GATE_DISPLAY",
    /// "AutoReturnSecondTime": 10
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public string StartWithIp { get; set; } = "192.168.0.";

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 22261;

        /// <summary>
        /// 开门命令
        /// </summary>
        public string ServerIp { get; set; } = "192.168.0.183";

        /// <summary>
        /// 端口
        /// </summary>
        public int ServerPort { get; set; } = 24260;

        /// <summary>
        /// 开门命令
        /// </summary>
        public string RelayOpenCmd { get; set; } = "1,1";

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; } = "TURNSTILE_PROCESSOR";

        /// <summary>
        /// Lgo
        /// </summary>
        public string LogoPath { get; set; } = "Resources/Images/logo.png";

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; } = "康塔智慧通行平台";

        /// <summary>
        /// 系统副名称
        /// </summary>
        public string SystemSecondName { get; set; } = "欢迎您";

        /// <summary>
        /// 版权声明
        /// </summary>
        public string SystemCopyright { get; set; } = " QuantaData.All Rights Reserved. 广州康塔科技有限公司  ";

        /// <summary>
        /// 自动返回时间
        /// </summary>
        public int AutoReturnSecondTime { get; set; } = 10;

        /// <summary>
        /// 是否全屏
        /// </summary>
        public bool IsFullScreen { get; set; } = true;

        /// <summary>
        /// 心跳时间
        /// </summary>
        public decimal ServerHeartbeatSecondTime { get; set; } = 10;
    }
}
