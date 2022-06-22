using KT.Quanta.Service.Dtos;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler.Clients
{
    public interface ISchindlerClientHostBase
    {
        CommunicateDeviceInfoModel CommunicateDeviceInfo { get; set; }

        Task CloseAsync();
        Task ConnectAsync();
        Task InitAsync(string ipAddress, int port);
        Task SendAsync(byte[] bytes);
        Task<bool> CheckAndLinkAsync();
        Task HeartbeatAsync();
    }
}