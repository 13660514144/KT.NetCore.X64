using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.DeviceDistributes
{
    public class CardDeviceDeviceDistributeService : ICardDeviceDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        public CardDeviceDeviceDistributeService(RemoteDeviceList remoteDeviceList)
        {
            _remoteDeviceList = remoteDeviceList;
        }

        public async Task AddOrUpdateAsync(CardDeviceModel model)
        {
            //获取分发设备
            var remoteDevice = await _remoteDeviceList.GetByKeyAsync(model.Processor.ProcessorKey);
            if (remoteDevice != null)
            {
                await remoteDevice?.AddOrUpdateCardDeviceAsync(model);
            }
        }

        public async Task DeleteAsync(string remoteDeviceKey, string id, long time)
        {
            //获取分发设备
            var remoteDevice = await _remoteDeviceList.GetByKeyAsync(remoteDeviceKey);
            if (remoteDevice != null)
            {
                await remoteDevice?.DeleteCardDeviceAsync(id, time);
            }
        }
    }
}
