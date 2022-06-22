using KT.Common.Data.Models;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public class PassRightApi : BackendApiBase, IPassRightApi
    {
        public PassRightApi(ILogger<PassRightApi> logger) : base(logger)
        {
        }

        public async Task<PageData<PassRightModel>> GetAllPageAsync(int page, int size, string name, string sign)
        {
            var result = await base.GetAsync<PageData<PassRightModel>>($"PassRight/allPage?page={page}&size={size}&name={name}&sign={sign}");
            return result;
        }
         
    }
}
