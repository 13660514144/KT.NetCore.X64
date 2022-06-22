using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public class ElevatorDeviceInfoDeviceDistributeService : IElevatorDeviceInfoDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        public ElevatorDeviceInfoDeviceDistributeService(RemoteDeviceList remoteDeviceList)
        {
            _remoteDeviceList = remoteDeviceList;
        }

        public async Task<List<ProcessorModel>> GetProcessorFloorsAsync()
        {
            var results = new List<ProcessorModel>();
            //获取电梯分发设备 
            await _remoteDeviceList.ExecuteAsync(x =>
            {
                return x.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value;
            },
            async (remoteDevice) =>
            {
                var outputNum = await remoteDevice.ElevatorDataRemoteService.GetOutputNumAsync(remoteDevice);
                var result = new ProcessorModel();
                result.Id = remoteDevice.RemoteDeviceInfo.DeviceId;
                result.ProcessorFloors = new List<ProcessorFloorModel>();

                ProcessorFloorModel detail;
                for (int i = 1; i <= outputNum; i++)
                {
                    detail = new ProcessorFloorModel();
                    detail.SortId = i;
                    detail.ProcessorId = remoteDevice.RemoteDeviceInfo.DeviceId;

                    result.ProcessorFloors.Add(detail);
                }

                results.Add(result);
            });
            return results;
        }

        /// <summary>
        /// 获取Hikvision输出映射
        /// </summary>
        /// <param name="processorId">边缘处理器id</param>
        /// <returns></returns>
        public async Task<List<ProcessorFloorModel>> GetInitOutputByProcessorIdAsync(string processorId)
        {
            var results = new List<ProcessorFloorModel>();
            //获取分发设备 
            await _remoteDeviceList.ExecuteAsync(x =>
            {
                return x.RemoteDeviceInfo.DeviceId == processorId
                    && x.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value;
            },
            async (remoteDevice) =>
            {
                var outputNum = await remoteDevice.ElevatorDataRemoteService.GetOutputNumAsync(remoteDevice);
                ProcessorFloorModel result;
                for (int i = 1; i <= outputNum; i++)
                {
                    result = new ProcessorFloorModel();
                    result.SortId = i;
                    result.ProcessorId = remoteDevice.RemoteDeviceInfo.DeviceId;

                    results.Add(result);
                }
            });

            return results;
        }

    }
}
