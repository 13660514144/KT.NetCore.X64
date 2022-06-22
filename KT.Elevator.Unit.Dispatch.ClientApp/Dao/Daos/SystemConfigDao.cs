using KT.Common.Data.Daos;
using KT.Elevator.Unit.Dispatch.ClientApp.Dao.Base;
using KT.Elevator.Unit.Dispatch.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Dispatch.Entity.Entities;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Dao.Daos
{
    public class SystemConfigDao : BaseDataDao<UnitDispatchSystemConfigEntity>, ISystemConfigDao
    {
        private ElevatorUnitDbContext _context;
        public SystemConfigDao(ElevatorUnitDbContext context) : base(context)
        {
            _context = context;
        }
 
    }
}
