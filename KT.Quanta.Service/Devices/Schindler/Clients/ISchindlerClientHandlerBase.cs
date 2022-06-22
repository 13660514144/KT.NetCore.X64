using DotNetty.Transport.Channels;
using KT.Quanta.Service.Dtos;

namespace KT.Quanta.Service.Devices.Schindler.Clients
{
    public interface ISchindlerClientHandlerBase : IChannelHandler
    {
        CommunicateDeviceInfoModel CommunicateDeviceInfo { get; set; }
    }
}