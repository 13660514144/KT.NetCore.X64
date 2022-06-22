using KT.Common.Core.Utils;
using KT.Turnstile.Unit.ClientApp.Device.IDevices;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Device.Devices
{
    public class DefaultUdpDevice : ISocketDevice
    {
        public TurnstileUnitCardDeviceEntity SocketDevice { get; private set; }

        // Upd客户端
        private UdpClient _udpClient;
        // 发送到的IP地址和端口号
        private IPEndPoint _sendPoint;
        // 监听线程，关闭或重启时等待结束
        private Task _receiveTask;
        // 数据接收是否启动
        private bool _isStartedReceive;

        private ILogger _logger;
        private AppSettings _appSettings;

        public DefaultUdpDevice(ILogger logger, AppSettings appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }

        public async Task InitAsync(TurnstileUnitCardDeviceEntity device)
        {
            await Task.Run(() =>
            {
                SocketDevice = device;

                // 发送到的IP地址和端口号
                var ip = IPAddress.Parse(device.RelayDeviceIp);
                _sendPoint = new IPEndPoint(ip, device.RelayDevicePort);

                try
                {
                    // 创建Upd客户端
                    _udpClient = new UdpClient();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("开启UDP设备失败：data:{0} ex:{1} ", JsonConvert.SerializeObject(device, JsonUtil.JsonPrintSettings), ex);
                }

                _logger.LogInformation("开启UDP设备成功：data:{1} ", JsonConvert.SerializeObject(device, JsonUtil.JsonPrintSettings));
                // 开始接收数据
                _isStartedReceive = true;
                _receiveTask = ReceiveMessage();
            });
        }

        public Task ReceiveExecAnsyc(object data)
        {
            return Task.Run(() =>
            {
                _logger.LogInformation($"处理默认UDP设备数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");
            });
        }

        public void Dispose()
        {
            // 终止监听
            _isStartedReceive = false;
            // 等待接收线程结束
            _receiveTask.Wait();
            // 关闭客户端
            _udpClient.Close();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendExceAnsyc(string message)
        {
            return Task.Run(() =>
            {
                _logger.LogInformation("UDP设备消息发送：ip:{0} port:{1} message:{3} ", _sendPoint.Address, _sendPoint.Port, message);
                try
                {
                    byte[] sendbytes = Encoding.ASCII.GetBytes(message);
                    _udpClient.Send(sendbytes, sendbytes.Length, _sendPoint);
                }
                catch (Exception ex)
                {
                    _logger.LogError("UDP设备消息发送失败：ip:{0} port:{1} ex:{3} ", _sendPoint.Address, _sendPoint.Port, ex);
                }
            });
        }

        /// <summary>
        /// 接收数据
        /// </summary> 
        private Task ReceiveMessage()
        {
            return Task.Run(() =>
            {
                IPEndPoint remotePoint;
                while (_isStartedReceive)
                {
                    try
                    {
                        //无数据读取不操作，否则中断时会报错
                        if (_udpClient.Available <= 0)
                        {
                            Thread.Sleep(100);
                            continue;
                        }
                        remotePoint = new IPEndPoint(IPAddress.Any, 0);
                        byte[] byteReceive = _udpClient.Receive(ref remotePoint);
                        string message = Encoding.ASCII.GetString(byteReceive, 0, byteReceive.Length);
                        //处理数据
                        ReceiveExecAnsyc(message);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("UDP设备接收数据出错：{0} ", ex);
                        break;
                    }
                }
                _logger.LogInformation("UDP设备已关闭！");
            });
        }
    }
}
