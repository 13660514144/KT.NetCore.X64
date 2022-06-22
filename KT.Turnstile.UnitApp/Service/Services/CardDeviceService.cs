using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Unit.ClientApp.Dao.IDaos;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Events;
using KT.Device.Unit.Events;

namespace KT.Turnstile.Unit.ClientApp.Service.Services
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

        public async Task AddOrUpdateAsync(List<TurnstileUnitCardDeviceEntity> entities)
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

        public async Task AddOrUpdateAsync(TurnstileUnitCardDeviceEntity entity)
        {
            var oldEntity = await _dao.SelectByIdAsync(entity.Id);
            if (oldEntity != null)
            {
                if (oldEntity.EditedTime > entity.EditedTime)
                {
                    _logger.LogWarning($"数据未更改：oldEditedTime:{oldEntity.EditedTime} EditedTime:{entity.EditedTime} ");
                    return;
                }

                oldEntity.BrandModel = entity.BrandModel;
                oldEntity.DeviceType = entity.DeviceType;
                oldEntity.PortName = entity.PortName;
                oldEntity.Baudrate = entity.Baudrate;
                oldEntity.Databits = entity.Databits;
                oldEntity.Stopbits = entity.Stopbits;
                oldEntity.Parity = entity.Parity;
                oldEntity.ReadTimeout = entity.ReadTimeout;
                oldEntity.Encoding = entity.Encoding;
                oldEntity.RelayCommunicateType = entity.RelayCommunicateType;
                oldEntity.RelayDeviceIp = entity.RelayDeviceIp;
                oldEntity.RelayDevicePort = entity.RelayDevicePort;
                oldEntity.RelayDeviceOut = entity.RelayDeviceOut;
                oldEntity.RelayOpenCmd = entity.RelayOpenCmd;
                oldEntity.HandleElevatorDeviceId = entity.HandleElevatorDeviceId;

                await _dao.UpdateAsync(oldEntity);
            }
            else
            {
                oldEntity = entity;
                await _dao.InsertAsync(oldEntity);
            }

            //创建并打开串口 
            _eventAggregator.GetEvent<AddOrEditCardDeviceEvent>().Publish(oldEntity);
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

                //删除串口 
                _eventAggregator.GetEvent<DeleteCardDeviceEvent>().Publish(oldEntity);
            }
        }

        public async Task<List<TurnstileUnitCardDeviceEntity>> GetAllAsync()
        {
            return await _dao.SelectAllAsync();
        }
    }
}
