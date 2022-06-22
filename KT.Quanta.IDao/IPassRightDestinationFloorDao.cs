using KT.Common.Data.Daos;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IPassRightDestinationFloorDao : IBaseDataDao<PassRightDestinationFloorEntity>
    { 
        Task<List<PassRightDestinationFloorEntity>> GetWithDetailBySignAsync(string sign);
        Task<PassRightDestinationFloorEntity> GetWithFloorAsync(string sign, string elevatorGroupId);
        Task<PassRightDestinationFloorEntity> GetBySignAndElevatorGroupIdAsync(string sign, string elevatorGroupId);
    }
}
