using KT.Common.Data.Daos;
using KT.Visitor.Data.Base;
using KT.Visitor.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.IDaos
{
    public interface ISystemConfigDataDao : IBaseDataDao<SystemConfigEntity>
    {
        Task<SystemConfigEntity> SelectByKeyAsync(string key);
    }
}
