using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public class HandleElevatorDeviceApi : BackendApiBase, IHandleElevatorDeviceApi
    {
        public HandleElevatorDeviceApi(ILogger<HandleElevatorDeviceApi> logger) : base(logger)
        {
        }

        public async Task<List<HandleElevatorDeviceModel>> GetAllAsync()
        {
            var result = await base.GetAsync<List<HandleElevatorDeviceModel>>($"HandleElevatorDevice/all");
            return result;
        }

        public async Task<List<HandleElevatorDeviceModel>> GetByElevatorGroupIdAsync(string elevatorGroupId)
        { 
            var result = await base.GetAsync<List<HandleElevatorDeviceModel>>($"HandleElevatorDevice/allByElevatorGroupId?elevatorGroupId={elevatorGroupId}");
            return result;
        }
    }
}
