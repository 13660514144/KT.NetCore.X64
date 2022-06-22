using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers
{
    public class HeartbeatHelper
    {
        private Timer _timer;
        private HubHelper _hubHelper;
        private ILogger _logger;
        private readonly IEventAggregator _eventAggregator;
        private readonly AppSettings _appSettings;

        public HeartbeatHelper(HubHelper hubHelper,
            ILogger logger)
        {
            _hubHelper = hubHelper;
            _logger = logger;

            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
        }

        public async void StartAsync()
        {
            //var dueTime = (int)((_appSettings.ServerHeartbeatSecondTime / 3) * 1000);
            //var period = (int)(_appSettings.ServerHeartbeatSecondTime * 1000);
            //_timer = new Timer(HeartbeatTimerCallbackAsync, null, dueTime, period);
            /*2021-06-18*/
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
                    //_logger.LogInformation($"设备在线：connectionId:{_hubHelper.MasterHub.ConnectionId} ");
                    await _hubHelper.LinkAsync();

                    // 设备在线
                    _eventAggregator.GetEvent<IsOnlineEvent>().Publish(true);
                }
                else
                {
                    //如果不在线开启SignalR并发送连接数据
                    _logger.LogInformation($"设备不在线！");

                    // 设备不在线
                    _eventAggregator.GetEvent<IsOnlineEvent>().Publish(false);

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
