using KT.Common.Data.Daos;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IHandleElevatorDeviceAuxiliaryDao : IBaseDataDao<HandleElevatorDeviceAuxiliaryEntity>
    {
        Task<HandleElevatorDeviceAuxiliaryEntity> GetByHandleElevatorDeviceIdAsync(string handleElevatorDeviceId);
    }
}
