using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using KT.Common.Netty.Clients;
using Microsoft.Extensions.Logging;
using System;

namespace KT.Device.Quanta.Clients
{
    public class QuantaClientHandler : QuantaClientHandlerBase, IQuantaClientHandler
    {
        private readonly ILogger<QuantaClientHandler> _logger;

        public QuantaClientHandler(ILogger<QuantaClientHandler> logger)
            : base(logger)
        {
            _logger = logger;
        }
    }
}