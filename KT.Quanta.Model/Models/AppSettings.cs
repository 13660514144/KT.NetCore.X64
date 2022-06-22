using KT.Quanta.Service.Devices.Hikvision.Models;
using System.Collections.Generic;

namespace KT.Quanta.Service.Models
{
    public class AppSettings
    {
        public AppSettings()
        {
            HikvisionTypeParameters = new List<HikvisionTypeParameterModel>();
            ElevatorServerMaps = new List<ElevatorServerMapSettings>();
            Mitsubishi = new MitsubishiSettings();
        }

        /// <summary>
        /// 收发信息端口，与http端口相同
        /// </summary>
        public int Port { get; set; } = 23260;

        /// <summary>
        /// 错误重试次数
        /// </summary>
        public int ErrorRetimes { get; set; } = 5;

        /// <summary>
        /// 错误销毁次数，累计错误到一定次数不再执行重试操作
        /// </summary>
        public int ErrorDestroyRetimes { get; set; } = 10;

        /// <summary>
        /// 检查是否在线延迟时间
        /// </summary>
        public decimal CheckOnlineDelaySecondTime { get; set; } = 10;

        /// <summary>
        /// 检查是否在线时间(秒)
        /// 间隔时间内检查一次，边缘处理器不在线不会自动重连，在检查确认离线后才自动重连
        /// </summary>
        public decimal CheckOnlineSecondTime { get; set; } = 10;

        /// <summary>
        /// 数据上传默认地址,如果数据中存在，则使用数据库中的数据
        /// </summary>
        public string DefaultPushAddress { get; set; } = "http://127.0.0.1:81/openapi/access/log/push";

        /// <summary>
        /// 海康设备账号
        /// </summary>
        public string HikvisionAccount { get; set; } = "admin";

        /// <summary>
        /// 海康设备密码
        /// </summary>
        public string HikvisionPassword { get; set; } = "admin123";

        /// <summary>
        /// 等待socket返回过期时间
        /// </summary>
        public int SocketReturnTimeOutMillis { get; set; } = 60000;

        /// <summary>
        /// 是否异步开启远程设备
        /// </summary>
        public bool IsAsyncInitRemoteDevice { get; set; } = true;

        /// <summary>
        /// 海康布防类型
        /// </summary>
        public List<HikvisionTypeParameterModel> HikvisionTypeParameters { get; set; }

        /// <summary>
        /// 海康布防类型
        /// </summary>
        public List<ElevatorServerMapSettings> ElevatorServerMaps { get; set; }

        /// <summary>
        /// 三菱电梯转换器
        /// </summary>
        public MitsubishiSettings Mitsubishi { get; set; }
    }

    public class MitsubishiSettings
    {
        public MitsubishiSettings()
        {
            Towards = new List<MitsubishiTowardSettings>();
        }

        public List<MitsubishiTowardSettings> Towards { get; set; }
    }

    public class MitsubishiTowardSettings
    {
        /// <summary>
        /// 端口
        /// </summary>
        public int ClientPort { get; set; } = 60173;

        /// <summary>
        /// 端口
        /// </summary>
        public int ServerPort { get; set; } = 52000;

        /// <summary>
        /// 电梯ip
        /// </summary>
        public string ElevatorIp { get; set; } = "192.168.0.49";

        /// <summary>
        /// 电梯端口
        /// </summary>
        public int ElevatorPort { get; set; } = 60173;
    }
}
