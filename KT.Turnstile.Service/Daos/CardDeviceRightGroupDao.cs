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
    public class CardDeviceRightGroupDao : BaseDataDao<CardDeviceRightGroupEntity>, ICardDeviceRightGroupDao
    {
        private QuantaDbContext _context;
        public CardDeviceRightGroupDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CardDeviceRightGroupEntity> AddAsync(CardDeviceRightGroupEntity entity)
        {
            //配置基本信息
            entity.CreatedTime = entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Creator = entity.Editor = DataStaticInfo.CurrentUserId;

            //关联读卡器
            if (entity.RelationCardDevices != null && entity.RelationCardDevices.FirstOrDefault() != null)
            {
                foreach (var item in entity.RelationCardDevices)
                {
                    var id = item.CardDevice.Id;
                    item.CardDevice = await _context.CardDevices
                        .Include(x => x.Processor)
                        .Include(x => x.RelayDevice)
                        .Include(x => x.SerialConfig)
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (item.CardDevice == null)
                    {
                        throw CustomException.Run("不存在的读卡器：id:{0} ", id);
                    }
                    item.CardDevice.EditedTime = entity.EditedTime;
                }
            }

            //没有关联通道，直接保存实体类
            await InsertAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            //查询出要删除的数据
            var entity = await _context.CardDeviceRightGroups
                .Include(x => x.RelationCardDevices)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }

            //执行新增操作
            var result = _context.CardDeviceRightGroups.Remove(entity);

            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<CardDeviceRightGroupEntity> EditAsync(CardDeviceRightGroupEntity entity)
        {
            //配置基本信息
            entity.EditedTime = DateTimeUtil.UtcNowMillis();
            entity.Editor = DataStaticInfo.CurrentUserId;

            //关联读卡器
            if (entity.RelationCardDevices != null && entity.RelationCardDevices.FirstOrDefault() != null)
            {
                foreach (var item in entity.RelationCardDevices)
                {
                    var id = item.CardDevice.Id;
                    item.CardDevice = await _context.CardDevices
                        .Include(x => x.Processor)
                        .Include(x => x.RelayDevice)
                        .Include(x => x.SerialConfig)
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (item.CardDevice == null)
                    {
                        throw CustomException.Run("不存在的读卡器：id:{0} ", id);
                    }
                    item.CardDevice.EditedTime = entity.EditedTime;
                }
            }

            await AttachUpdateAsync(entity);

            return entity;
        }


        public async Task<CardDeviceRightGroupEntity> GetByIdAsync(string id)
        {
            var result = await _context.CardDeviceRightGroups
                .Include(x => x.RelationCardDevices)
                .ThenInclude(x => x.CardDevice.Processor)
                .Include(x => x.RelationCardDevices)
                .ThenInclude(x => x.CardDevice.RelayDevice)
                .Include(x => x.RelationCardDevices)
                .ThenInclude(x => x.CardDevice.SerialConfig)
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<CardDeviceRightGroupEntity> GetWidthCardDeviceByIdAsync(string id)
        {
            var result = await _context.CardDeviceRightGroups
                .Include(x => x.RelationCardDevices)
                .ThenInclude(x => x.CardDevice)
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<List<CardDeviceRightGroupEntity>> GetAllAsync()
        {
            var results = await _context.CardDeviceRightGroups
                 .Include(x => x.RelationCardDevices)
                 .ThenInclude(x => x.CardDevice.Processor)
                 .Include(x => x.RelationCardDevices)
                 .ThenInclude(x => x.CardDevice.RelayDevice)
                 .Include(x => x.RelationCardDevices)
                 .ThenInclude(x => x.CardDevice.SerialConfig)
                 .ToListAsync();
            return results;
        }

        public async Task<List<CardDeviceRightGroupEntity>> GetPageAsync(int page, int size)
        {
            var results = await _context.CardDeviceRightGroups
                .Include(x => x.RelationCardDevices)
                .ThenInclude(x => x.CardDevice.Processor)
                .Include(x => x.RelationCardDevices)
                .ThenInclude(x => x.CardDevice.RelayDevice)
                .Include(x => x.RelationCardDevices)
                .ThenInclude(x => x.CardDevice.SerialConfig)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            return results;
        }
    }
}
