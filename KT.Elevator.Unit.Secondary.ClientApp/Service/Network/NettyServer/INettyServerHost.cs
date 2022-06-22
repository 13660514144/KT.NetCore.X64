using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Network.NettyServer
{
    public interface INettyServerHost
    {
        Task CloseAsync();
        Task RunAsync(int port);
    }
}