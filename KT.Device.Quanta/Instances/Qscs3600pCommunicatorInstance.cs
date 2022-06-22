using KT.Common.Event;
using KT.Device.Quanta.Communicators;
using KT.Device.Quanta.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KT.Device.Quanta.Instances
{
    public class Qscs3600pCommunicatorInstance : IQscs3600pCommunicatorInstance
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<Qscs3600pCommunicatorInstance> _logger;
        private readonly IDeviceList _deviceList;

        public Qscs3600pCommunicatorInstance(IEventAggregator eventAggregator,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<Qscs3600pCommunicatorInstance> logger,
            IDeviceList deviceList)
        {
            _eventAggregator = eventAggregator;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _deviceList = deviceList;
        }

        public Task StartAsync()
        {
            _eventAggregator.GetEvent<CreateQscs3600pCommunicatorEvent>().Subscribe(CreateQscs3600pCommunicatorAsync);

            return Task.CompletedTask;
        }

        private async void CreateQscs3600pCommunicatorAsync(CareateQscs3600pCommunicatorModel model)
        {
            //TODO:端口加1
            var communicatorKey = $"{model.RemoteIp}:{model.RemotePort + 1}";

            //选销毁旧程序
            var device = await _deviceList.GetAsync(communicatorKey);
            if (device != null)
            {
                if (device is IQscs3600pCommunicator oldDevice)
                {
                    await oldDevice.UpdateAsync(model.RemoteIp, model.RemotePort);
                }
                else
                {
                    //删除并销毁
                    device = await _deviceList.RemoveAsync(communicatorKey);
                    if (device is IAsyncDisposable disposableDevice)
                    {
                        await disposableDevice.DisposeAsync();
                    }

                    //创建新程序 
                    await CreateDeviceAsync(model);
                }
            }
            else
            {
                //创建新程序 
                await CreateDeviceAsync(model);
            }
        }

        /// <summary>
        /// 创建设备
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task CreateDeviceAsync(CareateQscs3600pCommunicatorModel model)
        {
            //创建设备
            var socpe = _serviceScopeFactory.CreateScope();
            var newDevice = socpe.ServiceProvider.GetRequiredService<IQscs3600pCommunicator>();
            await newDevice.CareateAsync(model.RemoteIp, model.RemotePort);
        }
    }
}
