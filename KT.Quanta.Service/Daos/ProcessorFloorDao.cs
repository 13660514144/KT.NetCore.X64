using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class ProcessorFloorDao : BaseDataDao<ProcessorFloorEntity>, IProcessorFloorDao
    {
        private QuantaDbContext _context;
        public ProcessorFloorDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task EditAsync(ProcessorFloorEntity entity)
        {
            entity.EditedTime = DateTimeUtil.UtcNowMillis();

            if (entity.ProcessorId.IsIdNull())
            {
                entity.Processor = await _context.Processors.FirstOrDefaultAsync(x => x.Id == entity.Processor.Id);
            }
            if (entity.FloorId.IsIdNull())
            {
                entity.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == entity.Floor.Id);
            }

            _context.ProcessorFloors.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(ProcessorFloorEntity entity)
        {
            entity.CreatedTime = entity.EditedTime = DateTimeUtil.UtcNowMillis();

            if (entity.ProcessorId.IsIdNull())
            {
                entity.Processor = await _context.Processors.FirstOrDefaultAsync(x => x.Id == entity.Processor.Id);
            }
            if (entity.FloorId.IsIdNull())
            {
                entity.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == entity.Floor.Id);
            }

            await _context.ProcessorFloors.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<ProcessorFloorEntity> GetWithFloorByProcessorIdAndSortIdAsync(string processorId, int sortId)
        {
            var result = await _context.ProcessorFloors
                .Include(x => x.Floor)
                .FirstOrDefaultAsync(x => x.ProcessorId == processorId && x.SortId == sortId);

            return result;
        }
    }
}
