using KT.Common.Data.Daos;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface ISystemConfigDao : IBaseDataDao<SystemConfigEntity>
    {

    }
}
