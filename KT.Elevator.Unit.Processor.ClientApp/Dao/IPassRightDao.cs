using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos
{
    public interface IPassRightDao : IBaseDataDao<UnitPassRightEntity>
    {
        Task<UnitPassRightEntity> GetWithDetailsByIdAsync(string id);
        Task<UnitPassRightEntity> GetByIdAndSignAsync(string id, string sign, string accessType);
        Task<List<UnitPassRightEntity>> GetBySignsAsync(List<string> signs, string accessType);
        Task<UnitPassRightEntity> GetBySignAsync(string sign, string accessType);
        Task EditAsync(UnitPassRightEntity entity);
        Task AddAsync(UnitPassRightEntity entity);
        Task<PageData<UnitPassRightEntity>> GetPageWithDetailsAsync(int page, int size);
    }
}
