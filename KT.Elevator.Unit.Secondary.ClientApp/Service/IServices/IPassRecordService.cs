using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.IServices
{
    public interface IPassRecordService
    {
        Task AddAsync(UnitPassRecordEntity entity);
        Task Delete(string id);
        Task<UnitPassRecordEntity> GetLast();
        void PushAsync(UnitHandleElevatorModel handleElevator);
        Task<UnitPassRecordEntity> GetBySignAndAccessTypeAsync(string sign, string accessType);
    }
}
