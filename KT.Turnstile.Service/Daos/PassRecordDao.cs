using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Turnstile.Manage.Service.Base;
using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace KT.Turnstile.Manage.Service.Daos
{
    public class PassRecordDao : BaseDataDao<PassRecordEntity>, IPassRecordDao
    {
        private QuantaDbContext _context;
        public PassRecordDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PassRecordEntity> AddAsync(PassRecordEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = IdUtil.NewId();
            }
            //配置基本信息
            entity.CreatedTime = entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Creator = entity.Editor = DataStaticInfo.CurrentUserId;

            //执行新增操作
            var result = await _context.PassRecords.AddAsync(entity);
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
            var entity = await _context.PassRecords.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return false;
            }

            //执行新增操作
            var result = _context.PassRecords.Remove(entity);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<PassRecordEntity> EditAsync(PassRecordEntity entity)
        {
            //配置基本信息
            entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Editor = DataStaticInfo.CurrentUserId;

            //执行新增操作
            var result = _context.PassRecords.Update(entity);
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public async Task<List<PassRecordEntity>> GetAllAsync()
        {
            var results = await _context.PassRecords.ToListAsync();
            return results;
        }

        public async Task<PassRecordEntity> GetByIdAsync(string id)
        {
            var result = await _context.PassRecords.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
