using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Models;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.DeviceDistributes
{
    public class DeviceInfoDeviceDistributeService : IDeviceInfoDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        public DeviceInfoDeviceDistributeService(RemoteDeviceList remoteDeviceList)
        {
            _remoteDeviceList = remoteDeviceList;
        }


        public async Task<List<ProcessorModel>> GetProcessorFloorsAsync()
        {
            var results = new List<ProcessorModel>();
            //获取分发设备 
            await _remoteDeviceList.ExecByWhereAsync(x =>
            {
                return x.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value
                && x.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value;
            },
            async (remoteDevice) =>
            {
                var outputNum = await remoteDevice.GetOutputNumAsync();
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

        public async Task<List<ProcessorFloorModel>> GetInitByProcessorIdAsync(string id)
        {
            var results = new List<ProcessorFloorModel>();
            //获取分发设备 
            await _remoteDeviceList.ExecByWhereAsync(x =>
            {
                return x.RemoteDeviceInfo.DeviceId == id
                    && x.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value
                    && x.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value;
            },
            async (remoteDevice) =>
            {
                var outputNum = await remoteDevice.GetOutputNumAsync();

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
