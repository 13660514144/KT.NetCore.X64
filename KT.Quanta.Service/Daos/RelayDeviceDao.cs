using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Quanta.Service.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class RelayDeviceDao : BaseDataDao<RelayDeviceEntity>, IRelayDeviceDao
    {
        private QuantaDbContext _context;
        public RelayDeviceDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RelayDeviceEntity> AddAsync(RelayDeviceEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = IdUtil.NewId();
            }

            //配置基本信息
            entity.CreatedTime = entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Creator = entity.Editor = DataStaticInfo.CurrentUserId;

            //执行新增操作
            var result = await _context.RelayDevices.AddAsync(entity);
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
            var entity = await _context.RelayDevices.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return false;
            }

            //执行新增操作
            var result = _context.RelayDevices.Remove(entity);
            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<RelayDeviceEntity> EditAsync(RelayDeviceEntity entity)
        {
            //配置基本信息
            entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Editor = DataStaticInfo.CurrentUserId;

            //执行新增操作
            var result = _context.RelayDevices.Update(entity);
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public async Task<List<RelayDeviceEntity>> GetAllAsync()
        {
            var results = await _context.RelayDevices.ToListAsync();
            return results;
        }

        public async Task<RelayDeviceEntity> GetByIdAsync(string id)
        {
            var result = await _context.RelayDevices.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
