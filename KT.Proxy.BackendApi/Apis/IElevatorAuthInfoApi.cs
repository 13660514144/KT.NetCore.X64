using KT.Proxy.BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public interface IElevatorAuthInfoApi
    {
        Task<ElevatorAuthInfoModel> GetElevatorAuthInfoByCardCode(string cardCode);
    }
}
