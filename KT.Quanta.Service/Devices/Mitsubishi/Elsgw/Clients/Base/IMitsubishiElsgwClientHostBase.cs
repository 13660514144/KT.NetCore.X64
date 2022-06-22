using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Clients
{
    public interface IMitsubishiElsgwClientHostBase
    {
        Task CloseAsync();
        Task ConnectAsync();
        Task InitAsync(string ipAddress, int port);
        Task SendAsync(MitsubishiElsgwTransmissionHeader header);
        Task<bool> CheckAndLinkAsync();
        Task HeartbeatAsync();
    }
}