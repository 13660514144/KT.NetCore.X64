using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Turnstile.Unit.ClientApp.Device.Devices;
using KT.Turnstile.Unit.ClientApp.Device.IDevices;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Ioc;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Device.Common
{
    /// <summary>
    /// 设备工厂
    /// 根据数据库存储的设备信息实例化设备操作原型
    /// </summary>
    public class SocketDeviceFactory
    {
        private ILogger _logger;
        private IContainerProvider _containerProvider;

        public SocketDeviceFactory(ILogger logger,
            IContainerProvider containerProvider)
        {
            _logger = logger;
            _containerProvider = containerProvider;
        }

        /// <summary>
        /// 根据设备信息创建对应设备操作
        /// </summary>
        /// <param name="socketDevice">设备信息</param>
        /// <returns>串口</returns>
        public async Task<ISocketDevice> CreateAsync(TurnstileUnitCardDeviceEntity socketDevice)
        {
            _logger.LogInformation("创建Socket设备：{0} ", JsonConvert.SerializeObject(socketDevice, JsonUtil.JsonPrintSettings));
            ISocketDevice device = null;
            if (socketDevice.RelayCommunicateType == CommunicateModeTypeEnum.TCP.Value)
            {
                device = _containerProvider.Resolve<DefaultTcpDevice>();
            }
            else
            {
                device = _containerProvider.Resolve<DefaultUdpDevice>();
            }
            //初始化Socket设备
            await device.InitAsync(socketDevice);
            return device;
        }

        ///// <summary>
        ///// 根据设备信息创建对应设备操作
        ///// </summary>
        ///// <param name="socketDeviceModels">设备信息</param>
        ///// <returns>所有串口</returns>
        //public async Task<List<ISocketDevice>> CreatesAsync(List<UnitCardDeviceEntity> socketDeviceModels)
        //{
        //    var socketDevices = new List<ISocketDevice>();
        //    foreach (var item in socketDeviceModels)
        //    {
        //        try
        //        {
        //            //创建串口
        //            var device = await CreateAsync(item);
        //            socketDevices.Add(device);
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError("创建Socket设备失败：ex:{0} ", ex);
        //        }
        //    }
        //    return socketDevices;
        //}
    }
}
