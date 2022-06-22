using KT.Elevator.Manage.Service.Devices.DistributeDatas;
using KT.Elevator.Manage.Service.Models;
using System;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Helpers
{
    public class QuantaRemoteDeviceService : IRemoteDeviceService
    {
        private IQuantaCardDeviceDistributeDataService _quantaCardDeviceDistributeDataService;
        public QuantaRemoteDeviceService(IQuantaCardDeviceDistributeDataService quantaCardDeviceDistributeDataService)
        {
            _quantaCardDeviceDistributeDataService = quantaCardDeviceDistributeDataService;
        }

        public async Task AddOrUpdateCardDeviceAsync(RemoteDeviceModel remoteDevice, CardDeviceModel model)
        {
            await _quantaCardDeviceDistributeDataService.AddOrUpdateAsync(remoteDevice, model);
        }

        public async Task DeleteCardDeviceAsync(RemoteDeviceModel remoteDevice, string id, long time)
        {
            await _quantaCardDeviceDistributeDataService.DeleteAsync(remoteDevice, id, time);
        }
    }
}
