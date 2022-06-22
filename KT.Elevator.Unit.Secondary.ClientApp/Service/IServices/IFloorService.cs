using KT.Common.Data.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.IServices
{
    public interface IFloorService
    {
        Task AddOrUpdateAsync(UnitFloorEntity entity);
        Task AddOrUpdateAsync(List<UnitFloorEntity> entities);
        Task DeleteAsync(string id, long editTime);
        Task<List<UnitFloorEntity>> GetAllByElevatorGroupIdAsync(string elevatorGroupId);
        Task<PageData<UnitFloorEntity>> GetPageByElevatorGroupIdAsync(string elevatorGroupId, int page, int size);
        Task AddOrEditElevatorGroupFloorsAsync(UnitHandleElevatorDeviceModel model);
        Task DeleteElevatorGroupFloorsAsync(string elevatorGroupId);
    }
}
