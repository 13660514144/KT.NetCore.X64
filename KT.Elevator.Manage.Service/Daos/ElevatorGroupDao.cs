using KT.Common.Data.Daos;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Daos
{
    public class ElevatorGroupDao : BaseDataDao<ElevatorGroupEntity>, IElevatorGroupDao
    {
        private ElevatorDbContext _context;
        public ElevatorGroupDao(ElevatorDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ElevatorGroupEntity> AddAsync(ElevatorGroupEntity entity)
        {
            if (entity.Edifice != null)
            {
                entity.Edifice = await _context.Edifices.FirstOrDefaultAsync(x => x.Id == entity.Edifice.Id);
            }
            if (entity.RelationFloors != null && entity.RelationFloors.FirstOrDefault() != null)
            {
                foreach (var item in entity.RelationFloors)
                {
                    item.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == item.Floor.Id);
                    //await InsertRelevanceAsync(item, false);
                }
            }
            await InsertAsync(entity, false);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ElevatorGroupEntity> EditAsync(ElevatorGroupEntity entity)
        {
            if (entity.Edifice != null)
            {
                entity.Edifice = await _context.Edifices.FirstOrDefaultAsync(x => x.Id == entity.Edifice.Id);
            }
            //先加入数据，否则删除时会把数据加入当前列表
            if (entity.RelationFloors != null && entity.RelationFloors.FirstOrDefault() != null)
            {
                foreach (var item in entity.RelationFloors)
                {
                    item.Floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == item.Floor.Id);
                    item.ElevatorGroup = entity;
                    //await InsertRelevanceAsync(item, false);
                }
            }
            //清理旧数据
            var oldRelationFloors = _context.ElevatorGroupRelationFloors.Where(x => x.ElevatorGroup.Id == entity.Id);
            if (oldRelationFloors != null)
            {
                foreach (var item in oldRelationFloors)
                {
                    _context.ElevatorGroupRelationFloors.Remove(item);
                }
            }
            await AttachUpdateAsync(entity, false);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<ElevatorGroupEntity>> GetAllAsync()
        {
            var entities = await _context.ElevatorGroups
                .Include(x => x.Edifice)
                .Include(x => x.RelationFloors)
                .ThenInclude(x => x.Floor)
                .ToListAsync();
            return entities;
        }

        public async Task<List<ElevatorGroupEntity>> GetAllWithElevatorInfosAsync()
        {
            var entities = await _context.ElevatorGroups
                .Include(x => x.ElevatorInfos)
                .ToListAsync();
            return entities;
        }

        public async Task<ElevatorGroupEntity> GetByIdAsync(string id)
        {
            var entity = await _context.ElevatorGroups
                .Include(x => x.Edifice)
                .Include(x => x.RelationFloors)
                .ThenInclude(x => x.Floor)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<ElevatorGroupEntity> GetWithElevatorInfosByIdAsync(string id)
        {
            var entities = await _context.ElevatorGroups
                 .Include(x => x.ElevatorInfos)
                 .FirstOrDefaultAsync(x => x.Id == id);
            return entities;
        }
    }
}
