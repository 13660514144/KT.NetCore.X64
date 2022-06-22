using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class ElevatorServerDao : BaseDataDao<ElevatorServerEntity>, IElevatorServerDao
    {
        private QuantaDbContext _context;
        public ElevatorServerDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ElevatorServerEntity> AddAsync(ElevatorServerEntity entity)
        {
            if (entity.ElevatorGroup != null)
            {
                entity.ElevatorGroup = await _context.ElevatorGroups.FirstOrDefaultAsync(x => x.Id == entity.ElevatorGroup.Id);
            }

            await _context.ElevatorServers.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<ElevatorServerEntity> EditAsync(ElevatorServerEntity entity)
        {
            if (entity.ElevatorGroup != null)
            {
                entity.ElevatorGroup = await _context.ElevatorGroups.FirstOrDefaultAsync(x => x.Id == entity.ElevatorGroup.Id);
            }
            _context.ElevatorServers.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<ElevatorServerEntity>> GetAllAsync()
        {
            var entities = await _context.ElevatorServers
                .Include(x => x.ElevatorGroup)
                .ToListAsync();
            return entities;
        }

        public async Task<ElevatorServerEntity> GetByIdAsync(string id)
        {
            var entity = await _context.ElevatorServers
                .Include(x => x.ElevatorGroup)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
    }
}
