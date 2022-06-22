using System.Collections.Generic;

namespace KT.Elevator.Unit.Processor.ClientApp
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
        public string Name { get; set; } = "二次派梯一体机";

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 23262;

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
        public string StartWithIp { get; set; } = "192.168.0.";

        /// <summary>
        /// 楼层显示每页条数
        /// </summary>
        public int PageSize { get; set; } = 28;

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; } = "ELEVATOR_SECONDARY";

        /// <summary>
        /// 派梯结果返回有效时间（秒）
        /// 超时未返回本次通行失效，记录不上传
        /// </summary>
        public decimal HandleResultOutSecondTime { get; set; } = 20;

        /// <summary>
        /// 自动返回首页
        /// </summary>
        public decimal DestinationPanelReturnSecondTime { get; set; } = 10;

        /// <summary>
        /// 自动返回首页
        /// </summary>
        public decimal SuccessReturnSecondTime { get; set; } = 5;

        /// <summary>
        /// 自动返回首页
        /// </summary>
        public decimal NotRightReturnSecondTime { get; set; } = 2.5M;

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
        /// 是否全屏
        /// </summary>
        public bool IsFullScreen { get; set; } = true;

        /// <summary>
        /// 人脸识别再启用时间
        /// </summary>
        public decimal FaceRestartSecondTime { get; set; } = 1.2M;

        /// <summary>
        /// 灯光控制
        /// </summary>
        public LampSwitchSettings LampSwitch { get; set; }

        /// <summary>
        /// 是否启用自动派梯
        /// </summary>
        public bool IsEnableAutoHandleElevator { get; set; } = true;

        /// <summary>
        /// 是否启用手动派梯
        /// </summary>
        public bool IsEnableManualHandleElevator { get; set; } = true;

        /// <summary>
        /// 考拉人脸识别地址
        /// </summary>
        public string KoalaUrl { get; set; } = "http://192.168.0.100:8866";

        /// <summary>
        /// 心跳时间
        /// </summary>
        public decimal ServerHeartbeatSecondTime { get; set; } = 10;
    }

    /// <summary>
    /// 灯开关控制
    /// </summary>
    public class LampSwitchSettings
    {
        public List<TimeRangeSettings> TimeRanges { get; set; }
    }

    /// <summary>
    /// 灯开关控制
    /// </summary>
    public class TimeRangeSettings
    {
        public string Start { get; set; }
        public string End { get; set; }
    }
}
