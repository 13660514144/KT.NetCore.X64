using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Hikvision.Models;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public class TurnstilePassRightDeviceDistributeService : ITurnstilePassRightDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        private IServiceScopeFactory _serviceScopeFactory;

        public TurnstilePassRightDeviceDistributeService(RemoteDeviceList remoteDeviceList,
            IServiceScopeFactory serviceScopeFactory)
        {
            _remoteDeviceList = remoteDeviceList;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task AddOrUpdateAsync(TurnstilePassRightModel model, FaceInfoModel face)
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
                     await remoteDevice.TurnstileDataRemoteService?.AddOrUpdatePassRightAsync(remoteDevice, model, face);
                 }

                 //分发父类,闸机中的梯控主机在梯控服务里面发送 
             });
        }

        private bool DistributeWhereAsync(TurnstilePassRightModel model, IRemoteDevice remoteDevice)
        {

            //边缘处理器
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value)
                {
                    if (model.AccessType != AccessTypeEnum.FACE.Value)
                    {
                        //人脸不能分发到海康梯控主机 
                        return true;
                    }
                }
                else if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.QSCS_3600P.Value)
                {
                    return true;
                }
            }
            //读卡器
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR_CARD_DEVICE.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_K152AM.Value)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task AddOrUpdateAsync(List<string> addOrEditDeviceIds, TurnstilePassRightModel model, FaceInfoModel face)
        {
            //获取分发设备
            await _remoteDeviceList.ExecuteAsync(
                (remoteDevice) =>
                {
                    var isDeviceId = addOrEditDeviceIds?.Contains(remoteDevice.RemoteDeviceInfo.DeviceId) == true;
                    var isDeviceType = DistributeWhereAsync(model, remoteDevice);

                    return isDeviceId && isDeviceType;
                },
                async (remoteDevice) =>
                {
                    if (remoteDevice.TurnstileDataRemoteService != null)
                    {
                        await remoteDevice.TurnstileDataRemoteService?.AddOrUpdatePassRightAsync(remoteDevice, model, face);
                    }

                    //分发父类,闸机中的梯控主机在梯控服务里面发送 
                });
        }

        public async Task DeleteAsync(TurnstilePassRightModel model)
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
                    await remoteDevice.TurnstileDataRemoteService?.DeletePassRightAsync(remoteDevice, model);
                }

                //分发父类,闸机中的梯控主机在梯控服务里面发送 
            });
        }

        public async Task DeleteAsync(List<string> deleteDeviceIds, TurnstilePassRightModel model)
        {
            //获取分发设备
            await _remoteDeviceList.ExecuteAsync(
                (remoteDevice) =>
                {
                    var isDeviceId = deleteDeviceIds?.Contains(remoteDevice.RemoteDeviceInfo.DeviceId) == true;
                    var isDeviceType = DistributeWhereAsync(model, remoteDevice);

                    return isDeviceId && isDeviceType;
                },
                async (remoteDevice) =>
                {
                    if (remoteDevice.TurnstileDataRemoteService != null)
                    {
                        await remoteDevice.TurnstileDataRemoteService?.DeletePassRightAsync(remoteDevice, model);
                    }

                    //分发父类,闸机中的梯控主机在梯控服务里面发送 
                });
        }
    }
}
