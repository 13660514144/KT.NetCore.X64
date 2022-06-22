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
    public class ProcessorDao : BaseDataDao<ProcessorEntity>, IProcessorDao
    {
        private ElevatorDbContext _context;
        public ProcessorDao(ElevatorDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProcessorEntity> AddAsync(ProcessorEntity entity)
        {
            if (entity.Floor != null)
            {
                entity.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == entity.Floor.Id);
            }
            await _context.Processors.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<ProcessorEntity> EditAsync(ProcessorEntity entity)
        {
            if (entity.Floor != null)
            {
                entity.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == entity.Floor.Id);
            }

            _context.Processors.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<ProcessorEntity>> GetAllAsync()
        {
            var results = await _context.Processors
                .Include(x => x.Floor)
                .ToListAsync();
            return results;
        }

        public async Task<List<ProcessorEntity>> GetWidthCardDevicesAsync()
        {
            var results = await _context.Processors
                .Include(x => x.CardDevices)
                .ThenInclude(x => x.SerialConfig)
                .Include(x => x.CardDevices)
                .ThenInclude(x => x.HandElevatorDevice)
                .Include(x => x.Floor)
                .ToListAsync();
            return results;
        }

        public async Task<ProcessorEntity> GetByIdAsync(string id)
        {
            var result = await _context.Processors
                  .Include(x => x.Floor)
                  .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<ProcessorEntity> GetWithFloorsByIdAsync(string id)
        {
            var result = await _context.Processors
                 .Include(x => x.ProcessorFloors)
                 .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
