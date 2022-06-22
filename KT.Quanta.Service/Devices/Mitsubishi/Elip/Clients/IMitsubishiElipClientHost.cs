using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients
{
    public interface IMitsubishiElipClientHost : IMitsubishiElipClientHostBase
    {
        Task<bool> CheckAndLinkAsync();
    }
}