using KT.Common.Core.Utils;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Hikvision;
using KT.Elevator.Manage.Service.Devices.Quanta;
using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Common
{
    public class RemoteDeviceFactory : IRemoteDeviceFactory
    {
        private readonly ILogger<RemoteDeviceFactory> _logger;
        private IServiceProvider _serviceProvider;

        public RemoteDeviceFactory(ILogger<RemoteDeviceFactory> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public IRemoteDevice Creator(RemoteDeviceModel model)
        {
            IRemoteDevice remoteDevice = null;
            if (model.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value)
            {
                //康塔边缘处理器
                if (model.BrandModel == BrandModelEnum.QSCS_3600P.Value)
                {
                    remoteDevice = _serviceProvider.GetRequiredService<QuantaRemoteDevice>();
                }
                //海康梯控主机
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value)
                {
                    remoteDevice = _serviceProvider.GetRequiredService<HikvisionRemoteDevice>();
                }
                else
                {
                    return DefaultRemoteDevice();
                }
            }
            //海康面板机7寸
            else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value)
            {
                remoteDevice = _serviceProvider.GetRequiredService<HikvisionRemoteDevice>();
            }
            //海康面板机10寸
            else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value)
            {
                remoteDevice = _serviceProvider.GetRequiredService<HikvisionRemoteDevice>();
            }
            else
            {
                remoteDevice = DefaultRemoteDevice();
            }
            remoteDevice.Init(model);

            return remoteDevice;
        }

        private IRemoteDevice DefaultRemoteDevice()
        {
            _logger.LogWarning($"未找到设备类型，使用默认远程QuantaRemoteDevice设备！");
            var remoteDevice = _serviceProvider.GetRequiredService<QuantaRemoteDevice>();
            return remoteDevice;
        }
    }
}
