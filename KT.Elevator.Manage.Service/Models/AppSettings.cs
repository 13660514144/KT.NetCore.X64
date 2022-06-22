using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Models
{
    public class AppSettings
    {
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
        /// 检查是否在线时间
        /// 间隔时间内检查一次，边缘处理器不在线不会自动重连，在检查确认离线后才自动重连
        /// </summary>
        public int CheckOnlineTime { get; set; } = 10000;

        /// <summary>
        /// 与客户端通信头
        /// </summary>
        public string SeekHeader { get; set; } = "b3da361289dd466eb61aa5f61ed4a4a9";

        /// <summary>
        /// 与客户端通信头
        /// </summary>
        public string HikvisionAccount { get; set; } = "admin";

        /// <summary>
        /// 与客户端通信头
        /// </summary>
        public string HikvisionPassword { get; set; } = "admin123";

        /// <summary>
        /// 数据上传默认地址,如果数据中存在，则使用数据库中的数据
        /// </summary>
        public string DefaultPushAddress { get; set; } = "http://192.168.0.219:8080/openapi/access/log/push";
    }
}
