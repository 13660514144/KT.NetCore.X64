using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Turnstile.Manage.Service.Base;
using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Daos
{
    public class CardDeviceDao : BaseDataDao<CardDeviceEntity>, ICardDeviceDao
    {
        private QuantaDbContext _context;
        public CardDeviceDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CardDeviceEntity> AddAsync(CardDeviceEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = IdUtil.NewId();
            }

            //配置基本信息
            entity.CreatedTime = entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Creator = entity.Editor = DataStaticInfo.CurrentUserId;

            //关联数据 
            entity.SerialConfig = await _context.SerialConfigs.FirstOrDefaultAsync(x => x.Id == entity.SerialConfig.Id);
            entity.RelayDevice = await _context.RelayDevices.FirstOrDefaultAsync(x => x.Id == entity.RelayDevice.Id);
            entity.Processor = await _context.Processors.FirstOrDefaultAsync(x => x.Id == entity.Processor.Id);

            //执行新增操作
            var result = await _context.CardDevices.AddAsync(entity);

            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            //查询出要删除的数据
            var entity = await _context.CardDevices.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return false;
            }

            //执行新增操作
            var result = _context.CardDevices.Remove(entity);
            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<CardDeviceEntity> EditAsync(CardDeviceEntity entity)
        {
            //配置基本信息
            entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Editor = DataStaticInfo.CurrentUserId;

            //关联数据 
            entity.SerialConfig = await _context.SerialConfigs.FirstOrDefaultAsync(x => x.Id == entity.SerialConfig.Id);
            entity.RelayDevice = await _context.RelayDevices.FirstOrDefaultAsync(x => x.Id == entity.RelayDevice.Id);
            entity.Processor = await _context.Processors.FirstOrDefaultAsync(x => x.Id == entity.Processor.Id);

            //执行新增操作
            var result = _context.CardDevices.Update(entity);
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public async Task<List<CardDeviceEntity>> GetAllAsync()
        {
            var results = await _context.CardDevices
                .Include(x => x.RelayDevice)
                .Include(x => x.SerialConfig)
                .Include(x => x.Processor)
                .ToListAsync();
            return results;
        }

        public async Task<List<CardDeviceEntity>> GetByProcessorIdAsync(string id)
        {
            var results = await _context.CardDevices
                .Include(x => x.RelayDevice)
                .Include(x => x.SerialConfig)
                .Include(x => x.Processor)
                .Where(x => x.Processor.Id == id)
                .ToListAsync();
            return results;
        }

        public async Task<CardDeviceEntity> GetByIdAsync(string id)
        {
            var result = await _context.CardDevices
                .Include(x => x.RelayDevice)
                .Include(x => x.SerialConfig)
                .Include(x => x.Processor)
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<CardDeviceEntity> DeleteReturnWidthProcessorAsync(string id)
        {
            //查询出要删除的数据
            var entity = await _context.CardDevices
                .Include(x => x.Processor)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            //删除数据
            await DeleteByIdAsync(id);

            return entity;
        }
    }
}
