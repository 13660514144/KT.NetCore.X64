using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Processor.ClientApp.Device;
using KT.Elevator.Unit.Processor.ClientApp.Service.IServices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using KT.Elevator.Unit.Processor.ClientApp.Service.Helpers;
using KT.Quanta.Common.Enums;
using KT.Device.Unit.Events;
using Prism.Events;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Services
{
    /// <summary>
    /// 读卡器设备服务
    /// </summary>
    public class SerialDeviceService : ISerialDeviceService
    {
        //服务
        private ICardDeviceService _serialDeviceService;
        private ILogger _logger;
        private InputDeviceSettings _inputDeviceSettings;
        private ConfigHelper _configHelper;
        private readonly IEventAggregator _eventAggregator;

        public SerialDeviceService(
            ICardDeviceService serialDeviceService,
            ILogger logger,
            InputDeviceSettings inputDeviceSettings,
            ConfigHelper configHelper,
            IEventAggregator eventAggregator)
        {
            _serialDeviceService = serialDeviceService;
            _logger = logger;
            _inputDeviceSettings = inputDeviceSettings;
            _configHelper = configHelper;
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// 初始化所有读卡器设备
        /// </summary>
        public async Task InitAllCardDeviceAsync()
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("初始化所有读卡器设备失败：{0} ", ex);
            }
        }
    }
}
