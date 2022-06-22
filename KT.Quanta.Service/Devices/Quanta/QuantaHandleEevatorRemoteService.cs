using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta
{
    public class QuantaHandleEevatorRemoteService : IHandleElevatorRemoteService
    {
        private ILogger<QuantaHandleEevatorRemoteService> _logger;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private IServiceScopeFactory _serviceScopeFactory;
        private IQuantaCardDeviceDistributeDataService _quantaCardDeviceDistribute;
        private IQuantaHandleElevatorDistributeDataService _quantaHandleEevatorDistribute;

        public QuantaHandleEevatorRemoteService(ILogger<QuantaHandleEevatorRemoteService> logger,
            IHubContext<QuantaDistributeHub> distributeHub,
            IServiceScopeFactory serviceScopeFactory,
            IQuantaCardDeviceDistributeDataService quantaCardDeviceDistribute,
            IQuantaHandleElevatorDistributeDataService quantaHandleEevatorDistribute)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _serviceScopeFactory = serviceScopeFactory;
            _quantaCardDeviceDistribute = quantaCardDeviceDistribute;
            _quantaHandleEevatorDistribute = quantaHandleEevatorDistribute;
        }

        public Task DirectCallAsync(IRemoteDevice remoteDevice, DistributeHandleElevatorModel distributeHandle)
        {
            throw new NotImplementedException();
        }

        public Task MultiFloorCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeMultiFloorHandleElevatorModel distributeHandle)
        {
            throw new NotImplementedException();
        }

        public Task RightCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeHandleElevatorModel distributeHandle)
        {
            throw new NotImplementedException();
        }
    }
}
