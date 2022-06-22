using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Enums;
using KT.Elevator.Unit.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.IServices
{
    public interface IElevatorGroupService
    {
        Task AddOrEditFromDeviceAsync(UnitHandleElevatorDeviceModel model);
    }
}
