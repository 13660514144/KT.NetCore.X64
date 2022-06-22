using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Device.Unit.CardReaders.Models;
using KT.Device.Unit.Devices;
using KT.Device.Unit.Events;
using KT.Quanta.Common.Enums;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace KT.Device.Unit.Instances
{
    public class QscsBx5nCardDeviceInstance : IQscsBx5nCardDeviceInstance
    {
        public BrandModelEnum BrandModel => BrandModelEnum.QSCS_BX5N;

        private readonly IEventAggregator _eventAggregator;
        private readonly IDeviceList _deviceList;
        private readonly ILogger _logger;

        public QscsBx5nCardDeviceInstance()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _deviceList = ContainerHelper.Resolve<IDeviceList>();
            _logger = ContainerHelper.Resolve<ILogger>();
        }

        public Task StartAsync()
        {
            _eventAggregator.GetEvent<AddOrEditCardDeviceEvent>().Subscribe(AddOrEditCardDeviceAsync);
            _eventAggregator.GetEvent<DeleteCardDeviceEvent>().Subscribe(DeleteCardDeviceAsync);

            return Task.CompletedTask;
        }

        private async void AddOrEditCardDeviceAsync(object obj)
        {
            if (!(obj is ICardDeviceModel))
            {
                _logger.LogWarning($"设备数据类型异常：type:{ obj.GetType().FullName} need:{typeof(ICardDeviceModel).FullName} ");
                return;
            }

            var cardDevice = (ICardDeviceModel)obj;
            if (cardDevice.BrandModel != BrandModel.Value)
            {
                _logger.LogInformation($"设备型号不符：brandModel:{cardDevice.BrandModel} need:{BrandModel.Value} ");
                return;
            }

            _logger.LogInformation($"{cardDevice.PortName} 新增或修改读卡器设备：{JsonConvert.SerializeObject(obj, JsonUtil.JsonPrintSettings)} ");

            try
            {
                //查看是否已经存在
                var device = await _deviceList.RemoveAsync(cardDevice.PortName);
                if (device != null)
                {
                    //销毁设备重新创建
                    await ((ICardDeviceBase)device).DisposeAsync();
                }

                //创建并打开串口 
                var newDevice = ContainerHelper.Resolve<IQscsBx5nCardDevice>();
                await newDevice.StartAsync(obj);

                //向队列中添加串口
                await _deviceList.AddAsync(newDevice.SerialPort.PortName, newDevice);

                _logger.LogInformation($"{cardDevice.PortName} 新增或修改读卡器设备完成！");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{cardDevice.PortName} 新增或修改读卡器设备失败：{ex} ");
            }
        }
        private async void DeleteCardDeviceAsync(object obj)
        {
            if (!(obj is ICardDeviceModel))
            {
                _logger.LogWarning($"设备数据类型异常：type:{ obj.GetType().FullName} need:{typeof(ICardDeviceModel).FullName} ");
                return;
            }

            var cardDevice = (ICardDeviceModel)obj;
            if (cardDevice.BrandModel != BrandModel.Value)
            {
                _logger.LogInformation($"设备型号不符：brandModel:{cardDevice.BrandModel} need:{BrandModel.Value} ");
                return;
            }

            _logger.LogInformation("删除读卡器设备：{0} ", JsonConvert.SerializeObject(cardDevice, JsonUtil.JsonPrintSettings));
            try
            {
                //查看是否已经存在
                var device = await _deviceList.RemoveAsync(cardDevice.PortName);
                if (device != null)
                {
                    //销毁设备重新创建
                    await ((ICardDeviceBase)device).DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("删除读卡器失败：{0} ", ex);
            }
        }

    }
}
