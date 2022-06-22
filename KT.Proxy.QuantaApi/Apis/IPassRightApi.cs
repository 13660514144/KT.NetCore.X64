using KT.Common.Data.Models;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public interface IPassRightApi
    { 
        Task<PageData<PassRightModel>> GetAllPageAsync(int page, int size, string personName, string cardNumber);
    }
}
