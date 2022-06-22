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
    public class FaceInfoDao : BaseDataDao<FaceInfoEntity>, IFaceInfoDao
    {
        public FaceInfoDao(ElevatorDbContext context) : base(context)
        {
        }
    }
}
