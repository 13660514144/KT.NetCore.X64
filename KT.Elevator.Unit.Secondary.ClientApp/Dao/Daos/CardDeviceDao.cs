using KT.Common.Data.Daos;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.Base;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Entity.Entities;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Dao.Daos
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
