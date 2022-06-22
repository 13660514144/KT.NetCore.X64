using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public class ElevatorAuthInfoApi : BackendApiBase, IElevatorAuthInfoApi
    {
        public ElevatorAuthInfoApi(ILogger<ElevatorAuthInfoApi> logger) : base(logger)
        {
        }

        public async Task<ElevatorAuthInfoModel> GetElevatorAuthInfoByCardCode(string cardNo)
        {
            var postdata = new { cardNo };
            var result = await PostAsync<ElevatorAuthInfoModel>("openapi/elevator/authinfo", postdata, isHasBaseHeader: false);
            return result;
            throw new NotImplementedException();
        }
    }
}
