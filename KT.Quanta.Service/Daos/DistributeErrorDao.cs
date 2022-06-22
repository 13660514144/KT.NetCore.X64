using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class DistributeErrorDao : BaseDataDao<DistributeErrorEntity>, IDistributeErrorDao
    {
        private QuantaDbContext _context;

        public DistributeErrorDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DistributeErrorEntity> AddAsync(DistributeErrorEntity entity)
        {
            //await InsertAsync(entity);

            return entity;
        }

        public async Task DeleteAsync(List<DistributeErrorEntity> entities)
        {
            /*
            if (entities == null || entities.FirstOrDefault() == null)
            {
                return;
            }

            _context.DistributeErrors.RemoveRange(entities);

            await _context.SaveChangesAsync();
            */
        }

        public async Task<DistributeErrorEntity> EditAsync(DistributeErrorEntity entity)
        {
            //await AttachAsync(entity);

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
