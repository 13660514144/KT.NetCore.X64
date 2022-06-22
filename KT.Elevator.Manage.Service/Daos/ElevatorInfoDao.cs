using KT.Common.Data.Daos;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Daos
{
    public class ElevatorInfoDao : BaseDataDao<ElevatorInfoEntity>, IElevatorInfoDao
    {
        private ElevatorDbContext _context;
        public ElevatorInfoDao(ElevatorDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ElevatorInfoEntity> AddAsync(ElevatorInfoEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.ElevatorGroupId))
            {
                entity.ElevatorGroup = await _context.ElevatorGroups.FirstOrDefaultAsync(x => x.Id == entity.ElevatorGroupId);
            }
            await InsertAsync(entity);
            return entity;
        }

        public async Task<ElevatorInfoEntity> EidtAsync(ElevatorInfoEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.ElevatorGroupId))
            {
                entity.ElevatorGroup = await _context.ElevatorGroups.FirstOrDefaultAsync(x => x.Id == entity.ElevatorGroupId);
            }

            await AttachUpdateAsync(entity);

            return entity;

        } 
    }
}
