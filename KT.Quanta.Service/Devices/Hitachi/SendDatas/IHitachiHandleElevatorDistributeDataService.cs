using KT.Elevator.Unit.Dispatch.Entity.Models;
using KT.Quanta.Service.Models;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hitachi.SendDatas
{
    public interface IHitachiHandleElevatorDistributeDataService
    {
        Task DirectCallAsync(RemoteDeviceModel remoteDevice, UnitDispatchSendHandleElevatorModel result);
        Task MultiFloorCallAsync(RemoteDeviceModel remoteDevice, UnitDispatchSendHandleElevatorModel result);
    }
}