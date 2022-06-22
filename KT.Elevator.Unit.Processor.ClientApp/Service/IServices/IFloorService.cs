using KT.Common.Data.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.IServices
{
    public interface IFloorService
    {
        Task AddOrUpdateAsync(UnitFloorEntity entity);
        Task AddOrUpdateAsync(List<UnitFloorEntity> entities);
        Task DeleteAsync(string id, long editTime);
        Task<List<UnitFloorEntity>> GetAllByElevatorGroupIdAsync(string elevatorGroupId);
        Task<PageData<UnitFloorEntity>> GetPageByElevatorGroupIdAsync(string elevatorGroupId, int page, int size);
        Task DeleteElevatorGroupFloorsAsync(string elevatorGroupId);
        Task<List<UnitFloorEntity>> GetAllByHandleElevatorDeviceIdAsync(string handleElevatorDeviceId);
    }
}
