using KT.Common.Data.Daos;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Daos
{
    public class HandleElevatorDeviceDao : BaseDataDao<HandleElevatorDeviceEntity>, IHandleElevatorDeviceDao
    {
        private ElevatorDbContext _context;
        public HandleElevatorDeviceDao(ElevatorDbContext context) : base(context)
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
            await AttachUpdateAsync(entity);

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
                .Include(x => x.Floor)
                .Include(x => x.ElevatorGroup)
                .ThenInclude(x => x.RelationFloors)
                .ThenInclude(x => x.Floor)
                .ThenInclude(x => x.Edifice)
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
                .ThenInclude(x => x.RelationFloors)
                .ThenInclude(x => x.Floor)
                .ThenInclude(x => x.Edifice)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
    }
}
