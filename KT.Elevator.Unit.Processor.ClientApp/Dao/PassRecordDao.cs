using KT.Common.Data.Daos;
using KT.Elevator.Unit.Processor.ClientApp.Dao.Base;
using KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Entity.Entities;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Dao.Daos
{
    public class PassRecordDao : BaseDataDao<UnitPassRecordEntity>, IPassRecordDao
    {
        private ElevatorUnitDbContext _context;
        public PassRecordDao(ElevatorUnitDbContext context) : base(context)
        {
            _context = context;
        }
 
    }
}
