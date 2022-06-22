using KT.Common.Data.Daos;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IPassRightAccessibleFloorDao : IBaseDataDao<PassRightAccessibleFloorEntity>
    { 
        Task<List<PassRightAccessibleFloorEntity>> GetWithDetailBySignAsync(string sign);
        Task<PassRightAccessibleFloorEntity> GetWithDetailBySignAndElevatorGroupIdAsync(string sign, string elevatorGroupId);
        Task<PassRightAccessibleFloorEntity> GetBySignAndElevatorGroupIdAsync(string sign, string elevatorGroupId);
    }
}
