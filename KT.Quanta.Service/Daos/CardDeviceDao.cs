using KT.Common.Core.Exceptions;
using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class CardDeviceDao : BaseDataDao<CardDeviceEntity>, ICardDeviceDao
    {
        private QuantaDbContext _context;
        public CardDeviceDao(QuantaDbContext context) : base(context)
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
            if (entity.HandleElevatorDevice != null)
            {
                entity.HandleElevatorDevice = await _context.HandElevatorDevices.FirstOrDefaultAsync(x => x.Id == entity.HandleElevatorDevice.Id);
            }
            if (entity.SerialConfig != null)
            {
                entity.SerialConfig = await _context.SerialConfigs.FirstOrDefaultAsync(x => x.Id == entity.SerialConfig.Id);
            }
            if (entity.RelayDevice != null)
            {
                entity.RelayDevice = await _context.RelayDevices.FirstOrDefaultAsync(x => x.Id == entity.RelayDevice.Id);
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
            if (entity.HandleElevatorDevice != null)
            {
                entity.HandleElevatorDevice = await _context.HandElevatorDevices.FirstOrDefaultAsync(x => x.Id == entity.HandleElevatorDevice.Id);
            }
            if (entity.SerialConfig != null)
            {
                entity.SerialConfig = await _context.SerialConfigs.FirstOrDefaultAsync(x => x.Id == entity.SerialConfig.Id);
            }
            if (entity.RelayDevice != null)
            {
                entity.RelayDevice = await _context.RelayDevices.FirstOrDefaultAsync(x => x.Id == entity.RelayDevice.Id);
            }

            await AttachAsync(entity);

            return entity;
        }

        public async Task<List<CardDeviceEntity>> GetAllAsync()
        {
            var entities = await _context.CardDevices.Include(x => x.Processor)
                 .Include(x => x.HandleElevatorDevice)
                 .Include(x => x.SerialConfig)
                 .ToListAsync();
            return entities;
        }

        public async Task<List<CardDeviceEntity>> GetByDeviceTypeAsync(string deviceType)
        {
            var entities = await _context.CardDevices
              .Include(x => x.Processor)
              .Include(x => x.HandleElevatorDevice)
              .Include(x => x.SerialConfig)
              .Where(x => x.DeviceType == deviceType)
              .ToListAsync();
            return entities;
        }

        public async Task<CardDeviceEntity> GetByIdAsync(string id)
        {
            var entity = await _context.CardDevices
                .Include(x => x.Processor)
                .Include(x => x.HandleElevatorDevice)
                .Include(x => x.SerialConfig)
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<List<CardDeviceEntity>> GetByProcessorIdAsync(string processorId)
        {
            var results = await _context.CardDevices
               .Include(x => x.RelayDevice)
               .Include(x => x.SerialConfig)
               .Include(x => x.Processor)
               .Where(x => x.Processor.Id == processorId)
               .ToListAsync();
            return results;
        }

        public async Task<List<CardDeviceEntity>> GetByInAndNotAsync(List<string> inIds, List<string> notIds)
        {
            if (inIds?.FirstOrDefault() == null)
            {
                return new List<CardDeviceEntity>();
            }

            if (notIds?.FirstOrDefault() == null)
            {
                var entities = await _context.CardDevices
                     .Where(x => inIds.Contains(x.Id))
                     .ToListAsync();

                return entities;
            }
            else
            {
                var entities = await _context.CardDevices
                 .Where(x => inIds.Contains(x.Id) && (!notIds.Contains(x.Id)))
                 .ToListAsync();

                return entities;
            }
        }

        public async Task<List<CardDeviceEntity>> GetByInAsync(List<string> inIds)
        {
            if (inIds?.FirstOrDefault() == null)
            {
                return new List<CardDeviceEntity>();
            }
            var entities = await _context.CardDevices
            .Where(x => inIds.Contains(x.Id))
            .ToListAsync();

            return entities;
        }

        public async Task<List<CardDeviceEntity>> GetByNotAsync(List<string> notIds)
        {
            if (notIds?.FirstOrDefault() == null)
            {
                var entities = await _context.CardDevices
                    .ToListAsync();

                return entities;
            }
            else
            {
                var entities = await _context.CardDevices
                    .Where(x => !notIds.Contains(x.Id))
                    .ToListAsync();

                return entities;
            }
        }

        public async Task<CardDeviceEntity> GetWithProcessorByIdAsync(string cardDeviceId)
        {
            var entity = await _context.CardDevices
                .Include(x => x.Processor)
                .FirstOrDefaultAsync(x => x.Id == cardDeviceId);

            return entity;
        }
    }
}
