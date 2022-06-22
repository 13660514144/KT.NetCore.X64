using KT.Common.Data.Daos;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.Base;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Entity.Entities;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Dao.Daos
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
