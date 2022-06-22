using KT.Common.Core.Exceptions;
using KT.Common.Data.Daos;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class PassRightDestinationFloorDao : BaseDataDao<PassRightDestinationFloorEntity>, IPassRightDestinationFloorDao
    {
        private QuantaDbContext _context;
        public PassRightDestinationFloorDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PassRightDestinationFloorEntity>> GetWithDetailBySignAsync(string sign)
        {
            var entities = await _context.PassRightDestinationFloors
                .Where(x => x.Sign == sign)
                .ToListAsync();
            return entities;
        }

        public async Task<PassRightDestinationFloorEntity> GetWithFloorAsync(string sign, string elevatorGroupId)
        {
            var entity = await _context.PassRightDestinationFloors
                .Include(x => x.Floor)
                .FirstOrDefaultAsync(x => x.Sign == sign
                                       && x.ElevatorGroupId == elevatorGroupId);
            return entity;
        }

        public async Task<PassRightDestinationFloorEntity> GetBySignAndElevatorGroupIdAsync(string sign, string elevatorGroupId)
        {
            var entity = await _context.PassRightDestinationFloors
                .FirstOrDefaultAsync(x => x.Sign == sign
                                       && x.ElevatorGroupId == elevatorGroupId);
            return entity;
        }
    }
}
