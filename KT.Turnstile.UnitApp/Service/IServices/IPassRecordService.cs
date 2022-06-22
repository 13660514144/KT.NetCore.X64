using KT.Elevator.Unit.Entity.Models;
using KT.Turnstile.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.IServices
{
    public interface IPassRecordService
    {
        Task AddAsync(TurnstileUnitPassRecordEntity entity);
        Task Delete(string id);
        Task<TurnstileUnitPassRecordEntity> GetLast();
    }
}
