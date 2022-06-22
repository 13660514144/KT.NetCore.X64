using DotNetty.Transport.Channels;
using System;

namespace KT.Quanta.Service.Devices.Kone.Clients
{
    public interface IKoneEliClientHandler : IChannelHandler
    {
        Action SetReceiveHeartbeatTimeHandler { get; set; }
    }
}