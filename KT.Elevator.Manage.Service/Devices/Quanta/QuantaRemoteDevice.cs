using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.Hubs;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Common.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Quanta
{
    public class QuantaRemoteDevice : IRemoteDevice
    {
        //远程设备数据
        public RemoteDeviceModel RemoteDeviceInfo { get; private set; }

        /// <summary>
        /// 登录客户端对象
        /// </summary>
        public object LoginUserClient { get; private set; }

        //包含的远程服务合集
        public RemoteServiceModel RemoteService { get; } = new RemoteServiceModel();

        private ILogger<QuantaRemoteDevice> _logger;
        private IHubContext<DistributeHub> _distributeHub;
        private IServiceProvider _serviceProvider;
        private IServiceScopeFactory _serviceScopeFactory;
        private SeekSendHelper _seekUdpHelper;
        private AppSettings _appSettings;

        public QuantaRemoteDevice(ILogger<QuantaRemoteDevice> logger,
            IHubContext<DistributeHub> distributeHub,
            IServiceProvider serviceProvider,
            IServiceScopeFactory serviceScopeFactory,
            SeekSendHelper seekUdpHelper,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _serviceProvider = serviceProvider;
            _serviceScopeFactory = serviceScopeFactory;
            _seekUdpHelper = seekUdpHelper;
            _appSettings = appSettings.Value;
        }

        public void Init(RemoteDeviceModel remoteDevice)
        {
            RemoteDeviceInfo = remoteDevice;

            RemoteService.ElevatorRemoteService = _serviceProvider.GetRequiredService<QuantaElevatorRemoteService>();
            RemoteService.DisplayRemoteService = _serviceProvider.GetRequiredService<QuantaDisplayRemoteService>();
        }

        public async Task<bool> CheckAndLinkAsync()
        {
            //已连接
            if (!string.IsNullOrEmpty(RemoteDeviceInfo.DeviceKey)
                && !string.IsNullOrEmpty(RemoteDeviceInfo.ConnectionId))
            {
                return true;
            }

            //连接
            var seekSocket = new SeekSocketModel();
            seekSocket.Header = _appSettings.SeekHeader;
            seekSocket.ClientIp = RemoteDeviceInfo.IpAddress;
            seekSocket.ClientPort = RemoteDeviceInfo.Port;

            await _seekUdpHelper.SendDataAsync(seekSocket);

            return false;
        }

        public void Heartbeat(string key)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public async Task AddOrUpdateCardDeviceAsync(CardDeviceModel model)
        {
            //电梯增加读卡器
            await RemoteService.ElevatorRemoteService?.AddOrUpdateCardDeviceAsync(this, model);

            //闸机增加读卡器
            await RemoteService.TurnstileRemoteService?.AddOrUpdateCardDeviceAsync(RemoteDeviceInfo, model);
        }

        public async Task DeleteCardDeviceAsync(string id, long time)
        {
            //电梯增加读卡器
            await RemoteService.ElevatorRemoteService?.DeleteCardDeviceAsync(this, id, time);

            //闸机增加读卡器
            await RemoteService.TurnstileRemoteService?.DeleteAsync(RemoteDeviceInfo, id, time);
        }

        public async Task AddOrUpdateHandleElevatorDeviceAsync(UnitHandleElevatorDeviceModel model)
        {
            await RemoteService.ElevatorRemoteService?.AddOrUpdateHandleElevatorDeviceAsync(this, model);
        }

        public async Task DeleteHandleElevatorDeviceAsync(string id)
        {
            await RemoteService.ElevatorRemoteService?.DeleteHandleElevatorDeviceAsync(this, id);
        }

        public async Task AddOrUpdatePassRightAsync(PassRightModel model, FaceInfoModel face)
        {
            await RemoteService.ElevatorRemoteService?.AddOrUpdatePassRightAsync(this, model, face);
        }

        public async Task DeletePassRightAsync(PassRightModel model)
        {
            await RemoteService.ElevatorRemoteService?.DeletePassRightAsync(this, model);
        }

        public Task<int> GetOutputNumAsync()
        {
            throw new NotImplementedException();
        }
    }
}