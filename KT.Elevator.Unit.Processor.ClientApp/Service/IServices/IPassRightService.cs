using KT.Common.Data.Models;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.IServices
{
    public interface IPassRightService
    {
        Task<UnitPassRightEntity> AddOrUpdateAsync(UnitPassRightEntity entity);
        Task<List<UnitPassRightEntity>> AddOrUpdateAsync(List<UnitPassRightEntity> entities);
        Task Delete(string id, long editTime);
        Task<List<UnitPassRightEntity>> GetByPageAsync(string type, int page, int size);
        Task<List<UnitPassRightEntity>> GetBySignsAsync(List<string> signs, string accessType);
        Task<UnitPassRightEntity> GetBySignAsync(string sign, string accessType);
        Task<PageData<UnitPassRightEntity>> GetPageWithDetailAsync(int page, int size);
        Task Deletesync(UnitPassRightEntity item);
    }
}
