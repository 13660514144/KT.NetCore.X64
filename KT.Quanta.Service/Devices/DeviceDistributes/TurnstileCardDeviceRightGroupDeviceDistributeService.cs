using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public class TurnstileCardDeviceRightGroupDeviceDistributeService : ITurnstileCardDeviceRightGroupDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        
        public TurnstileCardDeviceRightGroupDeviceDistributeService(RemoteDeviceList remoteDeviceList
             )
        {
            _remoteDeviceList = remoteDeviceList;
            
        }

        public async Task AddOrUpdateAsync(TurnstileCardDeviceRightGroupModel model)
        {
            //获取分发设备
            await _remoteDeviceList.ExecuteAsync(
                (remoteDevice) =>
                {
                    return DistributeWhereAsync(model, remoteDevice);
                },
                async (remoteDevice) =>
                {
                    if (remoteDevice.TurnstileDataRemoteService != null)
                    {

                        await remoteDevice.TurnstileDataRemoteService?.AddOrUpdateCardDeviceRightGroupAsync(remoteDevice, model);
                    }
                });
        }

        private bool DistributeWhereAsync(TurnstileCardDeviceRightGroupModel model, IRemoteDevice remoteDevice)
        {
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.QSCS_3600P.Value)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteAsync(TurnstileCardDeviceRightGroupModel model)
        {
            //获取分发设备
            await _remoteDeviceList.ExecuteAsync(
                (remoteDevice) =>
                {
                    return DistributeWhereAsync(model, remoteDevice);
                },
                async (remoteDevice) =>
                {
                    if (remoteDevice.TurnstileDataRemoteService != null)
                    {
                        await remoteDevice.TurnstileDataRemoteService?.DeleteCardDeviceRightGroupAsync(remoteDevice, model.Id, model.EditedTime);
                    }
                });
        }
    }
}
