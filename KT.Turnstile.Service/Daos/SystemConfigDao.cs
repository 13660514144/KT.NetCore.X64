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
    public class SystemConfigDao : BaseDataDao<SystemConfigEntity>, ISystemConfigDao
    {
        private QuantaDbContext _context;
        public SystemConfigDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SystemConfigEntity> AddAsync(SystemConfigEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = IdUtil.NewId();
            }
            //配置基本信息
            entity.CreatedTime = entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Creator = entity.Editor = DataStaticInfo.CurrentUserId;

            //执行新增操作
            var result = await _context.SystemConfigs.AddAsync(entity);
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
            var entity = await _context.SystemConfigs.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return false;
            }

            //执行新增操作
            var result = _context.SystemConfigs.Remove(entity);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<SystemConfigEntity> EditAsync(SystemConfigEntity entity)
        {
            //配置基本信息
            entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Editor = DataStaticInfo.CurrentUserId;

            //执行新增操作
            var result = _context.SystemConfigs.Update(entity);
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public async Task<List<SystemConfigEntity>> GetAllAsync()
        {
            var results = await _context.SystemConfigs.ToListAsync();
            return results;
        }

        public async Task<SystemConfigEntity> GetByIdAsync(string id)
        {
            var result = await _context.SystemConfigs.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<SystemConfigEntity> GetByKeyAsync(string key)
        {
            var result = await _context.SystemConfigs.FirstOrDefaultAsync(x => x.Key == key);
            return result;
        }
    }
}
