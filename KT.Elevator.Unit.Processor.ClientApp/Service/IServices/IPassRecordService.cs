using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.IServices
{
    public interface IPassRecordService
    {
        Task AddAsync(UnitPassRecordEntity entity);
        Task Delete(string id);
        Task<UnitPassRecordEntity> GetLast();
        void PushAsync(UnitHandleElevatorModel handleElevator);
    }
}
