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
    public class HandleElevatorInputDeviceDao : BaseDataDao<HandleElevatorInputDeviceEntity>, IHandleElevatorInputDeviceDao
    {
        private ElevatorDbContext _context;
        public HandleElevatorInputDeviceDao(ElevatorDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<HandleElevatorInputDeviceEntity> AddAsync(HandleElevatorInputDeviceEntity entity)
        {
            if (entity.HandElevatorDevice != null)
            {
                entity.HandElevatorDevice = await _context.HandElevatorDevices.FirstOrDefaultAsync(x => x.Id == entity.HandElevatorDevice.Id);
            }

            await InsertAsync(entity);

            return entity;
        }

        public async Task<HandleElevatorInputDeviceEntity> DeleteReturnWidthHandleElevatorDeviceAsync(string id)
        {
            //查询出要删除的数据
            var entity = await _context.HandleElevatorInputDevices
                .Include(x => x.HandElevatorDevice)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            //删除数据
            await DeleteByIdAsync(id);

            return entity;
        }

        public async Task<HandleElevatorInputDeviceEntity> EditAsync(HandleElevatorInputDeviceEntity entity)
        {
            if (entity.HandElevatorDevice != null)
            {
                entity.HandElevatorDevice = await _context.HandElevatorDevices.FirstOrDefaultAsync(x => x.Id == entity.HandElevatorDevice.Id);
            }

            await AttachUpdateAsync(entity);

            return entity;
        }

        public async Task<List<HandleElevatorInputDeviceEntity>> GetAllAsync()
        {
            var entities = await _context.HandleElevatorInputDevices
                 .Include(x => x.HandElevatorDevice)
                 .ToListAsync();
            return entities;
        }

        public async Task<HandleElevatorInputDeviceEntity> GetByDeviceIdAndCardTypeAsync(string handleElevatorDeviceId, string deviceType)
        {
            var entity = await _context.HandleElevatorInputDevices
             .FirstOrDefaultAsync(x => x.HandElevatorDevice.Id == handleElevatorDeviceId
                                        && x.DeviceType == deviceType);
            return entity;
        }

        public async Task<HandleElevatorInputDeviceEntity> GetByIdAsync(string id)
        {
            var entity = await _context.HandleElevatorInputDevices
                .Include(x => x.HandElevatorDevice)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
    }
}
