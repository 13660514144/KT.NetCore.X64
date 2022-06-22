using KT.Common.Data.Daos;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Daos
{
    public class DistributeErrorDao : BaseDataDao<DistributeErrorEntity>, IDistributeErrorDao
    {
        private ElevatorDbContext _context;

        public DistributeErrorDao(ElevatorDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DistributeErrorEntity> AddAsync(DistributeErrorEntity entity)
        {
            await InsertAsync(entity);

            return entity;
        }

        public async Task DeleteAsync(List<DistributeErrorEntity> entities)
        {
            if (entities == null || entities.FirstOrDefault() == null)
            {
                return;
            }

            _context.DistributeErrors.RemoveRange(entities);

            await _context.SaveChangesAsync();
        }

        public async Task<DistributeErrorEntity> EditAsync(DistributeErrorEntity entity)
        {
            await AttachUpdateAsync(entity);

            return entity;
        }

        public Task<List<DistributeErrorEntity>> GetAllAsync()
        {
            var entities = _context.DistributeErrors
                .ToListAsync();
            return entities;
        }

        public Task<DistributeErrorEntity> GetByIdAsync(string id)
        {
            var entity = _context.DistributeErrors
           .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }
    }
}
