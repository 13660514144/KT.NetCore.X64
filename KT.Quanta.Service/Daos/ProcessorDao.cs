using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class ProcessorDao : BaseDataDao<ProcessorEntity>, IProcessorDao
    {
        private QuantaDbContext _context;
        public ProcessorDao(QuantaDbContext context) : base(context)
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
                .ThenInclude(x => x.HandleElevatorDevice)
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

        public async Task<ProcessorEntity> GetWithProcessorFloorsByIdAsync(string processorId)
        {
            var result = await _context.Processors
                  .Include(x => x.ProcessorFloors)
                  .ThenInclude(x => x.Floor)
                  .FirstOrDefaultAsync(x => x.Id == processorId);

            return result;
        }

        public async Task<List<ProcessorEntity>> GetByDeviceTypeAsync(string deviceType)
        {
            var results = await _context.Processors
                .Include(x => x.Floor)
                .Where(x => x.DeviceType == deviceType)
                .ToListAsync();

            return results;
        }


        public async Task<ProcessorEntity> GetWidthCardDevicesByIdAsync(string id)
        {
            var result = await _context.Processors
                  .Include(x => x.CardDevices)
                  .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

    }
}
