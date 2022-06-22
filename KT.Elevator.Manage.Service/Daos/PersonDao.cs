using KT.Common.Data.Daos;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Daos
{
    public class PersonDao : BaseDataDao<PersonEntity>, IPersonDao
    {
        private ElevatorDbContext _context;
        public PersonDao(ElevatorDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
