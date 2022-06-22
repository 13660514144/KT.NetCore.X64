using KT.Quanta.Service.Devices.Kone.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone.Clients
{
    public interface IKoneEliClientHost
    {
        Task CloseAsync();
        Task ConnectAsync();
        Task InitAsync(string ipAddress, int port);
        Task SendAsync(KoneEliHeaderRequest bytes);
        Task<bool> CheckAndLinkAsync();
        Task HeartbeatAsync();
    }
}