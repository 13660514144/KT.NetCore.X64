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
    public class PassRightAccessibleFloorDetailDao : BaseDataDao<PassRightAccessibleFloorDetailEntity>, IPassRightAccessibleFloorDetailDao
    {
        private QuantaDbContext _context;
        public PassRightAccessibleFloorDetailDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<PassRightAccessibleFloorDetailEntity> GetAsync(string sign, string elevatorGroupId, string floorId)
        {
            var entity = _context.PassRightAccessibleFloorDetails
                .FirstOrDefaultAsync(x => x.PassRightAccessibleFloor.Sign == sign
                   && x.PassRightAccessibleFloor.ElevatorGroupId == elevatorGroupId
                   && x.FloorId == floorId);

            return entity;
        }

        public Task<PassRightAccessibleFloorDetailEntity> GetWithFloorAsync(string sign,
            string elevatorGroupId)
        {
            var entity = _context.PassRightAccessibleFloorDetails
               .FirstOrDefaultAsync(x => x.PassRightAccessibleFloor.Sign == sign
                   && x.PassRightAccessibleFloor.ElevatorGroupId == elevatorGroupId);

            return entity;
        }
    }
}
