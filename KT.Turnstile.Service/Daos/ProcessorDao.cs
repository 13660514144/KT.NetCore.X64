using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Turnstile.Manage.Service.Base;
using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace KT.Turnstile.Manage.Service.Daos
{
    public class ProcessorDao : BaseDataDao<ProcessorEntity>, IProcessorDao
    {
        private QuantaDbContext _context;
        private ILogger<ProcessorDao> _logger;
        public ProcessorDao(QuantaDbContext context,
            ILogger<ProcessorDao> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ProcessorEntity> AddAsync(ProcessorEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = IdUtil.NewId();
            }
            //配置基本信息
            entity.CreatedTime = entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Creator = entity.Editor = DataStaticInfo.CurrentUserId;

            //ip与端口不能相同
            var ipEntity = await _context.Processors.FirstOrDefaultAsync(x => x.Port == entity.Port && x.IpAddress == entity.IpAddress);

            if (ipEntity != null)
            {
                await DeleteProcessor(ipEntity);
            }

            //执行新增操作
            var result = await _context.Processors.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        private async Task DeleteProcessor(ProcessorEntity oldEntity)
        {
            _logger.LogWarning("系统中已存在相同的IP与端口的边缘处理器：ip:{0} port:{1} ", oldEntity.IpAddress, oldEntity.Port);
            var distributeErrors = await _context.DistributeErrors.Where(x => x.Processor.Id == oldEntity.Id).ToListAsync();
            if (distributeErrors != null && distributeErrors.FirstOrDefault() != null)
            {
                _context.DistributeErrors.RemoveRange(distributeErrors);
            }
            var cardDevices = await _context.CardDevices.Where(x => x.Processor.Id == oldEntity.Id).ToListAsync();
            if (cardDevices != null && cardDevices.FirstOrDefault() != null)
            {
                foreach (var item in cardDevices)
                {
                    var cardDeviceRightGroups = await _context.CardDeviceRightGroups
                        .Include(x => x.RelationCardDevices)
                        .Where(x => x.RelationCardDevices.Any(y => y.CardDevice.Id == item.Id))
                        .ToListAsync();

                    if (cardDeviceRightGroups != null && cardDeviceRightGroups.FirstOrDefault() != null)
                    {
                        foreach (var obj in cardDeviceRightGroups)
                        {
                            var passRights = await _context.PassRights
                                .Include(x => x.RelationCardDeviceRightGroups)
                                .Where(x => x.RelationCardDeviceRightGroups.Any(y => y.CardDeviceRightGroup.Id == obj.Id))
                                .ToListAsync();

                            if (passRights != null && passRights.FirstOrDefault() != null)
                            {
                                _context.PassRights.RemoveRange(passRights);
                            }

                            _context.CardDeviceRightGroups.Remove(obj);
                        }
                    }
                    _context.CardDevices.Remove(item);
                }
            }
            _context.Processors.Remove(oldEntity);
            _logger.LogWarning("已删除系统中存在的相同的IP与端口的边缘处理器：ip:{0} port:{1} ", oldEntity.IpAddress, oldEntity.Port);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            //查询出要删除的数据
            var entity = await _context.Processors.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return false;
            }

            //执行新增操作
            await DeleteProcessor(entity);

            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<ProcessorEntity> EditAsync(ProcessorEntity entity)
        {
            //配置基本信息
            entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Editor = DataStaticInfo.CurrentUserId;

            //修改更新时间 
            entity.EditedTime = DateTimeUtil.UtcNowMillis();

            //ip与端口不能相同
            var ipEntity = await _context.Processors.FirstOrDefaultAsync(x => x.Port == entity.Port && x.IpAddress == entity.IpAddress && x.Id != entity.Id);

            if (ipEntity != null)
            {
                //throw CustomException.Run("系统中已存在相同的IP与端口的边缘处理器：ip:{0} port:{1} ", entity.IpAddress, entity.Port);
                await DeleteProcessor(ipEntity);
            }

            //执行新增操作
            var result = _context.Processors.Update(entity);
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return result.Entity;
        }

        public async Task<List<ProcessorEntity>> GetAllAsync()
        {
            var results = await _context.Processors.ToListAsync();
            return results;
        }

        public async Task<ProcessorEntity> GetByIdAsync(string id)
        {
            var result = await _context.Processors.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}