using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Coders;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Handlers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients
{
    /// <summary>
    /// socket客户端
    /// </summary>
    public class MitsubishiElipTcpClientHost : MitsubishiElipTcpClientHostBase, IMitsubishiElipClientHost
    {
        private ILogger<MitsubishiElipTcpClientHost> _logger;
        private IServiceProvider _serviceProvider;

        public MitsubishiElipTcpClientHost(ILogger<MitsubishiElipTcpClientHost> logger,
            IServiceProvider serviceProvider)
            : base(logger, serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> CheckAndLinkAsync()
        {
            if (ClientChannel == null || !ClientChannel.Active)
            {
                await ConnectAsync();
            }
            return true;
        }

        public override IMitsubishiElipClientHandlerBase GetChannelHandler()
        {
            var mitsubishiElipClientHandler = _serviceProvider.GetRequiredService<IMitsubishiElipClientHandler>();
            return mitsubishiElipClientHandler;
        }
    }
}
