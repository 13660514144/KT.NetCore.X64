using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Elevator.Unit.Processor.ClientApp.Dao.Base;
using KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Dao.Daos
{
    public class HandleElevatorDeviceDao : BaseDataDao<UnitHandleElevatorDeviceEntity>, IHandleElevatorDeviceDao
    {
        private ElevatorUnitDbContext _context;
        public HandleElevatorDeviceDao(ElevatorUnitDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
