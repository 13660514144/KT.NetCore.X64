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
    public class DopSpecificDefaultAccessMaskDao : BaseDataDao<DopSpecificDefaultAccessMaskEntity>, IDopSpecificDefaultAccessMaskDao
    {
        private QuantaDbContext _context;
        public DopSpecificDefaultAccessMaskDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<DopSpecificDefaultAccessMaskEntity>> GetWithFloorsByGroupIdAsync(string elevatorGroupId)
        {
            var results = await _context.DopSpecificDefaultAccessMasks
                .Include(x => x.HandleElevatorDevice)
                .Include(x => x.MaskFloors)
                .Where(x => x.ElevatorGroupId == elevatorGroupId)
                .ToListAsync();

            return results;
        }
        public async Task<DopSpecificDefaultAccessMaskEntity> GetWithFloorsByIdAsync(string id)
        {
            var result = await _context.DopSpecificDefaultAccessMasks
                .Include(x => x.MaskFloors)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
        public async Task<List<DopSpecificDefaultAccessMaskEntity>> GetAllAsync()
        {
            var results = await _context.DopSpecificDefaultAccessMasks
                .ToListAsync();

            return results;
        }

        public async Task<List<DopSpecificDefaultAccessMaskEntity>> GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDeviceAsync()
        {
            var results = await _context.DopSpecificDefaultAccessMasks
                .Include(x => x.ElevatorGroup)
                .Include(x => x.HandleElevatorDevice)
                .ToListAsync();

            return results;
        }

    }
}
