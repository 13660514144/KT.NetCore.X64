using KT.Common.Data.Daos;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Daos
{
    public class SerialConfigDao : BaseDataDao<SerialConfigEntity>, ISerialConfigDao
    {
        public SerialConfigDao(ElevatorDbContext context) : base(context)
        {
        }
    }
}
