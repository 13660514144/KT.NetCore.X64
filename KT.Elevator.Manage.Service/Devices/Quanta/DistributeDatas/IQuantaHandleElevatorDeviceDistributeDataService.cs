using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas
{
    public interface IQuantaHandleElevatorDeviceDistributeDataService
    {
        Task AddOrEditAsync(RemoteDeviceModel remoteDevice, UnitHandleElevatorDeviceModel model);
        Task DeleteAsync(RemoteDeviceModel remoteDevice, string id);
    }
}
