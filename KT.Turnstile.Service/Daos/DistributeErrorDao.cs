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
    public class DistributeErrorDao : BaseDataDao<DistributeErrorEntity>, IDistributeErrorDao
    {
        private QuantaDbContext _context;
        public DistributeErrorDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DistributeErrorEntity> AddAsync(DistributeErrorEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = IdUtil.NewId();
            }
            //配置基本信息
            entity.CreatedTime = entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Creator = entity.Editor = DataStaticInfo.CurrentUserId;

            //查询关联类
            entity.Processor = await _context.Processors.FirstOrDefaultAsync(x => x.Id == entity.Processor.Id);

            //执行新增操作
            var result = await _context.DistributeErrors.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public async Task DeleteAsync(string id)
        {
            //查询出要删除的数据
            var entity = await _context.DistributeErrors.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return;
            }

            //执行新增操作
            _context.DistributeErrors.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(List<DistributeErrorEntity> entities)
        {
            //执行新增操作
            foreach (var item in entities)
            {
                _context.DistributeErrors.Remove(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<DistributeErrorEntity> EditAsync(DistributeErrorEntity entity)
        {
            //配置基本信息
            entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Editor = DataStaticInfo.CurrentUserId;

            //查询关联类
            entity.Processor = await _context.Processors.FirstOrDefaultAsync(x => x.Id == entity.Processor.Id);

            //执行新增操作
            var result = _context.DistributeErrors.Update(entity);
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public async Task<List<DistributeErrorEntity>> GetAllAsync()
        {
            var results = await _context.DistributeErrors
                .Include(x => x.Processor)
                .ToListAsync();
            return results;
        }

        public async Task<DistributeErrorEntity> GetByIdAsync(string id)
        {
            var result = await _context.DistributeErrors
                .Include(x => x.Processor)
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<DistributeErrorEntity>> GetByProcessorIdAsync(string id)
        {
            var results = await _context.DistributeErrors
                .OrderBy(x => x.EditedTime).
                Where(x => x.Processor.Id == id).
                ToListAsync();
            return results;
        }
    }
}
