using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos
{
    public interface IFloorDao : IBaseDataDao<UnitFloorEntity>
    {
        Task<PageData<UnitFloorEntity>> GetPageByElevatorGroupIdAsync(string elevatorGroupId, int page, int size);
        Task<List<UnitFloorEntity>> GetByIdsAsync(List<string> ids); 
    }
}
