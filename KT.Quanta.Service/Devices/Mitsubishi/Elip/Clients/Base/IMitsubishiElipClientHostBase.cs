using DotNetty.Transport.Channels;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using System.Net;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients
{
    public interface IMitsubishiElipClientHostBase
    {
        IPAddress RemoteIp { get; }
        int RemotePort { get; }
        int LocalPort { get; }
        IChannel ClientChannel { get; }

        Task CloseAsync();
        Task ConnectAsync();
        Task InitAsync(string remoteIp, int remotePort);
        Task SendAsync(MitsubishiElipCommunicationHeader header);
        Task HeartbeatAsync();
        Task SetLocalPortAsync(int localPort);
    }
}