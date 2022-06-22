using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class HandleElevatorInputDeviceDao : BaseDataDao<HandleElevatorInputDeviceEntity>, IHandleElevatorInputDeviceDao
    {
        private QuantaDbContext _context;
        public HandleElevatorInputDeviceDao(QuantaDbContext context) : base(context)
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

            await AttachAsync(entity);

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
