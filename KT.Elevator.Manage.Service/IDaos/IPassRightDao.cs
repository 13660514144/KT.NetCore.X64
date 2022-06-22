using KT.Common.Data.Daos;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface IPassRightDao : IBaseDataDao<PassRightEntity>
    {
        Task<List<PassRightEntity>> GetAllAsync();
        Task<PassRightEntity> GetByIdAsync(string id);
        Task<PassRightEntity> AddAsync(PassRightEntity entity);
        Task<PassRightEntity> EditAsync(PassRightEntity entity);
        Task RemoveAsync(PassRightEntity entity);
        Task<List<PassRightEntity>> DeleteBySignAsync(string sign);
        Task<List<PassRightEntity>> GetPageWithFloorAsync(int page, int size);
        Task<PassRightEntity> GetBySignAndAccessTypeAsync(string sign, string accessType);
        Task<PassRightEntity> GetWidthFloorsBySignAsync(string sign);
        Task<List<PassRightEntity>> DeleteReturnPersonRightsBySignAsync(string sign);
        Task<PassRightEntity> GetWithPersonRrightsByIdAsync(string id);
    }
}
