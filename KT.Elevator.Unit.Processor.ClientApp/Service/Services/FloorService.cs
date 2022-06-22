using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Processor.ClientApp.Service.IServices;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KT.Common.Data.Models;
using System.Linq;
using KT.Elevator.Unit.Entity.Models;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Services
{
    public class FloorService : IFloorService
    {
        private IFloorDao _dao;
        private ILogger _logger;
        private IContainerProvider _containerProvider;
        private readonly IElevatorGroupDao _elevatorGroupDao ;

        public FloorService(IFloorDao dao,
            ILogger logger,
            IContainerProvider containerProvider,
            IElevatorGroupDao elevatorGroupDao)
        {
            _dao = dao;
            _logger = logger;
            _containerProvider = containerProvider;
            _elevatorGroupDao = elevatorGroupDao;
        }

        public async Task AddOrUpdateAsync(List<UnitFloorEntity> entities)
        {
            if (entities == null)
            {
                return;
            }
            foreach (var entity in entities)
            {
                try
                {
                    await AddOrUpdateAsync(entity);
                }
                catch (Exception ex)
                {
                    _logger.LogError("新增或修改楼层错误：ex:{0} ", ex);
                }
            }
        }

        public async Task AddOrUpdateAsync(UnitFloorEntity entity)
        {
            var oldEntity = await _dao.SelectByIdAsync(entity.Id);
            if (oldEntity != null)
            {
                if (oldEntity.EditedTime > entity.EditedTime)
                {
                    _logger.LogWarning($"数据未更改：oldEditedTime:{oldEntity.EditedTime} EditedTime:{entity.EditedTime} ");
                    return;
                }
                await _dao.DeleteAsync(oldEntity);
            }
            await _dao.InsertAsync(entity);
        }

        public async Task DeleteAsync(string id, long editTime)
        {
            var oldEntity = await _dao.SelectByIdAsync(id);
            if (oldEntity != null)
            {
                if (oldEntity.EditedTime > editTime)
                {
                    _logger.LogWarning($"数据未更改：oldEditedTime:{oldEntity.EditedTime} EditedTime:{editTime} ");
                    return;
                }
                await _dao.DeleteAsync(oldEntity);
            }
        }
         

        public async Task<List<UnitFloorEntity>> GetAllByElevatorGroupIdAsync(string elevatorGroupId)
        {
            var elevatorGroup  = await _elevatorGroupDao.GetWithRelevanceFloorsByIdAsync(elevatorGroupId);
            if(elevatorGroup?.ElevatorGroupFloors?.FirstOrDefault() == null)
            {
                return new List<UnitFloorEntity>();
            }
            var floorIds = elevatorGroup.ElevatorGroupFloors.Select(x => x.FloorId).ToList();
            return await _dao.GetByIdsAsync(floorIds);
        }

        public async Task<List<UnitFloorEntity>> GetAllByHandleElevatorDeviceIdAsync(string handleElevatorDeviceId)
        {
            var handleElevatorDevice = await _dao.SelectRelevanceObjectAsync<UnitHandleElevatorDeviceEntity>(handleElevatorDeviceId);
            if (handleElevatorDevice == null)
            {
                return new List<UnitFloorEntity>();
            }
            var elevatorGroup = await _elevatorGroupDao.GetWithRelevanceFloorsByIdAsync(handleElevatorDevice.ElevatorGroupId);
            if (elevatorGroup?.ElevatorGroupFloors?.FirstOrDefault() == null)
            {
                return new List<UnitFloorEntity>();
            }
            var floorIds = elevatorGroup.ElevatorGroupFloors.Select(x => x.FloorId).ToList();
            return await _dao.GetByIdsAsync(floorIds);
        }

        public async Task<PageData<UnitFloorEntity>> GetPageByElevatorGroupIdAsync(string elevatorGroupId, int page, int size)
        {
            return await _dao.GetPageByElevatorGroupIdAsync(elevatorGroupId, page, size);
        }

        public async Task DeleteElevatorGroupFloorsAsync(string elevatorGroupId)
        {
            var elevatorGroup = await _elevatorGroupDao.GetWithRelevanceFloorsByIdAsync(elevatorGroupId);
            if (elevatorGroup?.ElevatorGroupFloors?.FirstOrDefault() == null)
            {
                return  ;
            }
            var floorIds = elevatorGroup.ElevatorGroupFloors.Select(x => x.FloorId).ToList();
         
            //删除旧数据
            if (!string.IsNullOrEmpty( floorIds?.FirstOrDefault()) )
            {
                foreach (var item in floorIds)
                {
                    await _dao.DeleteByIdAsync(item, false);
                }
            }

            //提交更改
            await _dao.SaveChangesAsync();
        }
    }
}
