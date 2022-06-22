using KT.Common.Data.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Proxy.BackendApi.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Services
{
    public class FloorService : IFloorService
    {
        private IFloorDao _dao;
        private ILogger _logger;
        private IContainerProvider _containerProvider;
        private IMemoryCache _memoryCache;

        public FloorService(IFloorDao dao,
            ILogger logger,
            IContainerProvider containerProvider,
            IMemoryCache memoryCache)
        {
            _dao = dao;
            _logger = logger;
            _containerProvider = containerProvider;
            _memoryCache = memoryCache;
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

            //清除缓存
            await CleanCacheAsync();
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

            //清除缓存
            await CleanCacheAsync();
        }

        public async Task AddOrEditElevatorGroupFloorsAsync(UnitHandleElevatorDeviceModel model)
        {
            var oldFloors = await _dao.GetAllByElevatorGroupIdAsync(model.ElevatorGroupId);

            //删除旧数据
            if (oldFloors != null && oldFloors.FirstOrDefault() != null)
            {
                foreach (var item in oldFloors)
                {
                    await _dao.DeleteAsync(item, false);
                }
            }

            //新增新数据 
            if (model.Floors != null && model.Floors.FirstOrDefault() != null)
            {
                foreach (var item in model.Floors)
                {
                    item.ElevatorGroupId = model.ElevatorGroupId;
                    item.HandleElevatorDeviceId = model.Id;
                    await _dao.InsertAsync(item, false);
                }
            }

            //提交更改
            await _dao.SaveChangesAsync();

            //清除缓存
            await CleanCacheAsync();
        }

        public async Task<List<UnitFloorEntity>> GetAllByElevatorGroupIdAsync(string elevatorGroupId)
        {
            var key = $"{CacheKeys.ElevatorFloors}:{elevatorGroupId}";
            var result = await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                var floors = await _dao.GetAllByElevatorGroupIdAsync(elevatorGroupId);
                if (floors?.FirstOrDefault() == null)
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                }
                else
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(24 * 60 * 60);
                }
                entry.AddExpirationToken(new CancellationChangeToken(CacheKeys.ElevatorFloorsCancellationTokenSource.Token));
                return floors;
            });
            return result;
        }

        public async Task<PageData<UnitFloorEntity>> GetPageByElevatorGroupIdAsync(string elevatorGroupId, int page, int size)
        {
            var key = $"{CacheKeys.ElevatorFloors}:{elevatorGroupId}:{page}:{size}";
            var result = await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                var pagefloors = await _dao.GetPageByElevatorGroupIdAsync(elevatorGroupId, page, size);
                if (pagefloors?.List?.FirstOrDefault() == null)
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                }
                else
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(24 * 60 * 60);
                }
                entry.AddExpirationToken(new CancellationChangeToken(CacheKeys.ElevatorFloorsCancellationTokenSource.Token));
                return pagefloors;
            });
            return result;
        }

        public async Task DeleteElevatorGroupFloorsAsync(string elevatorGroupId)
        {
            var oldFloors = await _dao.GetAllByElevatorGroupIdAsync(elevatorGroupId);

            //删除旧数据
            if (oldFloors != null && oldFloors.FirstOrDefault() != null)
            {
                foreach (var item in oldFloors)
                {
                    await _dao.DeleteAsync(item, false);
                }
            }

            //提交更改
            await _dao.SaveChangesAsync();

            //清除缓存
            await CleanCacheAsync();
        }

        private async Task CleanCacheAsync()
        {
            CacheKeys.ElevatorFloorsCancellationTokenSource = await CacheKeys.ResetAsync(CacheKeys.ElevatorFloorsCancellationTokenSource);
        }
    }
}
