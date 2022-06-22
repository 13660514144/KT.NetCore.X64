using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using MySql.Data;
//using Newtonsoft.Json;
////using HelperTools;
//using ContralServer.CfgFileRead;
//using System.Data;
//using System.Text;
namespace KT.Quanta.Service.Daos
{
    public class PassRightDao : BaseDataDao<PassRightEntity>, IPassRightDao
    {
        private ILogger<PassRightDao> _logger;
        private QuantaDbContext _context;

        public PassRightDao(QuantaDbContext context,
            ILogger<PassRightDao> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PassRightEntity> AddAsync(PassRightEntity entity)
        {
            _logger.LogInformation($"新增权限：id:{entity.Id} sign:{entity.Sign} accessType:{entity.AccessType} rightType:{entity.RightType} ");
            var now = DateTimeUtil.UtcNowMillis();

            if (entity.RelationFloors != null && entity.RelationFloors.FirstOrDefault() != null)
            {
                foreach (var item in entity.RelationFloors)
                {
                    item.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == item.Floor.Id);
                    item.EditedTime = now;
                }
            }

            if (entity.Floor != null)
            {
                entity.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == entity.Floor.Id);
            }
            if (entity.Person != null)
            {
                entity.Person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == entity.Person.Id);
            }

            ////关联读卡器权限组
            //if (entity.RelationCardDeviceRightGroups != null && entity.RelationCardDeviceRightGroups.FirstOrDefault() != null)
            //{
            //    foreach (var item in entity.RelationCardDeviceRightGroups)
            //    {
            //        var id = item.CardDeviceRightGroup.Id;
            //        item.CardDeviceRightGroup = await _context.CardDeviceRightGroups
            //            .Include(x => x.RelationCardDevices)
            //            .ThenInclude(x => x.CardDevice)
            //            .ThenInclude(x => x.Processor)
            //            .FirstOrDefaultAsync(x => x.Id == id);

            //        if (item.CardDeviceRightGroup == null)
            //        {
            //            throw CustomException.Run("不存在的读卡器权限组：id:{0} ", id);
            //        }
            //        item.CardDeviceRightGroup.EditedTime = entity.EditedTime;
            //    }
            //}

            entity.EditedTime = now;                        
            
            await InsertAsync(entity, false);

