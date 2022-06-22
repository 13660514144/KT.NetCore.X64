using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class HandleElevatorDeviceDao : BaseDataDao<HandleElevatorDeviceEntity>, IHandleElevatorDeviceDao
    {
        private QuantaDbContext _context;
        public HandleElevatorDeviceDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<HandleElevatorDeviceEntity> AddAsync(HandleElevatorDeviceEntity entity)
        {
            if (entity.Floor != null)
            {
                entity.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == entity.Floor.Id);
            }
            if (entity.ElevatorGroup != null)
            {
                entity.ElevatorGroup = await _context.ElevatorGroups.FirstOrDefaultAsync(x => x.Id == entity.ElevatorGroup.Id);
            }
            await InsertAsync(entity);

            return entity;
        }

        public async Task<HandleElevatorDeviceEntity> EditAsync(HandleElevatorDeviceEntity entity)
        {
            if (entity.Floor != null)
            {
                entity.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == entity.Floor.Id);
            }
            if (entity.ElevatorGroup != null)
            {
                entity.ElevatorGroup = await _context.ElevatorGroups.FirstOrDefaultAsync(x => x.Id == entity.ElevatorGroup.Id);
            }
            await AttachAsync(entity);

            return entity;
        }

        public async Task<List<HandleElevatorDeviceEntity>> GetAllAsync()
        {
            var entities = await _context.HandElevatorDevices
                .Include(x => x.Floor)
                .Include(x => x.ElevatorGroup)
                .ToListAsync();
            return entities;
        }

        public async Task<HandleElevatorDeviceEntity> GetByIdAsync(string id)
        {
            var entity = await _context.HandElevatorDevices
                .Include(x => x.Floor)
                .Include(x => x.ElevatorGroup)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
        public async Task<HandleElevatorDeviceEntity> GetWithFloorsByIdAsync(string id)
        {
            var entity = await _context.HandElevatorDevices
                .Include(x => x.Floor.Edifice)
                .Include(x => x.ElevatorGroup)
                .ThenInclude(x => x.ElevatorGroupFloors)
                .ThenInclude(x => x.Floor.Edifice)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
        public async Task<HandleElevatorDeviceEntity> GetWithFloorAndRelevanceFloorsByIdAsync(string id)
        {
            var entity = await _context.HandElevatorDevices
                .Include(x => x.Floor)
                .Include(x => x.ElevatorGroup)
                .ThenInclude(x => x.ElevatorGroupFloors)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
        public async Task<HandleElevatorDeviceEntity> GetWithFloorByIdAsync(string id)
        {
            var entity = await _context.HandElevatorDevices
                .Include(x => x.Floor)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
        public async Task<HandleElevatorDeviceEntity> GetWithElevatorServerByIdAsync(string id)
        {
            var entity = await _context.HandElevatorDevices
                .Include(x => x.Floor)
                .Include(x => x.ElevatorGroup)
                .ThenInclude(x => x.ElevatorServers)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<HandleElevatorDeviceEntity> GetWithServersAndFloorsByIdAsync(string id)
        {
            var entity = await _context.HandElevatorDevices
                .Include(x => x.Floor)
                .Include(x => x.ElevatorGroup.ElevatorServers)
                .Include(x => x.ElevatorGroup)
                .ThenInclude(x => x.ElevatorGroupFloors)
                .ThenInclude(x => x.Floor)
                .ThenInclude(x => x.Edifice)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<HandleElevatorDeviceEntity> GetWithElevatorGroupByIdAsync(string id)
        {
            var entity = await _context.HandElevatorDevices
                .Include(x => x.Floor)
                .Include(x => x.ElevatorGroup)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
    }
}
