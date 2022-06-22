using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IElevatorInfoDao : IBaseDataDao<ElevatorInfoEntity>
    {
        Task<ElevatorInfoEntity> EidtAsync(ElevatorInfoEntity entity);
        Task<ElevatorInfoEntity> AddAsync(ElevatorInfoEntity entity);
        Task<ElevatorInfoEntity> GetByElevatorGroupIdAndRealIdAsync(string elevatorGroupId, string realId);
    }
}
