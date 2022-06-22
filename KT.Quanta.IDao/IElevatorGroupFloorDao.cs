using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IElevatorGroupFloorDao : IBaseDataDao<ElevatorGroupFloorEntity>
    {
        Task<ElevatorGroupFloorEntity> GetWithFloorByElevatorGroupIdAndFloorIdAsync(string elevatorGroupId, string floorId);
        Task<ElevatorGroupFloorEntity> GetByElevatorGroupIdAndFloorIdAsync(string elevatorGroupId, string floorId);
        Task<ElevatorGroupFloorEntity> GetByElevatorGroupIdAndRealFloorIdAsync(string elevatorGroupId, string realFloorId);
        Task<ElevatorGroupFloorEntity> GetWithFloorByElevatorGroupIdAndRealFloorIdAsync(string elevatorGroupId, string realFloorId);
        Task<List<ElevatorGroupFloorEntity>> GetByElevatorGroupIdAndFloorIdsAsync(string elevatorGroupId, List<string> destinationFloorIds);
        Task<List<ElevatorGroupFloorEntity>> GetWithFloorsByElevatorGroupIdAndFloorIdsAsync(string elevatorGroupId, List<string> destinationFloorIds);
        Task<List<ElevatorGroupFloorEntity>> GetWithEdificeFloorsByElevatorGroupIdAndFloorIdsAsync(string elevatorGroupId, List<string> destinationFloorIds);
    }
}
