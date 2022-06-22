using KT.Common.Data.Daos;
using KT.Quanta.Entity.Kone;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class DopGlobalDefaultAccessMaskDao : BaseDataDao<DopGlobalDefaultAccessMaskEntity>, IDopGlobalDefaultAccessMaskDao
    {
        private QuantaDbContext _context;
        public DopGlobalDefaultAccessMaskDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<DopGlobalDefaultAccessMaskEntity>> GetWithFloorsByGroupIdAsync(string elevatorGroupId)
        {
            var results = await _context.DopGlobalDefaultAccessMasks
                .Include(x => x.MaskFloors)
                .Where(x => x.ElevatorGroupId == elevatorGroupId)
                .OrderBy(x => x.ElevatorGroup.Name)
                .ToListAsync();

            return results;
        }

        public async Task<DopGlobalDefaultAccessMaskEntity> GetWithFloorsByIdAsync(string id)
        {
            var result = await _context.DopGlobalDefaultAccessMasks
                .Include(x => x.MaskFloors)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<List<DopGlobalDefaultAccessMaskEntity>> GetAllAsync()
        {
            var results = await _context.DopGlobalDefaultAccessMasks
                .ToListAsync();

            return results;
        }
        public async Task<List<DopGlobalDefaultAccessMaskEntity>> GetAllDopGlobalMasksWithElevatorGroupAsync()
        {
            var results = await _context.DopGlobalDefaultAccessMasks
                .Include(x => x.ElevatorGroup)
                .ToListAsync();

            return results;
        }

    }
}
