using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class EdificeDao : BaseDataDao<EdificeEntity>, IEdificeDao
    {
        private QuantaDbContext _context;
        public EdificeDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EdificeEntity> AddWidthFloorAsync(EdificeEntity entity)
        {
            await _context.Edifices.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<EdificeEntity> EditWidthFloorAsync(EdificeEntity entity)
        {
            _context.Edifices.Update(entity);

            //保存更改
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<EdificeEntity>> GetAllWithFloorAsync()
        {
            var entities = await _context.Edifices.Include(x => x.Floors).ToListAsync();
            return entities;
        }

        public async Task<EdificeEntity> GetWithFloorByIdAsync(string id)
        {
            var entity = await _context.Edifices
                .Include(x => x.Floors)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<List<EdificeEntity>> GeWithFloorByIdsAsync(List<string> ids)
        {
            var entities = await _context.Edifices
                .Include(x => x.Floors)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            return entities;
        }

        public async Task RemoveWithFloorByIdAsync(string id)
        {
            //删除大厦
            var entity = await _context.Edifices.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return;
            }
            _context.Edifices.Remove(entity);

            //保存更改
            await _context.SaveChangesAsync();
        }
    }
}
