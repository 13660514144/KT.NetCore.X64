using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.IServices
{
    public interface IHandleElevatorDeviceService
    {
        Task<UnitSystemConfigModel> AddOrEditAsync(UnitHandleElevatorDeviceModel model, UnitSystemConfigModel configModel);
        Task DeleteAsync(string handleElevatorDeviceId);
    }
}
