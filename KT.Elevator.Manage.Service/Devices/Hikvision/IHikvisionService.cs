using KT.Elevator.Manage.Service.Models;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Hikvision
{
    public interface IHikvisionService
    {
        Task UploadPassRecordAsync(PassRecordModel passRecord);
    }
}