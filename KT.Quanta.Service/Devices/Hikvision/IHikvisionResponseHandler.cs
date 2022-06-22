using KT.Quanta.Service.Models;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hikvision
{
    public interface IHikvisionResponseHandler
    {
        Task UploadPassRecordAsync(PassRecordModel passRecord);
    }
}