using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using KT.Common.Data.Models;
using KT.Quanta.Model.Elevator.Dtos;

namespace KT.Quanta.Service.Services
{
    /// <summary>
    /// 通行权限数据存储服务
    /// </summary>
    public interface IHandleElevatorDeviceAuxiliaryService
    {
        Task AddOrEditsAsync(List<HandleElevatorDeviceAuxiliaryModel> models);
        Task<List<HandleElevatorDeviceAuxiliaryModel>> GetAllAsync();
    }
}
