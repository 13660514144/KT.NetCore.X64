using KT.Common.Data.Daos;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface IElevatorInfoDao : IBaseDataDao<ElevatorInfoEntity>
    {
        Task<ElevatorInfoEntity> EidtAsync(ElevatorInfoEntity entity);
        Task<ElevatorInfoEntity> AddAsync(ElevatorInfoEntity entity); 
    }
}
