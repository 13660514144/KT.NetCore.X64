using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Service.Helpers
{
    public class HeartbeatHelper
    {
        private Timer _timer;
        private HubHelper _hubHelper;
        private ILogger _logger;
        public HeartbeatHelper(HubHelper hubHelper,
            ILogger logger)
        {
            _hubHelper = hubHelper;
            _logger = logger;
        }

        public async void StartAsync()
        {
            _timer = new Timer(HeartbeatTimerCallbackAsync, null, 10 * 1000, 30 * 1000);

            await Task.CompletedTask;
        }

        private async void HeartbeatTimerCallbackAsync(object state)
        {
            if (_hubHelper.MasterHub == null)
            {
                _logger.LogInformation($"Signal客户未开启！");
                return;
            }
            try
            {
                if (!string.IsNullOrEmpty(_hubHelper.MasterHub.ConnectionId))
                {
                    //如果在线则发送连接数据
                    _logger.LogInformation($"设备在线：connectionId:{_hubHelper.MasterHub.ConnectionId} ");
                    await _hubHelper.LinkAsync();
                }
                else
                {
                    //如果不在线开启SignalR并发送连接数据
                    _logger.LogInformation($"设备不在线！");

                    if (_hubHelper.MasterHub.State == HubConnectionState.Disconnected)
                    {
                        _logger.LogInformation($"设备不在线，重新连接开始！");
                        await _hubHelper.MasterHub.StartAsync();
                        _logger.LogInformation($"设备不在线，重新连接结束！");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"SignalR心跳失败：{ex} ");
            }
        }
    }
}
