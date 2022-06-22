using KT.Common.Core.Exceptions;
using KT.Device.Unit.Events;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Quanta.Common.Enums;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Services
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

        /// <summary>
        /// 初始化读卡器设备,二次派梯一体机
        /// </summary>
        public Task InitQrCodeDeviceAsync()
        {
            _logger.LogInformation($"\r\n初始化二维码读卡器");
            _logger.LogInformation($"start IP {_configHelper.LocalConfig.HandleElevatorDeviceId}");
            try
            {
                var serialDevices = new List<UnitCardDeviceEntity>();
                UnitCardDeviceEntity serialDevice;
                //初始化IC阅读器
                if (!string.IsNullOrEmpty(_inputDeviceSettings.IcCardDeviceSettings.Type))
                {
                    serialDevice = new UnitCardDeviceEntity();
                    serialDevice.Id = _configHelper.LocalConfig.HandleElevatorDeviceId;
                    serialDevice.DeviceType = CardDeviceTypeEnum.IC.Value;
                    serialDevice.BrandModel = _inputDeviceSettings.IcCardDeviceSettings.Type;
                    serialDevice.HandleDeviceId = _configHelper.LocalConfig.HandleElevatorDeviceId;
                    serialDevice = SetSerialConfig(serialDevice, _inputDeviceSettings.IcCardDeviceSettings.SerialSettings);
                    _logger.LogInformation($"\r\n初始化IC读卡器：{JsonConvert.SerializeObject(serialDevice)}");
                    serialDevices.Add(serialDevice);
                }

                //初始化二维码阅读器
                serialDevice = new UnitCardDeviceEntity();
                serialDevice.Id = _configHelper.LocalConfig.HandleElevatorDeviceId;
                serialDevice.DeviceType = CardDeviceTypeEnum.QR.Value;
                serialDevice.BrandModel = _inputDeviceSettings.QrCodeDeviceSettings.Type;
                serialDevice.HandleDeviceId = _configHelper.LocalConfig.HandleElevatorDeviceId;
                serialDevice = SetSerialConfig(serialDevice, _inputDeviceSettings.QrCodeDeviceSettings.SerialSettings);
                _logger.LogInformation($"\r\n初始化二维码读卡器：{JsonConvert.SerializeObject(serialDevice)}");
                serialDevices.Add(serialDevice);


                //创建并打开串口
                foreach (var item in serialDevices)
                {
                    _eventAggregator.GetEvent<AddOrEditCardDeviceEvent>().Publish(item);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("初始化读卡器设备失败：{0} ", ex);
            }

            return Task.CompletedTask;
        }

        private UnitCardDeviceEntity SetSerialConfig(UnitCardDeviceEntity serialDevice, SerialDeviceSettings serialDeviceSettings)
        {
            serialDevice.PortName = serialDeviceSettings.PortName;
            serialDevice.Baudrate = serialDeviceSettings.Baudrate;
            serialDevice.Databits = serialDeviceSettings.Databits;
            serialDevice.Stopbits = serialDeviceSettings.Stopbits;
            serialDevice.Parity = serialDeviceSettings.Parity;
            serialDevice.ReadTimeout = serialDeviceSettings.ReadTimeout;
            serialDevice.Encoding = serialDeviceSettings.Encoding;            
            return serialDevice;
        }
    }
}
