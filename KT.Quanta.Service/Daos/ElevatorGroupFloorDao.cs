using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class ElevatorGroupFloorDao : BaseDataDao<ElevatorGroupFloorEntity>, IElevatorGroupFloorDao
    {
        private QuantaDbContext _context;
        public ElevatorGroupFloorDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ElevatorGroupFloorEntity> GetWithFloorByElevatorGroupIdAndFloorIdAsync(string elevatorGroupId, string floorId)
        {
            var entity = await _context.ElevatorGroupFloors
                .Include(x => x.Floor)
                .FirstOrDefaultAsync(x => x.ElevatorGroupId == elevatorGroupId && x.FloorId == floorId);

            return entity;
        }
        public async Task<ElevatorGroupFloorEntity> GetByElevatorGroupIdAndFloorIdAsync(string elevatorGroupId, string floorId)
        {
            var entity = await _context.ElevatorGroupFloors
                .FirstOrDefaultAsync(x => x.ElevatorGroupId == elevatorGroupId && x.FloorId == floorId);

            return entity;
        }
        public async Task<List<ElevatorGroupFloorEntity>> GetByElevatorGroupIdAndFloorIdsAsync(string elevatorGroupId, List<string> destinationFloorIds)
        {
            var entities = await _context.ElevatorGroupFloors
                .Where(x => x.ElevatorGroupId == elevatorGroupId && destinationFloorIds.Contains(x.FloorId))
                .ToListAsync();

            return entities;
        }

        public async Task<List<ElevatorGroupFloorEntity>> GetWithFloorsByElevatorGroupIdAndFloorIdsAsync(string elevatorGroupId, List<string> destinationFloorIds)
        {
            var entities = await _context.ElevatorGroupFloors
                .Include(x => x.Floor)
                .Where(x => x.ElevatorGroupId == elevatorGroupId && destinationFloorIds.Contains(x.FloorId))
                .ToListAsync();

            return entities;
        }
        public async Task<List<ElevatorGroupFloorEntity>> GetWithEdificeFloorsByElevatorGroupIdAndFloorIdsAsync(string elevatorGroupId, List<string> destinationFloorIds)
        {
            var entities = await _context.ElevatorGroupFloors
                .Include(x => x.Floor)
                .ThenInclude(x => x.Edifice)
                .Where(x => x.ElevatorGroupId == elevatorGroupId && destinationFloorIds.Contains(x.FloorId))
                .ToListAsync();

            return entities;
        }

        public async Task<ElevatorGroupFloorEntity> GetByElevatorGroupIdAndRealFloorIdAsync(string elevatorGroupId, string realFloorId)
        {
            var entity = await _context.ElevatorGroupFloors
             .FirstOrDefaultAsync(x => x.ElevatorGroupId == elevatorGroupId && x.RealFloorId == realFloorId);

            return entity;
        }
        public async Task<ElevatorGroupFloorEntity> GetWithFloorByElevatorGroupIdAndRealFloorIdAsync(string elevatorGroupId, string realFloorId)
        {
            var entity = await _context.ElevatorGroupFloors
                .Include(x => x.Floor)
                .FirstOrDefaultAsync(x => x.ElevatorGroupId == elevatorGroupId && x.RealFloorId == realFloorId);

            return entity;
        }
    }
}
