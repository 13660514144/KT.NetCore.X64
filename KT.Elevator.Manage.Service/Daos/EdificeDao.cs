using KT.Common.Data.Daos;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Common;

namespace KT.Elevator.Manage.Service.Daos
{
    public class EdificeDao : BaseDataDao<EdificeEntity>, IEdificeDao
    {
        private ElevatorDbContext _context;
        public EdificeDao(ElevatorDbContext context) : base(context)
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
