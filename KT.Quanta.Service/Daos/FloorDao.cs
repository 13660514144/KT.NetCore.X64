using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class FloorDao : BaseDataDao<FloorEntity>, IFloorDao
    {
        private QuantaDbContext _context;
        public FloorDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<FloorEntity>> GetAllAsync()
        {
            var entities = await _context.Floors.Include(x => x.Edifice).ToListAsync();
            return entities;
        }

        public async Task<List<FloorEntity>> GetByEdificeIdAsync(string id)
        {
            var entities = await _context.Floors.Include(x => x.Edifice).Where(x => x.Edifice.Id == id).ToListAsync();
            return entities;
        }

        public async Task<FloorEntity> GetByIdAsync(string id)
        {
            var entity = await _context.Floors.Include(x => x.Edifice).FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
        public async Task<List<FloorEntity>> GetWithEdificeByIdsAsync(List<string> ids)
        {
            var entities = await _context.Floors
                .Include(x => x.Edifice)
                .Where(x => ids.Contains( x.Id))
                .ToListAsync();

            return entities;
        }
        public async Task<List<FloorEntity>> GetByIdsAsync(List<string> ids)
        {
            var entities = await _context.Floors
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            return entities;
        }
    }
}
