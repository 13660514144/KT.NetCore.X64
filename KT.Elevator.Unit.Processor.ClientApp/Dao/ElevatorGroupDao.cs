using KT.Common.Data.Daos;
using KT.Elevator.Unit.Processor.ClientApp.Dao.Base;
using KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Entity.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KT.Elevator.Unit.Processor.ClientApp.Dao.Daos
{
    public class ElevatorGroupDao : BaseDataDao<UnitElevatorGroupEntity>, IElevatorGroupDao
    {
        private ElevatorUnitDbContext _context;
        public ElevatorGroupDao(ElevatorUnitDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<UnitElevatorGroupEntity> GetWithRelevanceFloorsByIdAsync(string elevatorGroupId)
        {
            var entities = _context.ElevatorGroups
                .Include(x => x.ElevatorGroupFloors)
                .FirstOrDefaultAsync();

            return entities;
        }
    }
}
