using KT.Common.Data.Daos;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Daos
{
    public class LoginUserDao : BaseDataDao<LoginUserEntity>, ILoginUserDao
    {
        public LoginUserDao(ElevatorDbContext context) : base(context)
        {
        }
    }
}
