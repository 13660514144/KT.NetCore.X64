using KT.Common.Data.Daos;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Daos
{
    public class PassRecordDao : BaseDataDao<PassRecordEntity>, IPassRecordDao
    {
        public PassRecordDao(ElevatorDbContext context) : base(context)
        {
        }

    }
}
