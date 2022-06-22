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
    public class HandleElevatorDeviceAuxiliaryDao : BaseDataDao<HandleElevatorDeviceAuxiliaryEntity>, IHandleElevatorDeviceAuxiliaryDao
    {
        private QuantaDbContext _context;
        public HandleElevatorDeviceAuxiliaryDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<HandleElevatorDeviceAuxiliaryEntity> GetByHandleElevatorDeviceIdAsync(string handleElevatorDeviceId)
        {
            var entity = await _context.HandleElevatorDeviceAuxiliaries
                .FirstOrDefaultAsync(x => x.HandleElevatorDeviceId == handleElevatorDeviceId);
            return entity;
        }
    }
}
