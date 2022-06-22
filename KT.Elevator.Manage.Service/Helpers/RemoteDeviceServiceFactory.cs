using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Hubs;
using KT.Elevator.Manage.Service.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Helpers
{
    public class RemoteDeviceServiceFactory : IRemoteDeviceServiceFactory
    {
        private ILogger<RemoteDeviceServiceFactory> _logger;
        private IHubContext<DistributeHub> _distributeHub;
        private IServiceProvider _serviceProvider;
        private RemoteDeviceList _remoteDeviceList;
        public RemoteDeviceServiceFactory(ILogger<RemoteDeviceServiceFactory> logger,
            IHubContext<DistributeHub> distributeHub,
            RemoteDeviceList remoteDeviceList,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _serviceProvider = serviceProvider;
            _remoteDeviceList = remoteDeviceList;
        }

 
        public IRemoteDeviceService CreateRemoteDeviceService(RemoteDeviceModel remoteDevice)
        {
            if (remoteDevice.ProductType == RemoteDeviceTypeEnum.ELEVATOR_PROCESSOR.Value)
            {
                if (remoteDevice.DeviceType == EquipmentTypeEnum.HIKVISION_DS_K1T672MW.Value
                    || remoteDevice.DeviceType == EquipmentTypeEnum.HIKVISION_DS_K2210.Value)
                {
                    return _serviceProvider.GetRequiredService<HikvisionRemoteDeviceService>();
                }
            }
            else
            {
                return _serviceProvider.GetRequiredService<QuantaRemoteDeviceService>();
            }

            throw new NotImplementedException();
        }

    }
}
