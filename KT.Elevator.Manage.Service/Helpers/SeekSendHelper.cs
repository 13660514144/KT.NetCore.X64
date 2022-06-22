using KT.Common.Core.Utils;
using KT.Elevator.Common;
using KT.Elevator.Manage.Service.Models;
using KT.Quanta.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Helpers
{
    /// <summary>
    /// 用于边缘处理器之间广播数据接收
    /// 拆分成发送与监听两个UDP客户端，否则同一台电脑不能开启多个应用
    /// </summary>
    public class SeekSendHelper : IDisposable
    {
        private ILogger<SeekSendHelper> _logger;
        private AppSettings _appsettings;
        private IServiceProvider _serviceProvider;

        // 用于UDP接收的网络服务类
        private UdpClient _udpClient;
        // 是否已启动接收
        private bool _isStartedReceive;

        public SeekSendHelper(ILogger<SeekSendHelper> logger,
            IOptions<AppSettings> appsettings,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _appsettings = appsettings.Value;
            _serviceProvider = serviceProvider;
        }

        public void Start()
        {
            // 未监听的情况，开始监听
            _isStartedReceive = true;

            //启动UDP
            Task.Run(async () =>
            {
                //启动客户端，失败重试
                await StartClientAsync();
            });
        }

        /// <summary>
        /// 启动UDP
        /// </summary>
        private async Task StartClientAsync()
        {
            try
            {
                _udpClient = new UdpClient();
                _logger.LogInformation("UDP发送器启动成功！");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("UDP发送器启动错误： ex:{0} ", ex);
                Thread.Sleep(100000);
                await StartClientAsync();
            }
        }

        public async Task SendDataAsync(SeekSocketModel seekSocket, IPEndPoint remotePoint = null)
        {
            if(string.IsNullOrEmpty(seekSocket.ClientIp))
            {
                _logger.LogError($"发送信息到UnitApp错误：{JsonConvert.SerializeObject(seekSocket, JsonUtil.JsonSettings)}");
                return;
            }
            await Task.Run(() =>
            {
                var ip = IPAddress.Parse(seekSocket.ClientIp);
                var clientEndPoint = new IPEndPoint(ip, seekSocket.ClientPort);

                List<string> ipAddresses = MachineUtil.GetIp();
                if (ipAddresses == null || ipAddresses.FirstOrDefault() == null)
                {
                    _logger.LogError("获取当前服务器IP失败！");
                    return;
                }
                ipAddresses = ipAddresses.Where(x => x.StartsWith("192.168.0.") || x.StartsWith("10.10.0.")).ToList();
                foreach (var item in ipAddresses)
                {
                    seekSocket.Header = _appsettings.SeekHeader;
                    seekSocket.DeviceKey = StaticInfo.ServerKey;
                    seekSocket.ServerIp = item;
                    seekSocket.ServerPort = StaticInfo.Port;

                    string sendData = JsonConvert.SerializeObject(seekSocket, JsonUtil.JsonSettings);

                    _logger.LogInformation("UDP发送数据：ip:{0} port:{1} message:{2} ", seekSocket.ClientIp, seekSocket.ClientPort, sendData);

                    var bytes = Encoding.UTF8.GetBytes(sendData);

                    _udpClient.SendAsync(bytes, bytes.Length, clientEndPoint);

                    if (remotePoint != null)
                    {
                        _udpClient.SendAsync(bytes, bytes.Length, remotePoint);
                    }
                }
            });
        }



        public void Dispose()
        {    // 终止监听
            _isStartedReceive = false;
            // 关闭客户端
            _udpClient.Close();

            _logger.LogInformation("UDP发送器已成功关闭！");
        }

    }
}