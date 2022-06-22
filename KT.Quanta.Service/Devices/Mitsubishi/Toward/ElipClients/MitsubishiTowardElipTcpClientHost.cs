using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients;
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

namespace KT.Quanta.Service.Devices.Mitsubishi.Toward.ElipClients
{
    /// <summary>
    /// socket客户端
    /// </summary>
    public class MitsubishiTowardElipTcpClientHost : MitsubishiElipTcpClientHostBase, IMitsubishiTowardElipTcpClientHost
    {
        private ILogger<MitsubishiTowardElipTcpClientHost> _logger;
        private IServiceProvider _serviceProvider;

        public MitsubishiTowardElipTcpClientHost(ILogger<MitsubishiTowardElipTcpClientHost> logger,
            IServiceProvider serviceProvider)
            : base(logger, serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public override IMitsubishiElipClientHandlerBase GetChannelHandler()
        {
            var mitsubishiElipClientHandler = _serviceProvider.GetRequiredService<IMitsubishiTowardElipClientHandler>(); 
            return mitsubishiElipClientHandler;
        }
    }
}
