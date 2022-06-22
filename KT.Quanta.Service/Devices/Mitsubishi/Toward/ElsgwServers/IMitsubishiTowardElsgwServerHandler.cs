using DotNetty.Transport.Channels;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.Handlers;

namespace KT.Quanta.Service.Devices.Mitsubishi.Toward.ElsgwServers
{
    public interface IMitsubishiTowardElsgwServerHandler : IChannelHandler
    {
        IMitsubishiTowardElsgwRequestHandler MitsubishiTowardElsgwRequestHandler { get; set; }
    }
}