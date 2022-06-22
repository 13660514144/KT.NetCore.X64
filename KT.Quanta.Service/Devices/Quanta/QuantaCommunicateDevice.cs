using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Common.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using KT.Quanta.Service.Dtos;
using KT.Common.Core.Utils;

namespace KT.Quanta.Service.Devices.Quanta
{
    public class QuantaCommunicateDevice : ICommunicateDevice
    {
        //远程设备数据
        public CommunicateDeviceInfoModel CommunicateDeviceInfo { get; private set; }

        private ILogger<QuantaCommunicateDevice> _logger;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private IServiceProvider _serviceProvider;
        private IServiceScopeFactory _serviceScopeFactory;
        private AppSettings _appSettings;

        public QuantaCommunicateDevice(ILogger<QuantaCommunicateDevice> logger,
            IHubContext<QuantaDistributeHub> distributeHub,
            IServiceProvider serviceProvider,
            IServiceScopeFactory serviceScopeFactory,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _serviceProvider = serviceProvider;
            _serviceScopeFactory = serviceScopeFactory;
            _appSettings = appSettings.Value;
        }

        public Task InitAsync(CommunicateDeviceInfoModel communicateDevice, RemoteDeviceModel remoteDevice)
        {
            CommunicateDeviceInfo = communicateDevice;

            //RemoteService.ElevatorRemoteService = _serviceProvider.GetRequiredService<QuantaElevatorRemoteService>();
            //RemoteService.DisplayRemoteService = _serviceProvider.GetRequiredService<QuantaDisplayRemoteService>(); 

            return Task.CompletedTask;
        }

        /// <summary>
        /// 登录客户端对象
        /// </summary>
        public T GetLoginUserClient<T>()
        {
            return (T)_distributeHub;
        }

        public Task<bool> CheckAndLinkAsync()
        {
            //已连接 
            if (!string.IsNullOrEmpty(CommunicateDeviceInfo.ConnectionId))
            {
                CommunicateDeviceInfo.IsOnline = true;

                //更新最后在线时间
                CommunicateDeviceInfo.LastOnlineTime = DateTimeUtil.UtcNowMillis();
                //统计连接失败次数
                CommunicateDeviceInfo.ReloginTimes = 0;

                return Task.FromResult(true);
            }
            _logger.LogWarning($"{CommunicateDeviceInfo.IpAddress}:{CommunicateDeviceInfo.Port} Quanta设备离线！ ");
            return Task.FromResult(false);
        }

        public async Task CloseAsync()
        {
            await _distributeHub.Clients.Client(CommunicateDeviceInfo.ConnectionId).SendAsync("Close");
            CommunicateDeviceInfo.ConnectionId = string.Empty;
            CommunicateDeviceInfo.IsOnline = false;
        }

        public Task HeartbeatAsync()
        {
            return Task.CompletedTask;
        }
    }
}