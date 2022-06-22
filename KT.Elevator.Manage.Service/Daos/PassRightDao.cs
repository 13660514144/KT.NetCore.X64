using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Daos
{
    public class PassRightDao : BaseDataDao<PassRightEntity>, IPassRightDao
    {
        private ILogger<PassRightDao> _logger;
        private ElevatorDbContext _context;

        public PassRightDao(ElevatorDbContext context,
            ILogger<PassRightDao> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PassRightEntity> AddAsync(PassRightEntity entity)
        {
            _logger.LogInformation($"新增权限：id:{entity.Id} sign:{entity.Sign} accessType:{entity.AccessType} ");
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

            entity.EditedTime = now;
            await InsertAsync(entity, false);

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<PassRightEntity>> DeleteBySignAsync(string sign)
        {
            try
            {
                var entities = await _context.PassRights.Where(x => x.Sign == sign).ToListAsync();

                if (entities?.FirstOrDefault() == null)
                {
                    return entities;
                }

                _context.PassRights.RemoveRange(entities);

                await _context.SaveChangesAsync();

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
            _logger.LogInformation($"修改权限：id:{entity.Id} sign:{entity.Sign} accessType:{entity.AccessType} ");
            var now = DateTimeUtil.UtcNowMillis();

            //关联楼层
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

            if (entity.Person != null)
            {
                entity.Person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == entity.Person.Id);
            }

            entity.EditedTime = now;
            await base.AttachUpdateAsync(entity, false);

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<PassRightEntity>> GetAllAsync()
        {
            var entities = await _context.PassRights
                .Include(x => x.RelationFloors)
                .ThenInclude(x => x.Floor)
                .ToListAsync();
            return entities;
        }


        public async Task<PassRightEntity> GetByIdAsync(string id)
        {
            var entity = await _context.PassRights
                .Include(x => x.RelationFloors)
                .ThenInclude(x => x.Floor)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<PassRightEntity> GetBySignAndAccessTypeAsync(string sign, string accessType)
        {
            if (accessType == CardTypeEnum.IC_CARD.Value || accessType == CardTypeEnum.QR_CODE.Value)
            {
                var entity = await _context.PassRights
                   .Include(x => x.Floor)
                   .Include(x => x.RelationFloors)
                   .ThenInclude(x => x.Floor)
                   .Where(x => x.Sign == sign
                        && (x.AccessType == CardTypeEnum.IC_CARD.Value || x.AccessType == CardTypeEnum.QR_CODE.Value))
                   .FirstOrDefaultAsync();

                return entity;
            }
            else
            {
                var entity = await _context.PassRights
                     .Include(x => x.Floor)
                     .Include(x => x.RelationFloors)
                     .ThenInclude(x => x.Floor)
                     .Where(x => x.Sign == sign && x.AccessType == accessType)
                     .FirstOrDefaultAsync();

                return entity;
            }
        }

        public async Task<PassRightEntity> GetWidthFloorsBySignAsync(string sign)
        {
            var entity = await _context.PassRights
               .Include(x => x.RelationFloors)
               .ThenInclude(x => x.Floor)
               .FirstOrDefaultAsync(x => x.Sign == sign);
            return entity;
        }

        public async Task<List<PassRightEntity>> DeleteReturnPersonRightsBySignAsync(string sign)
        {
            try
            {
                var entities = await _context.PassRights
                    .Include(x => x.Person.PassRights)
                    .Where(x => x.Sign == sign).ToListAsync();

                if (entities?.FirstOrDefault() == null)
                {
                    return entities;
                }

                _context.PassRights.RemoveRange(entities);

                await _context.SaveChangesAsync();

                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError($"删除权限错误：ex:{ex} ");
                return null;
            }
        }

        public async Task<List<PassRightEntity>> GetPageWithFloorAsync(int page, int size)
        {
            var results = await _context.PassRights
                .Include(x => x.RelationFloors)
                .ThenInclude(x => x.Floor)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return results;
        }

        public async Task RemoveAsync(PassRightEntity entity)
        {
            _context.PassRights.Remove(entity);

            await _context.SaveChangesAsync();
        }


        public async Task<PassRightEntity> GetWithPersonRrightsByIdAsync(string id)
        {
            var entity = await _context.PassRights
              .Include(x => x.Person.PassRights)
              .Include(x => x.RelationFloors)
              .ThenInclude(x => x.Floor)
              .Where(x => x.Sign == id)
              .FirstOrDefaultAsync();

            return entity;
        }
    }
}
