using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Schindler.Models;
using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public class ElevatorPassRightDeviceDistributeService : IElevatorPassRightDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        public ElevatorPassRightDeviceDistributeService(RemoteDeviceList remoteDeviceList)
        {
            _remoteDeviceList = remoteDeviceList;
        }

        public async Task AddOrUpdateAsync(PassRightModel model, FaceInfoModel face, PassRightModel oldModel)
        {
            //获取分发设备
            await _remoteDeviceList.ExecuteAsync(
                (remoteDevice) =>
                {
                    return DistributeWhereAsync(model, remoteDevice);
                },
                async (remoteDevice) =>
                {
                    if (remoteDevice.ElevatorDataRemoteService != null)
                    {
                        await remoteDevice.ElevatorDataRemoteService?.AddOrUpdatePassRightAsync(remoteDevice, model, face, oldModel);
                    }

                    ////分发父类
                    //await DistributeParentAddOrUpdateAsync(model, face, remoteDevice);
                });
        }
        public async Task DeleteAsync(PassRightModel model)
        {
            //获取分发设备
            await _remoteDeviceList.ExecuteAsync(
                (remoteDevice) =>
                {
                    return DistributeWhereAsync(model, remoteDevice);
                },
                async (remoteDevice) =>
                {
                    if (remoteDevice.ElevatorDataRemoteService != null)
                    {
                        await remoteDevice.ElevatorDataRemoteService?.DeletePassRightAsync(remoteDevice, model);
                    }

                    ////分发父类
                    //await DistributeParentDeleteAsync(model, remoteDevice);
                });
        }

        private bool DistributeWhereAsync(PassRightModel model, IRemoteDevice remoteDevice)
        {
            //边缘处理器
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value)
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
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR_CARD_DEVICE.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_K152AM.Value)
                {
                    return true;
                }
            }
            //二次派梯一体机
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.QSCS_DLS81.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.QIACS_DLS81.Value)
                {
                    return true;
                }
            }
            //派梯客户端
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_CLIENT.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HITACHI_ELE_3600.Value)
                {
                    return true;
                }
            }
            
            //闸机中的梯控主机也要发送
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value)
                {
                    if (model.AccessType != AccessTypeEnum.FACE.Value)
                    {
                        //人脸不能分发到海康梯控主机 
                        return true;
                    }
                }
            }
            //电梯服务
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SERVER.Value)
            {
                //迅达电梯数据同步
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.SCHINDLER_PORT.Value)
                {
                    if (remoteDevice.RemoteDeviceInfo.ModuleType == SchindlerModuleTypeEnum.SYNC.Value)
                    {
                        return true;
                    }
                }
                //三菱派梯设备
                else if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HITACHI_DFRS.Value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
