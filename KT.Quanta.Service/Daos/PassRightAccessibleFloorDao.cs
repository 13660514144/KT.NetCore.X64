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
    public class PassRightAccessibleFloorDao : BaseDataDao<PassRightAccessibleFloorEntity>, IPassRightAccessibleFloorDao
    {
        private QuantaDbContext _context;
        public PassRightAccessibleFloorDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PassRightAccessibleFloorEntity>> GetWithDetailBySignAsync(string sign)
        {
            var entities = await _context.PassRightAccessibleFloors
                .Include(x => x.PassRightAccessibleFloorDetails)
                .Where(x => x.Sign == sign)
                .ToListAsync();
            return entities;
        }

        public async Task<PassRightAccessibleFloorEntity> GetWithDetailBySignAndElevatorGroupIdAsync(string sign, string elevatorGroupId)
        {
            var entity = await _context.PassRightAccessibleFloors
                .Include(x => x.PassRightAccessibleFloorDetails)
                .FirstOrDefaultAsync(x => x.Sign == sign
                                       && x.ElevatorGroupId == elevatorGroupId);
            return entity;
        }

        public async Task<PassRightAccessibleFloorEntity> GetBySignAndElevatorGroupIdAsync(string sign, string elevatorGroupId)
        {
            var entity = await _context.PassRightAccessibleFloors
                .FirstOrDefaultAsync(x => x.Sign == sign
                                       && x.ElevatorGroupId == elevatorGroupId);
            return entity;
        }
    }
}
