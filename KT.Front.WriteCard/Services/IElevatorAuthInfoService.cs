using KT.Proxy.BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Front.WriteCard.Services
{
    public interface IElevatorAuthInfoService
    {
        Task<ElevatorAuthInfoModel> GetElevatorAuthInfoAsync(string deviceCode);
    }
}