            await _context.SaveChangesAsync();
            //_context.SaveChanges();
            return entity;
        }

        public async Task<List<PassRightEntity>> DeleteBySignAsync(string sign, string rightType)
        {
            try
            {
                var entities = await _context.PassRights.Where(x => x.Sign == sign && x.RightType == rightType).ToListAsync();

                if (entities?.FirstOrDefault() == null)
                {
                    return entities;
                }

                _context.PassRights.RemoveRange(entities);

                //await _context.SaveChangesAsync();
                _context.SaveChanges();
                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError($"删除权限错误：ex:{ex} ");
                return null;
            }
        }

        public async Task<PassRightEntity> EditAsync(PassRightEntity entity)
        {
            _logger.LogInformation($"修改权限：id:{entity.Id} sign:{entity.Sign} accessType:{entity.AccessType} rightType:{entity.RightType} ");
            var now = DateTimeUtil.UtcNowMillis();

            ////关联楼层
            //if (entity.RelationFloors != null && entity.RelationFloors.FirstOrDefault() != null)
            //{
            //    foreach (var item in entity.RelationFloors)
            //    {
            //        item.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == item.Floor.Id);
            //        item.EditedTime = now;
            //    }
            //}

            if (entity.Floor != null)
            {
                entity.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == entity.Floor.Id);
            }
            if (entity.Person != null)
            {
                entity.Person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == entity.Person.Id);
            }

            entity.EditedTime = now;


            await AttachAsync(entity, false);

            await _context.SaveChangesAsync();
            //_context.SaveChanges();
            return entity;
        }

        public async Task<List<PassRightEntity>> GetWithFloorsByRightTypeAsync(string rightType)
        {
            var entities = await _context.PassRights
                .Include(x => x.RelationFloors)
                .ThenInclude(x => x.Floor)
                .Where(x => x.RightType == rightType)
                .ToListAsync();
            return entities;
        }

        public async Task<PageData<PassRightEntity>> GetPageWithFloorsByRightTypeAsync(int page, int size, string rightType, string name, string sign)
        {
            var pageData = new PageData<PassRightEntity>();
            pageData.Page = page;
            pageData.Size = size;

            var query = _context.PassRights
                .Include(x => x.Floor)
                .Include(x => x.RelationFloors)
                .ThenInclude(x => x.Floor)
                .Where(x => x.RightType == rightType);
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Person.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(sign))
            {
                query = query.Where(x => x.Sign == sign);
            }

            var entities = await query.Skip((page - 1) * size)
               .Take(size)
               .ToListAsync();

            pageData.List = entities;

            return pageData;
        }

        public async Task<List<PassRightEntity>> GetByRightTypeAsync(string rightType)
        {
            var entities = await _context.PassRights
                .Where(x => x.RightType == rightType)
                .ToListAsync();
            return entities;
        }

        public async Task<PassRightEntity> GetWithFloorsByIdAsync(string id)
        {
            var entity = await _context.PassRights
                .Include(x => x.RelationFloors)
                .ThenInclude(x => x.Floor)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<PassRightEntity> GetWithFloorAndFloorsBySignAndAccessTypeAsync(string sign, string accessType, string rightType)
        {
            if (accessType == AccessTypeEnum.IC_CARD.Value || accessType == AccessTypeEnum.QR_CODE.Value)
            {
                var entity = await _context.PassRights
                   .Include(x => x.Floor)
                   .Include(x => x.RelationFloors)
                   .ThenInclude(x => x.Floor)
                   .Where(x => x.Sign == sign
                        && x.RightType == rightType
                        && (x.AccessType == AccessTypeEnum.IC_CARD.Value || x.AccessType == AccessTypeEnum.QR_CODE.Value))
                   .FirstOrDefaultAsync();

                return entity;
            }
            else
            {
                var entity = await _context.PassRights
                     .Include(x => x.Floor)
                     .Include(x => x.RelationFloors)
                     .ThenInclude(x => x.Floor)
                     .Where(x => x.Sign == sign && x.RightType == rightType && x.AccessType == accessType)
                     .FirstOrDefaultAsync();

                return entity;
            }
        }

        public async Task<PassRightEntity> GetWithRightGroupsBySignAndAccessTypeAsync(string sign, string accessType, string rightType)
        {
            if (accessType == AccessTypeEnum.IC_CARD.Value || accessType == AccessTypeEnum.QR_CODE.Value)
            {
                var entity = await _context.PassRights
                     .Include(x => x.RelationCardDeviceRightGroups)
                     .ThenInclude(x => x.CardDeviceRightGroup)
                   .Where(x => x.Sign == sign
                        && x.RightType == rightType
                        && (x.AccessType == AccessTypeEnum.IC_CARD.Value || x.AccessType == AccessTypeEnum.QR_CODE.Value))
                   .FirstOrDefaultAsync();

                return entity;
            }
            else
            {
                var entity = await _context.PassRights
                     .Include(x => x.RelationCardDeviceRightGroups)
                     .ThenInclude(x => x.CardDeviceRightGroup)
                     .Where(x => x.Sign == sign && x.RightType == rightType && x.AccessType == accessType)
                     .FirstOrDefaultAsync();

                return entity;
            }
        }

        public async Task<PassRightEntity> GetWithFloorsBySignAsync(string sign, string rightType)
        {
            var entity = await _context.PassRights
               .Include(x => x.RelationFloors)
               .ThenInclude(x => x.Floor)
               .FirstOrDefaultAsync(x => x.Sign == sign && x.RightType == rightType);
            return entity;
        }

        public async Task<List<PassRightEntity>> DeleteReturnPersonRightsBySignAsync(string sign, string rightType)
        {
            try
            {
                var entities = await _context.PassRights
                    .Include(x => x.Person.PassRights)
                    .Where(x => x.Sign == sign && x.RightType == rightType)
                    .ToListAsync();

                if (entities?.FirstOrDefault() == null)
                {
                    return entities;
                }

                _context.PassRights.RemoveRange(entities);

                //await _context.SaveChangesAsync();
                _context.SaveChanges();

                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError($"删除权限错误：ex:{ex} ");
                return null;
            }
        }

        public async Task<List<PassRightEntity>> GetPageWithFloorAsync(int page, int size, string rightType)
        {
            var results = await _context.PassRights
                .Include(x => x.RelationFloors)
                .ThenInclude(x => x.Floor)
                .Where(x => x.RightType == rightType)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return results;
        }

        public async Task RemoveAsync(PassRightEntity entity)
        {
            _context.PassRights.Remove(entity);

            await _context.SaveChangesAsync();
            //_context.SaveChanges();
        }


        public async Task<PassRightEntity> GetWithPersonRrightsBySignAsync(string sign)
        {
            var entity = await _context.PassRights
              .Include(x => x.Person.PassRights)
              .Include(x => x.RelationFloors)
              .ThenInclude(x => x.Floor)
              .Where(x => x.Sign == sign)
              .FirstOrDefaultAsync();

            return entity;
        }

        public async Task<List<PassRightEntity>> GetPageWithRightGroupAsync(int page, int size, string rightType)
        {
            var results = await _context.PassRights
                .Include(x => x.RelationCardDeviceRightGroups)
                .ThenInclude(x => x.CardDeviceRightGroup.RelationCardDevices)
                .ThenInclude(x => x.CardDevice)
                .ThenInclude(x => x.Processor)
                .Skip((page - 1) * size)
                .Take(size)
                .Where(x => x.RightType == rightType)
                .ToListAsync();
            return results;
        }

        public async Task<List<PassRightEntity>> GetBySignAsync(string sign, string rightType)
        {
            var entities = await _context.PassRights
             .Where(x => x.Sign == sign && x.RightType == rightType)
             .ToListAsync();

            return entities;
        }

        public async Task<List<CardDeviceRightGroupRelationCardDeviceEntity>> RightGroupRelationCardDevicesByGroupIdsAsync(List<string> cardDeviceRightGroupIds)
        {
            var entities = await _context.Set<CardDeviceRightGroupRelationCardDeviceEntity>()
                .Include(x => x.CardDevice)
                .Where(x => cardDeviceRightGroupIds.Contains(x.CardDeviceRightGroupId))
                .ToListAsync();

            return entities;
        }

        public async Task<PassRightEntity> GetWithPersonBySignAsync(string sign)
        {
            var entity = await _context.PassRights
                 .Include(x => x.Person)
                 .Where(x => x.Sign == sign)
                 .FirstOrDefaultAsync();

            return entity;
        }

        public async Task<List<PassRightEntity>> GetAllWithPersonBySignAsync(string sign)
        {
            var entity = await _context.PassRights
                 .Include(x => x.Person)
                 .Where(x => x.Sign == sign)
                 .ToListAsync();

            return entity;
        }

        public async Task<List<PassRightEntity>> GetWithPersonByLamdbalAsync(string personId, string rightType)
        {
            var entity = await _context.PassRights
                 .Include(x => x.Person)
                 .Where(x => x.Person.Id == personId && x.RightType == rightType)
                 .ToListAsync();

            return entity;
        }

        //public async Task<List<PassRightEntity>> GetWithCardDeviceByRightTypeAndDeviceIdsAsync(string rightType, List<string> deviceIds)
        //{
        //    var entities = await _context.PassRights
        //    .Include(x => x.RelationCardDeviceRightGroups)
        //    .ThenInclude(x => x.CardDeviceRightGroup)
        //    .ThenInclude(x => x.RelationCardDevices)
        //    .ThenInclude(x => x.CardDevice)
        //    .Where(x => x.RightType == rightType
        //            && x.RelationCardDeviceRightGroups.Any(z =>
        //                z.CardDeviceRightGroup.RelationCardDevices.Any(m =>
        //                        deviceIds.Contains(m.CardDeviceId)
        //                        || deviceIds.Contains(m.CardDevice.ProcessorId))))
        //    .ToListAsync();

        //    return entities;
        //}

        public async Task<bool> IsExistsByIdAndCardDeviceIdAsync(string id, string cardDeviceId)
        {
            var entity = await _context.PassRights
                .FirstOrDefaultAsync(x => x.Id == id
                    && x.RelationCardDeviceRightGroups.Any(z =>
                        z.CardDeviceRightGroup.RelationCardDevices.Any(m => m.CardDeviceId == cardDeviceId)));

            return entity != null;
        }

        public async Task<bool> IsExistsByIdAndProcessorIdAsync(string id, string processorId)
        {
            var entity = await _context.PassRights
                .FirstOrDefaultAsync(x => x.Id == id
                    && x.RelationCardDeviceRightGroups.Any(z =>
                        z.CardDeviceRightGroup.RelationCardDevices.Any(m => m.CardDevice.ProcessorId == processorId)));

            return entity != null;
        }
        public async Task<bool> IsExistsByIdAndCardDeviceIdExcludeRightGroupIdAsync(string id, string cardDeviceId, string excludeRightGroupId)
        {
            var entity = await _context.PassRights
                .FirstOrDefaultAsync(x => x.Id == id
                    && x.RelationCardDeviceRightGroups.Any(z =>
                        z.CardDeviceRightGroup.RelationCardDevices.Any(m => m.CardDeviceId == cardDeviceId))
                    && x.RelationCardDeviceRightGroups.Any(z =>
                        z.CardDeviceRightGroupId != excludeRightGroupId));

            return entity != null;
        }

        public async Task<bool> IsExistsByIdAndProcessorIdExcludeRightGroupIdAsync(string id, string processorId, string excludeRightGroupId)
        {
            var entity = await _context.PassRights
                .FirstOrDefaultAsync(x => x.Id == id
                    && x.RelationCardDeviceRightGroups.Any(z =>
                        z.CardDeviceRightGroup.RelationCardDevices.Any(m => m.CardDevice.ProcessorId == processorId))
                    && x.RelationCardDeviceRightGroups.Any(z =>
                        z.CardDeviceRightGroupId != excludeRightGroupId));

            return entity != null;
        }

        public Task<PassRightEntity> GetWithPersonByLambdaAsync(string passRightSign, string accessType)
        {
            var entity = _context.PassRights.
                Include(x => x.Person)
                .FirstOrDefaultAsync(x => x.Sign == passRightSign && x.AccessType == accessType);

            return entity;
        }
    }
}
