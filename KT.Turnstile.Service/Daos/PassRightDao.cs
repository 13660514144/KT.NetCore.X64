using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Turnstile.Manage.Service.Base;
using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Entity;
using KT.Turnstile.Entity.Entities;
using KT.Turnstile.Common.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Daos
{
    public class PassRightDao : BaseDataDao<PassRightEntity>, IPassRightDao
    {
        private QuantaDbContext _context;
        public PassRightDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PassRightEntity> AddAsync(PassRightEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = IdUtil.NewId();
            }
            //配置基本信息
            entity.CreatedTime = entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Creator = entity.Editor = DataStaticInfo.CurrentUserId;

            //关联读卡器权限组
            if (entity.RelationCardDeviceRightGroups != null && entity.RelationCardDeviceRightGroups.FirstOrDefault() != null)
            {
                foreach (var item in entity.RelationCardDeviceRightGroups)
                {
                    var id = item.CardDeviceRightGroup.Id;
                    item.CardDeviceRightGroup = await _context.CardDeviceRightGroups
                        .Include(x => x.RelationCardDevices)
                        .ThenInclude(x => x.CardDevice)
                        .ThenInclude(x => x.Processor)
                        .FirstOrDefaultAsync(x => x.Id == id);
                    if (item.CardDeviceRightGroup == null)
                    {
                        throw CustomException.Run("不存在的读卡器权限组：id:{0} ", id);
                    }
                    item.CardDeviceRightGroup.EditedTime = entity.EditedTime;
                }
            }

            //没有关联通道，直接保存实体类
            _context.PassRights.Add(entity);

            //执行新增操作  
            var rows = await _context.SaveChangesAsync();
            if (rows <= 0)
            {
                return null;
            }
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            //查询出要删除的数据
            var entity = await _context.PassRights.Include(x => x.RelationCardDeviceRightGroups).FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return false;
            }

            //执行新增操作
            var result = _context.PassRights.Remove(entity);

            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<PassRightEntity> EditAsync(PassRightEntity entity)
        {
            //配置基本信息
            entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Editor = DataStaticInfo.CurrentUserId;

            //关联读卡器权限组
            if (entity.RelationCardDeviceRightGroups != null && entity.RelationCardDeviceRightGroups.FirstOrDefault() != null)
            {
                foreach (var item in entity.RelationCardDeviceRightGroups)
                {
                    var id = item.CardDeviceRightGroup.Id;
                    item.CardDeviceRightGroup = await _context.CardDeviceRightGroups
                        .Include(x => x.RelationCardDevices)
                        .ThenInclude(x => x.CardDevice)
                        .ThenInclude(x => x.Processor)
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (item.CardDeviceRightGroup == null)
                    {
                        throw CustomException.Run("不存在的读卡器权限组：id:{0} ", id);
                    }
                    item.CardDeviceRightGroup.EditedTime = entity.EditedTime;
                }
            }

            //执行新增操作
            await AttachUpdateAsync(entity);

            return entity;
        }

        public async Task<List<PassRightEntity>> GetAllAsync()
        {
            var results = await _context.PassRights
                .Include(x => x.RelationCardDeviceRightGroups)
                .ThenInclude(x => x.CardDeviceRightGroup.RelationCardDevices)
                .ThenInclude(x => x.CardDevice)
                .ThenInclude(x => x.Processor)
                .ToListAsync();
            return results;
        }

        public async Task<List<PassRightEntity>> GetPageAsync(int page, int size)
        {
            var results = await _context.PassRights
                .Include(x => x.RelationCardDeviceRightGroups)
                .ThenInclude(x => x.CardDeviceRightGroup.RelationCardDevices)
                .ThenInclude(x => x.CardDevice)
                .ThenInclude(x => x.Processor)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            return results;
        }

        public async Task<PassRightEntity> GetByIdAsync(string id)
        {
            var result = await _context.PassRights
                .Include(x => x.RelationCardDeviceRightGroups)
                .ThenInclude(x => x.CardDeviceRightGroup.RelationCardDevices)
                .ThenInclude(x => x.CardDevice)
                .ThenInclude(x => x.Processor)
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<PassRightEntity>> GetByProcessorIdAsync(string processorId)
        {
            var result = await _context.PassRights
                 .Include(x => x.RelationCardDeviceRightGroups)
                 .ThenInclude(x => x.CardDeviceRightGroup.RelationCardDevices)
                 .ThenInclude(x => x.CardDevice)
                 .ThenInclude(x => x.Processor)
                 .Where(x => x.RelationCardDeviceRightGroups.
                    FirstOrDefault(x => x.CardDeviceRightGroup.RelationCardDevices.
                        FirstOrDefault(x => x.CardDevice.Processor.Id == processorId) != null) != null)
                 .ToListAsync();
            return result;
        }

        public async Task<List<PassRightEntity>> GetByRightGroupId(string id)
        {
            var results = await _context.PassRights
                .Include(x => x.RelationCardDeviceRightGroups
                    .Where(x => x.CardDeviceRightGroup.Id == id))
                .ThenInclude(x => x.CardDeviceRightGroup.RelationCardDevices)
                .ThenInclude(x => x.CardDevice)
                .ThenInclude(x => x.Processor)
                .Where(x => x.RelationCardDeviceRightGroups.FirstOrDefault() != null)
                .ToListAsync();

            return results;
        }

        public async Task<List<PassRightEntity>> GetWithCardDeviceWidthProcessorByRightGroupId(string id)
        {
            var results = await _context.PassRights
                .Include(x => x.RelationCardDeviceRightGroups
                    .Where(x => x.CardDeviceRightGroup.Id == id))
                .ThenInclude(x => x.CardDeviceRightGroup.RelationCardDevices)
                .ThenInclude(x => x.CardDevice)
                .ThenInclude(x => x.Processor)
                .Where(x => x.RelationCardDeviceRightGroups.FirstOrDefault() != null)
                .ToListAsync();

            return results;
        }

        public async Task<List<PassRightEntity>> GetHasRightListAsync(string cardNumber, long passTime)
        {
            var results = await _context.PassRights
                .Include(x => x.RelationCardDeviceRightGroups)
                .ThenInclude(x => x.CardDeviceRightGroup.RelationCardDevices)
                .ThenInclude(x => x.CardDevice)
                .ThenInclude(x => x.Processor)
                .Where(x => x.CardNumber == cardNumber
                    && x.TimeOut > passTime)
                .ToListAsync();

            return results;
        }

        public async Task<PassRightEntity> GetLastAsync(string cardNumber)
        {
            var results = await _context.PassRights
                .Include(x => x.RelationCardDeviceRightGroups)
                .ThenInclude(x => x.CardDeviceRightGroup.RelationCardDevices)
                .ThenInclude(x => x.CardDevice)
                .ThenInclude(x => x.Processor)
                .Where(x => x.CardNumber == cardNumber)
                .OrderByDescending(x => x.CreatedTime)
                .FirstOrDefaultAsync();
            return results;
        }
    }
}
