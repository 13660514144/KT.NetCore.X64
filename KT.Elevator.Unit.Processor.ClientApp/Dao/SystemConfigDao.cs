using KT.Common.Data.Daos;
using KT.Elevator.Unit.Processor.ClientApp.Dao.Base;
using KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Entity.Entities;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Dao.Daos
{
    public class SystemConfigDao : BaseDataDao<UnitSystemConfigEntity>, ISystemConfigDao
    {
        private ElevatorUnitDbContext _context;
        public SystemConfigDao(ElevatorUnitDbContext context) : base(context)
        {
            _context = context;
        }
 
    }
}
