using KT.Elevator.Manage.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Helpers
{
    public interface IRemoteDeviceService
    {
        Task AddOrUpdateCardDeviceAsync(RemoteDeviceModel remoteDevice, CardDeviceModel model);
        Task DeleteCardDeviceAsync(RemoteDeviceModel remoteDevice, string id, long time);
    }
}
