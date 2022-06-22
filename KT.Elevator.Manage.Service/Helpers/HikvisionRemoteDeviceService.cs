using KT.Elevator.Manage.Service.Devices.Hikvision;
using KT.Elevator.Manage.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Helpers
{
    public class HikvisionRemoteDeviceService : IRemoteDeviceService
    {
        public Task AddOrUpdateCardDeviceAsync(RemoteDeviceModel remoteDevice, CardDeviceModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCardDeviceAsync(RemoteDeviceModel remoteDevice, string id, long time)
        {
            throw new NotImplementedException();
        }
    }
}
