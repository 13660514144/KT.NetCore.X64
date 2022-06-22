using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public interface IQuantaHandleElevatorDeviceDistributeDataService
    {
        Task AddOrEditAsync(RemoteDeviceModel remoteDevice, UnitHandleElevatorDeviceModel model);
        Task DeleteAsync(RemoteDeviceModel remoteDevice, string id);
    }
}
