using KT.Common.Data.Daos;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Daos
{
    public class SystemConfigDao : BaseDataDao<SystemConfigEntity>, ISystemConfigDao
    {
        public SystemConfigDao(ElevatorDbContext context) : base(context)
        {
        }
    }
}
