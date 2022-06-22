using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Hikvision.Models;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hikvision
{
    public class HikvisionCommunicateDevice : ICommunicateDevice
    {
        //是否启动事件上传
        private bool _isDeployEvent = false;

        public CommunicateDeviceInfoModel CommunicateDeviceInfo { get; private set; }

        private ILogger<HikvisionCommunicateDevice> _logger;
        private IServiceProvider _serviceProvider;
        private AppSettings _appSettings;
        private IHikvisionSdkService _hikvisionSdkService;

        public HikvisionCommunicateDevice(ILogger<HikvisionCommunicateDevice> logger,
            IServiceProvider serviceProvider,
            IOptions<AppSettings> appSettings,
            IHikvisionSdkService hikvisionSdkService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _appSettings = appSettings.Value;
            _hikvisionSdkService = hikvisionSdkService;
        }

        public T GetLoginUserClient<T>()
        {
            return (T)_hikvisionSdkService;
        }

        public async Task InitAsync(CommunicateDeviceInfoModel communicateDevice, RemoteDeviceModel remoteDevice)
        {
            CommunicateDeviceInfo = communicateDevice;

            //事件上传配置
            var typeParameter = _appSettings.HikvisionTypeParameters?.FirstOrDefault(x => x.BrandModel == remoteDevice.BrandModel);
            if (typeParameter != null)
            {
                _logger.LogWarning($"{remoteDevice.BrandModel} 海康事件上传方式配置：{JsonConvert.SerializeObject(typeParameter, JsonUtil.JsonPrintSettings)} ");
            }
            else
            {
                typeParameter = new HikvisionTypeParameterModel();
                typeParameter.BrandModel = remoteDevice.BrandModel;
                _logger.LogWarning($"{remoteDevice.BrandModel} 海康事件上传方式未配置，使用默认值：{JsonConvert.SerializeObject(typeParameter, JsonUtil.JsonPrintSettings)} ");
            }

            await _hikvisionSdkService.InitAsync(communicateDevice, typeParameter);
        }

        public async Task<bool> CheckAndLinkAsync()
        {
            //已连接
            if (CommunicateDeviceInfo.IsOnline)
            {
                if (!_isDeployEvent)
                {
                    //异步 启用事件上传
                    _isDeployEvent = await _hikvisionSdkService.DeployEventAsync();
                    //统计连接失败次数
                    if (_isDeployEvent)
                    {
                        //成功清零
                        CommunicateDeviceInfo.ReloginTimes = 0;
                    }
                    else
                    {
                        //失败累加1
                        CommunicateDeviceInfo.ReloginTimes++;
                    }
                }

                //更新最后在线时间
                CommunicateDeviceInfo.LastOnlineTime = DateTimeUtil.UtcNowMillis();

                return true;
            }

            //连接
            var userId = await _hikvisionSdkService.LoginAsync(_appSettings.HikvisionAccount, _appSettings.HikvisionPassword);

            CommunicateDeviceInfo.ConnectionId = userId.ToString();
            if (userId < 0)
            {
                //统计连接失败次数
                CommunicateDeviceInfo.ReloginTimes++;

                _logger.LogWarning($"设备登录失败：{JsonConvert.SerializeObject(CommunicateDeviceInfo, JsonUtil.JsonPrintSettings)} ");
                return false;
            }
            else
            {
                CommunicateDeviceInfo.IsOnline = true;
                _logger.LogInformation($"设备登录成功：{JsonConvert.SerializeObject(CommunicateDeviceInfo, JsonUtil.JsonPrintSettings)} ");

                //更新最后在线时间
                CommunicateDeviceInfo.LastOnlineTime = DateTimeUtil.UtcNowMillis();
                //统计连接失败次数
                CommunicateDeviceInfo.ReloginTimes = 0;

                return true;
            }
        }

        public Task HeartbeatAsync()
        {
            return Task.CompletedTask;
        }

        public async Task CloseAsync()
        {
            await _hikvisionSdkService.LogoutAsync();

            CommunicateDeviceInfo.IsOnline = false;
            _isDeployEvent = false;
        }
    }
}
