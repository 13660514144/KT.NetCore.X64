using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Schindler.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler.Clients
{
    /// <summary>
    /// socket客户端
    /// </summary>
    public class SchindlerDispatchClientHost : SchindlerClientHostBase, ISchindlerDispatchClientHost
    {
        private IServiceProvider _serviceProvider;
        public SchindlerDispatchClientHost(ILogger<SchindlerDispatchClientHost> logger,
            IServiceProvider serviceProvider)
            : base(logger, serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override ISchindlerClientHandlerBase GetClientHandler()
        {
            return _serviceProvider.GetRequiredService<ISchindlerDispatchClientHandler>();
        }
    }
}
