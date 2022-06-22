using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class ElevatorInfoDao : BaseDataDao<ElevatorInfoEntity>, IElevatorInfoDao
    {
        private QuantaDbContext _context;
        public ElevatorInfoDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ElevatorInfoEntity> AddAsync(ElevatorInfoEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.ElevatorGroupId))
            {
                entity.ElevatorGroup = await _context.ElevatorGroups.FirstOrDefaultAsync(x => x.Id == entity.ElevatorGroupId);
            }
            await InsertAsync(entity);
            return entity;
        }

        public async Task<ElevatorInfoEntity> EidtAsync(ElevatorInfoEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.ElevatorGroupId))
            {
                entity.ElevatorGroup = await _context.ElevatorGroups.FirstOrDefaultAsync(x => x.Id == entity.ElevatorGroupId);
            }

            await AttachAsync(entity);

            return entity;

        }

        public async Task<ElevatorInfoEntity> GetByElevatorGroupIdAndRealIdAsync(string elevatorGroupId, string realId)
        {
            return await _context.ElevatorInfos.FirstOrDefaultAsync(x => x.ElevatorGroupId == elevatorGroupId && x.RealId == realId);
        }
    }
}
