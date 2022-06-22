using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public class TurnstileCardDeviceDeviceDistributeService : ITurnstileCardDeviceDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        private readonly ILogger<TurnstileCardDeviceDeviceDistributeService> _logger;
        public TurnstileCardDeviceDeviceDistributeService(RemoteDeviceList remoteDeviceList,
            ILogger<TurnstileCardDeviceDeviceDistributeService> logger)
        {
            _remoteDeviceList = remoteDeviceList;
            _logger = logger;
        }

        public async Task AddOrUpdateAsync(TurnstileCardDeviceModel model)
        {
            //获取分发设备
            var remoteDevice = await _remoteDeviceList.GetByIdAsync(model.Processor.Id);
            if (remoteDevice?.TurnstileDataRemoteService != null)
            {
                if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR.Value)
                {
                    if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.QSCS_3600P.Value)
                    {
                        await remoteDevice.TurnstileDataRemoteService?.AddOrUpdateCardDeviceAsync(remoteDevice, model);
                    }
                }
            }
        }

        public async Task DeleteAsync(string id, long time)
        {
            //获取分发设备
            var remoteDevice = await _remoteDeviceList.GetByIdAsync(id);
            if (remoteDevice?.TurnstileDataRemoteService != null)
            {
                if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR.Value)
                {
                    if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.QSCS_3600P.Value)
                    {
                        await remoteDevice.TurnstileDataRemoteService?.DeleteCardDeviceAsync(remoteDevice, id, time);
                    }
                }
            }
        }
    }
}
