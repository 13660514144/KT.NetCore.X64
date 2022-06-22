using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Enums;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Processor.ClientApp.Events;
using KT.Elevator.Unit.Processor.ClientApp.Service.IServices;
using KT.Elevator.Unit.Processor.ClientApp.Service.Services;
using KT.Quanta.Common.Enums;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Helpers
{
    public class InitDataHelper
    {
        private IContainerProvider _containerProvider;
        private ConfigHelper _configHelper;
        private InputDeviceSettings _inputDeviceSettings;
        private AppSettings _appSettings;
        private IEventAggregator _eventAggregator;
        private ILogger _logger;

        public InitDataHelper(ConfigHelper configHelper,
            IContainerProvider containerProvider,
            InputDeviceSettings inputDeviceSettings,
            AppSettings appSettings,
            IEventAggregator eventAggregator,
            ILogger logger)
        {
            _configHelper = configHelper;
            _containerProvider = containerProvider;
            _inputDeviceSettings = inputDeviceSettings;
            _appSettings = appSettings;
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        /// <summary>
        /// 从服务端拉取数据更新到本地
        /// </summary>
        /// <param name="masterHub"></param>
        /// <param name="hasError">是否包含错误</param>
        /// <param name="isForce">是否强制更新</param>
        /// <returns></returns>
        public async Task InitAsync(HubConnection masterHub, bool isForce)
        {
            //更新硬件，从本地上传
            await PushInputDevicesAsync(masterHub);

            //更新错误数据 
            await GetErrorsAsync(masterHub);

            if (isForce || _configHelper.LocalConfig.LastSyncTime == 0)
            {
                var utcNow = DateTimeUtil.UtcNowMillis();

                if (_appSettings.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value)
                {
                    //更新硬件，从本地上传
                    await GetHandleElevatorDeviceAsync(masterHub);
                }
                if (_appSettings.DeviceType == DeviceTypeEnum.ELEVATOR_CLIENT.Value)
                {
                    //更新硬件，从本地上传
                    await GetHandleElevatorDeviceAsync(masterHub);
                }
                if (_appSettings.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value)
                {
                    //初始化读卡器
                    await GetCardDevicesAsync(masterHub);
                }
                //初始化权限
                await GetPassRightsAsync(masterHub);

                //更新本地配置
                _configHelper.LocalConfig.LastSyncTime = utcNow;
                var systemConfigSerivce = _containerProvider.Resolve<SystemConfigService>();
                await systemConfigSerivce.AddOrUpdateAsync(UnitSystemConfigEnum.LAST_SYNC_TIME, utcNow);
            }
        }

        /// <summary>
        /// 更新硬件，从本地上传
        /// </summary>
        /// <param name="masterHub"></param>
        /// <param name="appendMessageAction"></param>
        /// <returns></returns>
        private async Task PushInputDevicesAsync(HubConnection masterHub)
        {
            _logger.LogInformation($"上传输设备信息！");
            var datas = new List<UnitHandleElevatorInputDeviceEntity>();

            if (_inputDeviceSettings.CameraSettings.IsEnable)
            {
                var data = new UnitHandleElevatorInputDeviceEntity();
                data.Name = _inputDeviceSettings.CameraSettings.Name + $"({MachineUtil.GetIp(_appSettings.StartWithIp)}:{_appSettings.Port})";
                data.AccessType = AccessTypeEnum.FACE.Value;
                data.DeviceType = CardDeviceTypeEnum.FACE_CAMERA.Value;
                datas.Add(data);

                _logger.LogInformation($"上传输设备信息完成：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

                await masterHub.InvokeAsync("HandleElevatorInputDevices", datas);

                _logger.LogInformation($"上传输设备信息完成！");
            }
        }

        /// <summary>
        /// 更新派梯设备
        /// </summary>
        /// <param name="masterHub"></param>
        /// <param name="appendMessageAction"></param>
        /// <returns></returns>
        private async Task GetHandleElevatorDeviceAsync(HubConnection masterHub)
        {
            _logger.LogInformation($"派梯设备设备信息！");
            var model = await masterHub.InvokeAsync<UnitHandleElevatorDeviceModel>("GetHandleElevatorDevice", _appSettings.DeviceType);
            _logger.LogInformation($"派梯设备设备信息：{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

            //配置本地文件 
            var systemConfigService = _containerProvider.Resolve<ISystemConfigService>();
            _configHelper.LocalConfig = await systemConfigService.AddOrEditFromDeviceAsync(model, _configHelper.LocalConfig);

            // 新增电梯组
            var elevatorGroupService = _containerProvider.Resolve<IElevatorGroupService>();
            await elevatorGroupService.AddOrEditFromDeviceAsync(model);

            // 新增或修改派梯设备
            var handleElevatorDeviceService = _containerProvider.Resolve<IHandleElevatorDeviceService>();
            await handleElevatorDeviceService.AddOrEditAsync(model);

            _logger.LogInformation($"派梯设备设备信息完成！");
        }

        private async Task GetErrorsAsync(HubConnection masterHub)
        {
            try
            {
                int page = 1;
                int size = 20;
                while (true)
                {
                    _logger.LogInformation($"更新推送错误数据：page:{page} size:{size} ");
                    var entities = await masterHub.InvokeAsync<List<UnitErrorModel>>("GetUnitErrors", _appSettings.DeviceType, page, size);
                    var errorService = _containerProvider.Resolve<IErrorService>();
                    await errorService.AddOrUpdateAsync(entities);
                    if (entities.FirstOrDefault() != null)
                    {
                        page++;
                    }
                    else
                    {
                        break;
                    }
                }
                _logger.LogInformation($"更新推送错误数据完成！");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, $"更新推送错误数据失败！ ");
            }
        }

        private async Task GetCardDevicesAsync(HubConnection masterHub)
        {
            _logger.LogInformation($"更新读卡器");
            var entities = await masterHub.InvokeAsync<List<UnitCardDeviceEntity>>("GetCardDevices", _appSettings.DeviceType);
            _logger.LogInformation($"更新读卡器：{JsonConvert.SerializeObject(entities, JsonUtil.JsonPrintSettings)} ");

            var serivce = _containerProvider.Resolve<ICardDeviceService>();
            await serivce.AddOrUpdateAsync(entities);

            _logger.LogInformation($"更新读卡器完成！");
        }

        private async Task GetPassRightsAsync(HubConnection masterHub)
        {
            int page = 1;
            int size = 5;
            while (true)
            {
                _logger.LogInformation($"更新通行权限：pate:{page} size:{size} ");
                var entities = await masterHub.InvokeAsync<List<UnitPassRightEntity>>("GetPassRights", page, size);
                _logger.LogInformation($"更新通行权限：{JsonConvert.SerializeObject(entities, JsonUtil.JsonPrintSettings)} ");

                var serivce = _containerProvider.Resolve<IPassRightService>();
                entities = await serivce.AddOrUpdateAsync(entities);

                //发布权限更新事件，写入内存
                _eventAggregator.GetEvent<AddOrEditPassRightsEvent>().Publish(entities);

                //继续更新数据
                if (entities.Count() > 0)
                {
                    page++;
                }
                else
                {
                    break;
                }
            }
            _logger.LogInformation($"更新通行权限完成！");
        }
    }
}
