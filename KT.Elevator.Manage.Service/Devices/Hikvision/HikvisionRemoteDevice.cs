using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Hikvision
{
    public class HikvisionRemoteDevice : IRemoteDevice
    {
        //是否启动事件上传
        private bool _isDeployEvent = false;

        public RemoteDeviceModel RemoteDeviceInfo { get; private set; }
        public object LoginUserClient => _hikvisionSdkService;
        public RemoteServiceModel RemoteService { get; } = new RemoteServiceModel();

        private ILogger<HikvisionRemoteDevice> _logger;
        private IServiceProvider _serviceProvider;
        private AppSettings _appSettings;
        private IHikvisionSdkService _hikvisionSdkService;

        public HikvisionRemoteDevice(ILogger<HikvisionRemoteDevice> logger,
            IServiceProvider serviceProvider,
            IOptions<AppSettings> appSettings,
            IHikvisionSdkService hikvisionSdkService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _appSettings = appSettings.Value;
            _hikvisionSdkService = hikvisionSdkService;
        }

        public void Init(RemoteDeviceModel remoteDevice)
        {
            RemoteDeviceInfo = remoteDevice;

            RemoteService.ElevatorRemoteService = _serviceProvider.GetRequiredService<HikvisionElevatorRemoteService>();

        }

        public async Task<bool> CheckAndLinkAsync()
        {
            //已连接
            if (!string.IsNullOrEmpty(RemoteDeviceInfo.ConnectionId)
                && Convert.ToInt32(RemoteDeviceInfo.ConnectionId) >= 0)
            {
                if (_isDeployEvent)
                {
                    //异步 启用事件上传
                    _hikvisionSdkService.DeployEventAsync();
                }

                return true;
            }

            //连接
            var userId = await _hikvisionSdkService.LoginAsync(this, _appSettings.HikvisionAccount, _appSettings.HikvisionPassword);

            RemoteDeviceInfo.ConnectionId = userId.ToString();
            if (userId < 0)
            {
                _logger.LogWarning($"设备登录失败：{JsonConvert.SerializeObject(RemoteDeviceInfo, JsonUtil.JsonSettings)} ");
                return false;
            }
            else
            {
                _logger.LogInformation($"设备登录成功：{JsonConvert.SerializeObject(RemoteDeviceInfo, JsonUtil.JsonSettings)} ");

                //异步 启用事件上传
                _hikvisionSdkService.DeployEventAsync();

                return true;
            }
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

        public async Task<int> GetOutputNumAsync()
        {
            return await RemoteService.ElevatorRemoteService.GetOutputNumAsync(this);
        }
    }
}