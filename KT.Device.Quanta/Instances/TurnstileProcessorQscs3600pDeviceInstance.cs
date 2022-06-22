using KT.Common.Event;
using KT.Device.Quanta.Devices;
using KT.Device.Quanta.Events;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Device.Quanta.Instances
{
    public class TurnstileProcessorQscs3600pDeviceInstance : ITurnstileProcessorQscs3600pDeviceInstance
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<TurnstileProcessorQscs3600pDeviceInstance> _logger;
        private readonly IDeviceList _deviceList;

        public TurnstileProcessorQscs3600pDeviceInstance(IEventAggregator eventAggregator,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<TurnstileProcessorQscs3600pDeviceInstance> logger,
            IDeviceList deviceList)
        {
            _eventAggregator = eventAggregator;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _deviceList = deviceList;
        }

        public Task StartAsync()
        {
            _eventAggregator.GetEvent<CreateTurnstileProcessorByIdEvent>().Subscribe(CreateTurnstileProcessorByIdAsync);
            _eventAggregator.GetEvent<CreateTurnstileProcessorEvent>().Subscribe(CreateTurnstileProcessorAsync);

            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            //数据库中查找数据
            using var scope = _serviceScopeFactory.CreateScope();
            var dao = scope.ServiceProvider.GetRequiredService<IProcessorDao>();
            var entities = await dao.GetWidthCardDevicesAsync();

            if (entities?.FirstOrDefault() == null)
            {
                return;
            }

            foreach (var item in entities)
            {
                CreateTurnstileProcessorAsync(item);
            }
        }

        private async void CreateTurnstileProcessorByIdAsync(string id)
        {
            //数据库中查找数据
            using var scope = _serviceScopeFactory.CreateScope();
            var dao = scope.ServiceProvider.GetRequiredService<IProcessorDao>();
            var entity = await dao.GetWidthCardDevicesByIdAsync(id);

            //较验数据
            if (entity == null)
            {
                _logger.LogError($"不存在的闸机边缘处理器：id:{id} ");
                return;
            }
            if (entity.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR.Value
                && entity.BrandModel == BrandModelEnum.QSCS_3600P.Value)
            {
                CreateTurnstileProcessorAsync(entity);
            }
            else
            {
                _logger.LogError($"闸机边缘处理器类型不正确：id:{id} ");
                return;
            }
        }

        private async void CreateTurnstileProcessorAsync(ProcessorEntity entity)
        {
            //数据库中查找数据
            if (entity.CardDevices?.FirstOrDefault() == null)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var dao = scope.ServiceProvider.GetRequiredService<ICardDeviceDao>();
                entity.CardDevices = await dao.GetByProcessorIdAsync(entity.Id);
            }

            //修改旧程序
            var device = await _deviceList.GetAsync(entity.Id);

            if (device != null)
            {
                if (device is ITurnstileProcessorQscs3600pDevice oldDevice)
                {
                    await oldDevice.UpdateAsync(entity);
                }
                else
                {
                    //删除并销毁
                    device = await _deviceList.RemoveAsync(entity.Id);
                    if (device is IAsyncDisposable disposableDevice)
                    {
                        await disposableDevice.DisposeAsync();
                    }

                    //创建设备
                    await CreateDeviceAsync(entity);
                }
            }
            else
            {
                //创建设备
                await CreateDeviceAsync(entity);
            }
        }

        /// <summary>
        /// 创建设备
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task CreateDeviceAsync(ProcessorEntity entity)
        {
            var socpe = _serviceScopeFactory.CreateScope();
            var newDevice = socpe.ServiceProvider.GetRequiredService<ITurnstileProcessorQscs3600pDevice>();
            await newDevice.CreateAsync(entity);
        }
    }
}
