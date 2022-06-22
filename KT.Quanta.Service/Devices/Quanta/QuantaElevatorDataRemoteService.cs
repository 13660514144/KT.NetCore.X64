using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta
{
    /// <summary>
    /// 康塔电梯远程服务
    /// </summary>
    public class QuantaElevatorDataRemoteService : IElevatorDataRemoteService
    {
        private ILogger<QuantaElevatorDataRemoteService> _logger;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private IServiceScopeFactory _serviceScopeFactory;
        private IQuantaCardDeviceDistributeDataService _quantaCardDeviceDistribute;
        private IQuantaHandleElevatorDeviceDistributeDataService _quantaHandleElevatorDeviceDistribute;
        private IQuantaPassRightDistributeDataService _quantaPassRightDistribute;

        public QuantaElevatorDataRemoteService(ILogger<QuantaElevatorDataRemoteService> logger,
            IHubContext<QuantaDistributeHub> distributeHub,
            IServiceScopeFactory serviceScopeFactory,
            IQuantaCardDeviceDistributeDataService quantaCardDeviceDistribute,
            IQuantaHandleElevatorDeviceDistributeDataService quantaHandleElevatorDeviceDistribute,
            IQuantaPassRightDistributeDataService quantaPassRightDistribute)
        {
            _logger = logger;
            _distributeHub = distributeHub;
            _serviceScopeFactory = serviceScopeFactory;
            _quantaCardDeviceDistribute = quantaCardDeviceDistribute;
            _quantaHandleElevatorDeviceDistribute = quantaHandleElevatorDeviceDistribute;
            _quantaPassRightDistribute = quantaPassRightDistribute;
        }

        public async Task AddOrUpdateCardDeviceAsync(IRemoteDevice remoteDevice, CardDeviceModel model)
        {
            await _quantaCardDeviceDistribute.AddOrUpdateAsync(remoteDevice.RemoteDeviceInfo, model);
        }

        public async Task DeleteCardDeviceAsync(IRemoteDevice remoteDevice, string id, long time)
        {
            await _quantaCardDeviceDistribute.DeleteAsync(remoteDevice.RemoteDeviceInfo, id, time);
        }

        public async Task AddOrUpdateHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, UnitHandleElevatorDeviceModel model)
        {
            await _quantaHandleElevatorDeviceDistribute.AddOrEditAsync(remoteDevice.RemoteDeviceInfo, model);
        }

        public async Task DeleteHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, string id)
        {
            await _quantaHandleElevatorDeviceDistribute.DeleteAsync(remoteDevice.RemoteDeviceInfo, id);
        }

        public async Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model, FaceInfoModel face, PassRightModel oldModel)
        {
            await _quantaPassRightDistribute.AddOrUpdateAsync(remoteDevice.RemoteDeviceInfo, model, face);
        }

        public async Task DeletePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model)
        {
            await _quantaPassRightDistribute.DeleteAsync(remoteDevice.RemoteDeviceInfo, model);
        }

        public Task<int> GetOutputNumAsync(IRemoteDevice remoteDevice)
        {
            throw new System.NotImplementedException();
        }
    }
}
