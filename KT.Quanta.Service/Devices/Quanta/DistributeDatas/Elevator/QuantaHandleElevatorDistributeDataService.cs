using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Dispatch.Entity.Models;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public class QuantaHandleElevatorDistributeDataService : IQuantaHandleElevatorDistributeDataService
    {
        private ILogger<QuantaHandleElevatorDistributeDataService> _logger;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private DistributeHelper _distributeHelper;

        public QuantaHandleElevatorDistributeDataService(ILogger<QuantaHandleElevatorDistributeDataService> logger,
            IHubContext<QuantaDistributeHub> distributeHub,
            DistributeHelper distributeHelper)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _distributeHelper = distributeHelper;
        }

        public async Task DirectCallAsync(RemoteDeviceModel remoteDevice, UnitDispatchSendHandleElevatorModel result)
        {
            //分发数据
            foreach (var item in remoteDevice.CommunicateDeviceInfos)
            {
                await _distributeHub.Clients.Client(item.ConnectionId).SendAsync("HandleElevatorDevice", result);
            }
        }

        public async Task MultiFloorCallAsync(RemoteDeviceModel remoteDevice, UnitDispatchSendHandleElevatorModel result)
        {
            //分发数据
            foreach (var item in remoteDevice.CommunicateDeviceInfos)
            {
                await _distributeHub.Clients.Client(item.ConnectionId).SendAsync("HandleElevatorDevices", result);
            }
        }
    }
}
