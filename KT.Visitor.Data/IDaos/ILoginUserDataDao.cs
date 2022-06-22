using KT.Common.Data.Daos;
using KT.Visitor.Data.Base;
using KT.Visitor.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.IDaos
{
    public interface ILoginUserDataDao : IBaseDataDao<LoginUserEntity>
    {
        Task<bool> IsExistsSystemUserAsync(string password);
        Task<LoginUserEntity> GetByAccountAsync(string account);
    }
}
