using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.IServices
{
    public interface IErrorService
    {
        Task AddOrUpdateAsync(List<UnitErrorModel> entities);
        Task AddOrUpdateAsync(UnitErrorModel entity);
    }
}
