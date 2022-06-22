using KT.Turnstile.Unit.ClientApp.Dao.IDaos;
using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KT.Elevator.Unit.Entity.Models;
using KT.Common.Core.Utils;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace KT.Turnstile.Unit.ClientApp.Service.Services
{
    public class PassRecordService : IPassRecordService
    {
        private IPassRecordDao _dao;

        public PassRecordService(IPassRecordDao dao)
        {
            _dao = dao;
        }

        public async Task<TurnstileUnitPassRecordEntity> GetLast()
        {
            return await _dao.SelectLastAsync();
        }

        public async Task AddAsync(TurnstileUnitPassRecordEntity entity)
        {
            await _dao.InsertAsync(entity);
        }

        public async Task Delete(string id)
        {
            var oldEntity = await _dao.SelectByIdAsync(id);
            if (oldEntity != null)
            {
                await _dao.DeleteAsync(oldEntity);
            }
        }
    }
}
