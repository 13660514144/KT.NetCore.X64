using KT.Common.Data.Daos;
using KT.Elevator.Unit.Entity.Entities;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos
{
    public interface IElevatorGroupDao : IBaseDataDao<UnitElevatorGroupEntity>
    {
        Task<UnitElevatorGroupEntity> GetWithRelevanceFloorsByIdAsync(string elevatorGroupId);
    }
}
