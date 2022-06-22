using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.IServices
{
    public interface IHandleElevatorDeviceService
    {
        Task AddOrEditAsync(UnitHandleElevatorDeviceModel model );
        Task DeleteAsync(string handleElevatorDeviceId);
    }
}
