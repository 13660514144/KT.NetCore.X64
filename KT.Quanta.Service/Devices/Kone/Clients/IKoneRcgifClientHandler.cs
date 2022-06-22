using DotNetty.Transport.Channels;
using System;

namespace KT.Quanta.Service.Devices.Kone.Clients
{
    public interface IKoneRcgifClientHandler : IChannelHandler
    {
        Action SetReceiveHeartbeatTimeHandler { get; set; }
    }
}