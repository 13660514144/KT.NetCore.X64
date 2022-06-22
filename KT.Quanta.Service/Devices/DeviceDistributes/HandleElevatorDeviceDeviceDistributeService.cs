using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public class HandleElevatorDeviceDeviceDistributeService : IHandleElevatorDeviceDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        private DistributeHelper _distributeHelper;
        public HandleElevatorDeviceDeviceDistributeService(RemoteDeviceList remoteDeviceList,
            DistributeHelper distributeHelper)
        {
            _remoteDeviceList = remoteDeviceList;
            _distributeHelper = distributeHelper;
        }

        public async Task AddOrUpdateAsync(UnitHandleElevatorDeviceModel model)
        {
            //获取分发设备 
            await _remoteDeviceList.ExecuteAsync(remoteDevice =>
            {
                return DistributeWhereAsync(model.Id, remoteDevice);
            },
            async (remoteDevice) =>
            {
                if (remoteDevice.ElevatorDataRemoteService != null)
                {
                    await remoteDevice.ElevatorDataRemoteService?.AddOrUpdateHandleElevatorDeviceAsync(remoteDevice, model);
                }
            });
        }

        private static bool DistributeWhereAsync(string id, IRemoteDevice remoteDevice)
        {
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.QSCS_DLS81.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.QIACS_DLS81.Value)
                {
                    if (remoteDevice.RemoteDeviceInfo.DeviceId == id)
                    {
                        return true;
                    }
                }
            }
            //派梯客户端
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_CLIENT.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HITACHI_ELE_3600.Value)
                {
                    if (remoteDevice.RemoteDeviceInfo.DeviceId == id)
                    {
                        return true;
                    }
                }
            }
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SERVER.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HITACHI_DFRS.Value)
                {
                    if (remoteDevice.RemoteDeviceInfo.DeviceId == id)
                    {
                        return true;
                    }
                }
            }
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value)
            {
                return true;
            }
            return false;
        }

        public async Task DeleteAsync(string id)
        {
            //获取分发设备 
            await _remoteDeviceList.ExecuteAsync(remoteDevice =>
            {
                return DistributeWhereAsync(id, remoteDevice);
            },
            async (remoteDevice) =>
            {
                if (remoteDevice.ElevatorDataRemoteService != null)
                {
                    await remoteDevice.ElevatorDataRemoteService?.DeleteHandleElevatorDeviceAsync(remoteDevice, id);
                }
            });
        }


    }
}
