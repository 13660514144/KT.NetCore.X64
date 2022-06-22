using KT.Visitor.Data.Entity;
using KT.Visitor.Data.Enums;
using KT.Visitor.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.IServices
{
    public interface ISystemConfigDataService
    {
        Task AddOrUpdateAsync(SystemConfigModel model);
        Task AddOrUpdateAsync(SystemConfigEnum keyEnum, object value);
        Task AddOrUpdatesAsync(List<SystemConfigEntity> entities);
        Task<SystemConfigModel> GetAsync();
    }
}
