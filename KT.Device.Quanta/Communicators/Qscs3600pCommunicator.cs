using KT.Common.Event;
using KT.Common.Netty.Common;
using KT.Device.Quanta.Clients;
using KT.Device.Quanta.Events;
using KT.Device.Quanta.Models;
using KT.Quanta.Common.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Device.Quanta.Communicators
{
    public class Qscs3600pCommunicator : IQscs3600pCommunicator
    {
        private string _communicatorKey;

        private IEventAggregator _eventAggregator;
        private IQuantaClientHost _quantaClientHost;
        private IDeviceList _deviceList;
        private readonly ILogger<Qscs3600pCommunicator> _logger;

        public Qscs3600pCommunicator(IEventAggregator eventAggregator,
            IQuantaClientHost quantaClientHost,
            IDeviceList deviceList,
            ILogger<Qscs3600pCommunicator> logger)
        {
            _eventAggregator = eventAggregator;
            _quantaClientHost = quantaClientHost;
            _deviceList = deviceList;
            _logger = logger;
        }

        public async Task CareateAsync(string remoteIp, int remotePort)
        {
            _logger.LogInformation($"创建边缘处理器Netty连接：{remoteIp}:{remotePort} ");

            //本地参数
            //TODO:端口加1
            _communicatorKey = $"{remoteIp}:{remotePort + 1}";

            //监听事件
            _eventAggregator.GetEvent<PassDisplaySendEvent>().Subscribe(PassShowSendAsync);
            _eventAggregator.GetEvent<HandleElevatorDisplaySendEvent>().Subscribe(HandledElevatorDisplaySendAsync);

            //连接服务
            //TODO:端口加1
            await _quantaClientHost.InitAsync(remoteIp, remotePort + 1);

            //向队列中加入设备
            await _deviceList.AddAsync(_communicatorKey, this);
        }

        public Task UpdateAsync(string remoteIp, int remotePort)
        {
            return Task.CompletedTask;
        }

        private void PassShowSendAsync(QuantaSendDataModel data)
        {
            if (data.CommunicatorKey != _communicatorKey)
            {
                return;
            }

            _quantaClientHost.SendAsync(data.QuantaNettyHeader);
        }

        private void HandledElevatorDisplaySendAsync(QuantaSendDataModel data)
        {
            if (data.CommunicatorKey != _communicatorKey)
            {
                return;
            }

            _quantaClientHost.SendAsync(data.QuantaNettyHeader);
        }

        public async ValueTask DisposeAsync()
        {
            if (_quantaClientHost != null)
            {
                await _quantaClientHost.CloseAsync();
            }
        }
    }
}
