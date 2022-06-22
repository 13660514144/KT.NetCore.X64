using KT.Common.Core.Exceptions;
using KT.Device;
using KT.Device.Unit.Events;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.Services
{
    /// <summary>
    /// 读卡器设备服务
    /// </summary>
    public class SerialDeviceService : ISerialDeviceService
    {
        //服务
        private ICardDeviceService _serialDeviceService;
        private IDeviceList _deviceList;
        private ILogger _logger;
        private readonly IEventAggregator _eventAggregator;

        public SerialDeviceService(
            ICardDeviceService serialDeviceService,
            IDeviceList deviceList,
            ILogger logger,
            IEventAggregator eventAggregator)
        {
            _serialDeviceService = serialDeviceService;
            _deviceList = deviceList;
            _logger = logger;
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// 初始化所有读卡器设备
        /// </summary>
        public async void InitAllCardDeviceAsync()
        {
            //获取所有设备 
            var serialDevices = await _serialDeviceService.GetAllAsync();
            if (serialDevices == null || serialDevices.FirstOrDefault() == null)
            {
                throw CustomException.Run("系统中不存在读卡器设备信息！");
            }

            //创建并打开串口
            foreach (var item in serialDevices)
            {
                _eventAggregator.GetEvent<AddOrEditCardDeviceEvent>().Publish(item);
            }
        }
    }
}
