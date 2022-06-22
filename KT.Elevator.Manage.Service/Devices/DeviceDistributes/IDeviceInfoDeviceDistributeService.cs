using KT.Elevator.Manage.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.DeviceDistributes
{
    public interface IDeviceInfoDeviceDistributeService
    {
        Task<List<ProcessorModel>> GetProcessorFloorsAsync();
        Task<List<ProcessorFloorModel>> GetInitByProcessorIdAsync(string id);
    }
}
