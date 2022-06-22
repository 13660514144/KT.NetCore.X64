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
    public class FloorDao : BaseDataDao<FloorEntity>, IFloorDao
    {
        private ElevatorDbContext _context;
        public FloorDao(ElevatorDbContext context) : base(context)
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
    }
}
