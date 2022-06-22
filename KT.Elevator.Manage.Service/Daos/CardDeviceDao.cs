using KT.Common.Core.Exceptions;
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
    public class CardDeviceDao : BaseDataDao<CardDeviceEntity>, ICardDeviceDao
    {
        private ElevatorDbContext _context;
        public CardDeviceDao(ElevatorDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CardDeviceEntity> AddAsync(CardDeviceEntity entity)
        {
            if (entity.Processor != null)
            {
                entity.Processor = await _context.Processors.FirstOrDefaultAsync(x => x.Id == entity.Processor.Id);
            }
            else
            {
                throw CustomException.Run($"关联边缘处理器不能为空！");
            }
            if (entity.HandElevatorDevice != null)
            {
                entity.HandElevatorDevice = await _context.HandElevatorDevices.FirstOrDefaultAsync(x => x.Id == entity.HandElevatorDevice.Id);
            }
            if (entity.SerialConfig != null)
            {
                entity.SerialConfig = await _context.SerialConfigs.FirstOrDefaultAsync(x => x.Id == entity.SerialConfig.Id);
            }

            await InsertAsync(entity);

            return entity;
        }

        public async Task<CardDeviceEntity> DeleteReturnWidthProcessorAsync(string id)
        {
            //查询出要删除的数据
            var entity = await _context.CardDevices
                .Include(x => x.Processor)
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

        public async Task<CardDeviceEntity> EditAsync(CardDeviceEntity entity)
        {
            if (entity.Processor != null)
            {
                entity.Processor = await _context.Processors.FirstOrDefaultAsync(x => x.Id == entity.Processor.Id);
            }
            else
            {
                throw CustomException.Run($"关联边缘处理器不能为空！");
            }
            if (entity.HandElevatorDevice != null)
            {
                entity.HandElevatorDevice = await _context.HandElevatorDevices.FirstOrDefaultAsync(x => x.Id == entity.HandElevatorDevice.Id);
            }
            if (entity.SerialConfig != null)
            {
                entity.SerialConfig = await _context.SerialConfigs.FirstOrDefaultAsync(x => x.Id == entity.SerialConfig.Id);
            }

            await AttachUpdateAsync(entity);

            return entity;
        }

        public async Task<List<CardDeviceEntity>> GetAllAsync()
        {
            var entities = await _context.CardDevices.Include(x => x.Processor)
                 .Include(x => x.HandElevatorDevice)
                 .Include(x => x.SerialConfig)
                 .ToListAsync();
            return entities;
        }

        public async Task<CardDeviceEntity> GetByIdAsync(string id)
        {
            var entity = await _context.CardDevices.Include(x => x.Processor)
                .Include(x => x.HandElevatorDevice)
                .Include(x => x.SerialConfig)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
    }
}
