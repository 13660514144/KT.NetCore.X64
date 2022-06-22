using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace KT.Front.WriteCard.Services
{
    public class ElevatorAuthInfoService : IElevatorAuthInfoService
    {
        private readonly IElevatorAuthInfoApi _elevatorAuthInfoApi;

        public ElevatorAuthInfoService(IElevatorAuthInfoApi elevatorAuthInfoApi)
        {
            _elevatorAuthInfoApi = elevatorAuthInfoApi;
        }
        public async Task<ElevatorAuthInfoModel> GetElevatorAuthInfoAsync(string deviceCode)
        {
            var result = await _elevatorAuthInfoApi.GetElevatorAuthInfoByCardCode(deviceCode);
            return result;
        }
    }
}
