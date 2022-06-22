using KT.Common.Data.Daos;
using KT.Elevator.Unit.Processor.ClientApp.Dao.Base;
using KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Entity.Entities;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Dao.Daos
{
    public class CardDeviceDao : BaseDataDao<UnitCardDeviceEntity>, ICardDeviceDao
    {
        private ElevatorUnitDbContext _context;
        public CardDeviceDao(ElevatorUnitDbContext context) : base(context)
        {
            _context = context;
        }
 
    }
}
