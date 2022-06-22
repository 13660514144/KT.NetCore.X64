using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Turnstile.Manage.Service.Base;
using KT.Turnstile.Manage.Service.IDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KT.Turnstile.Entity;
using KT.Turnstile.Entity.Entities;
using KT.Common.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using KT.Common.Data.Daos;

namespace KT.Turnstile.Manage.Service.Daos
{
    public class SerialConfigDao : BaseDataDao<SerialConfigEntity>, ISerialConfigDao
    {
        private QuantaDbContext _context;
        public SerialConfigDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SerialConfigEntity> AddAsync(SerialConfigEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = IdUtil.NewId();
            }

            //配置基本信息
            entity.CreatedTime = entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Creator = entity.Editor = DataStaticInfo.CurrentUserId;

            //执行新增操作
            var result = await _context.SerialConfigs.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.SerialConfigs.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return false;
            }

            var result = _context.SerialConfigs.Remove(entity);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<SerialConfigEntity> EditAsync(SerialConfigEntity entity)
        {
            //配置基本信息
            entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Editor = DataStaticInfo.CurrentUserId;

            //执行新增操作
            var result = _context.SerialConfigs.Update(entity);
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public async Task<List<SerialConfigEntity>> GetAllAsync()
        {
            var results = await _context.SerialConfigs.ToListAsync();
            return results;
        }

        public async Task<SerialConfigEntity> GetByIdAsync(string id)
        {
            var result = await _context.SerialConfigs.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
