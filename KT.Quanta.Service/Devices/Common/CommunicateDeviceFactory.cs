using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Hikvision;
using KT.Quanta.Service.Devices.Kone;
using KT.Quanta.Service.Devices.Mitsubishi.Elip;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw;
using KT.Quanta.Service.Devices.Quanta;
using KT.Quanta.Service.Devices.Schindler;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    public class CommunicateDeviceFactory : ICommunicateDeviceFactory
    {
        private readonly ILogger<CommunicateDeviceFactory> _logger;
        private IServiceProvider _serviceProvider;

        public CommunicateDeviceFactory(ILogger<CommunicateDeviceFactory> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<ICommunicateDevice> CreatorAsync(CommunicateDeviceInfoModel model, RemoteDeviceModel remoteDevice)
        {
            _logger.LogInformation($"初始化远程电梯设备：{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

            ICommunicateDevice communicateDevice = null;
            //海康
            if (model.CommunicateDeviceType == CommunicateDeviceTypeEnum.HIKVISION.Value)
            {
                communicateDevice = _serviceProvider.GetRequiredService<HikvisionCommunicateDevice>();
            }
            //康塔
            else if (model.CommunicateDeviceType == CommunicateDeviceTypeEnum.QUANTA.Value)
            {
                communicateDevice = _serviceProvider.GetRequiredService<QuantaCommunicateDevice>();
            }
            //通力
            else if (model.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_RCGIF.Value)
            {
                communicateDevice = _serviceProvider.GetRequiredService<KoneRcgifCommunicateDevice>();
            }
            //通力
            else if (model.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_ELI.Value)
            {
                communicateDevice = _serviceProvider.GetRequiredService<KoneEliCommunicateDevice>();
            }
            //迅达
            else if (model.CommunicateDeviceType == CommunicateDeviceTypeEnum.SCHINDLER_SYNC.Value)
            {
                communicateDevice = _serviceProvider.GetRequiredService<SchindlerCommunicateDevice>();
            }
            //迅达
            else if (model.CommunicateDeviceType == CommunicateDeviceTypeEnum.SCHINDLER_DISPATCH.Value)
            {
                communicateDevice = _serviceProvider.GetRequiredService<SchindlerCommunicateDevice>();
            }
            //迅达
            else if (model.CommunicateDeviceType == CommunicateDeviceTypeEnum.SCHINDLER_RECORD.Value)
            {
                communicateDevice = _serviceProvider.GetRequiredService<SchindlerCommunicateDevice>();
            }
            //三菱
            else if (model.CommunicateDeviceType == CommunicateDeviceTypeEnum.MITSUBISHI_ELSGW.Value)
            {
                communicateDevice = _serviceProvider.GetRequiredService<MitsubishiElsgwCommunicateDevice>();
            }
            //三菱
            else if (model.CommunicateDeviceType == CommunicateDeviceTypeEnum.MITSUBISHI_ELIP.Value)
            {
                communicateDevice = _serviceProvider.GetRequiredService<MitsubishiElipCommunicateDevice>();
            }

            if (communicateDevice == null)
            {
                _logger.LogError($"初始化远程设备出错：message:not found the brand model. data:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");
                communicateDevice = DefaultCommunicateDevice();
            }
            //communicateDevice = DefaultCommunicateDevice();
            await communicateDevice.InitAsync(model, remoteDevice);            
            return communicateDevice;
        }

        private ICommunicateDevice DefaultCommunicateDevice()
        {
            var communicateDevice = _serviceProvider.GetRequiredService<QuantaCommunicateDevice>();

            return communicateDevice;
        }
    }
}
