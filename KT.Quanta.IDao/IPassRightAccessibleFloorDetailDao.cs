using KT.Common.Data.Daos;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IPassRightAccessibleFloorDetailDao : IBaseDataDao<PassRightAccessibleFloorDetailEntity>
    {
        Task<PassRightAccessibleFloorDetailEntity> GetWithFloorAsync(string sign, string elevatorGroupId);
        Task<PassRightAccessibleFloorDetailEntity> GetAsync(string sign, string elevatorGroupId, string floorId);
    }
}
