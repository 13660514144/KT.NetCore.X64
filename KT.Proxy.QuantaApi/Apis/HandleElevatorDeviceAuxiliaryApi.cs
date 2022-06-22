using KT.Common.Data.Models;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public class HandleElevatorDeviceAuxiliaryApi : BackendApiBase, IHandleElevatorDeviceAuxiliaryApi
    {
        public HandleElevatorDeviceAuxiliaryApi(ILogger<HandleElevatorDeviceAuxiliaryApi> logger)
            : base(logger)
        {
        }

        public async Task AddOrEditsAsync(List<HandleElevatorDeviceAuxiliaryModel> model)
        {
            await base.PostAsync($"HandleElevatorDeviceAuxiliary/AddOrEdits", model);
        }

        public async Task<List<HandleElevatorDeviceAuxiliaryModel>> GetAllAsync()
        {
            return await base.GetAsync<List<HandleElevatorDeviceAuxiliaryModel>>($"HandleElevatorDeviceAuxiliary/all");
        }
    }
}
