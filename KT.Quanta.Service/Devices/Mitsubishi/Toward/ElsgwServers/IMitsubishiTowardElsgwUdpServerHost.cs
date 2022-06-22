using KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using System.Net;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Toward.ElsgwServers
{
    public interface IMitsubishiTowardElsgwUdpServerHost
    {
        IMitsubishiElipClientHostBase MitsubishiElipClientHost { get; set; }

        Task CloseAsync();
        Task InitAsync(int port);
        Task SendAsync(MitsubishiElsgwTransmissionHeader header, EndPoint endPoint);
    }
}