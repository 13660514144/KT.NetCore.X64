using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KT.Device.Unit.Events;
using Prism.Events;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Services
{
    public class CardDeviceService : ICardDeviceService
    {
        private ICardDeviceDao _dao;
        private ILogger _logger;
        private IContainerProvider _containerProvider;
        private readonly IEventAggregator _eventAggregator;

        public CardDeviceService(ICardDeviceDao dao,
            ILogger logger,
            IContainerProvider containerProvider,
            IEventAggregator eventAggregator)
        {
            _dao = dao;
            _logger = logger;
            _containerProvider = containerProvider;
            _eventAggregator = eventAggregator;
        }

        public async Task AddOrUpdateAsync(List<UnitCardDeviceEntity> entities)
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
                    _logger.LogError("新增或修改权限组错误：ex:{0} ", ex);
                }
            }
        }

        public async Task AddOrUpdateAsync(UnitCardDeviceEntity entity)
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

            //创建并打开串口 
            _eventAggregator.GetEvent<AddOrEditCardDeviceEvent>().Publish(oldEntity);
        }

        public async Task Delete(string id, long editTime)
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

                //删除串口 
                _eventAggregator.GetEvent<DeleteCardDeviceEvent>().Publish(oldEntity);
            }
        }

        public async Task<List<UnitCardDeviceEntity>> GetAllAsync()
        {
            return await _dao.SelectAllAsync();
        }
    }
}
