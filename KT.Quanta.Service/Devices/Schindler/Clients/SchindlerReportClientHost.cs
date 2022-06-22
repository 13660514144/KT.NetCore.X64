using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace KT.Quanta.Service.Devices.Schindler.Clients
{
    /// <summary>
    /// socket客户端
    /// </summary>
    public class SchindlerReportClientHost : SchindlerClientHostBase, ISchindlerReportClientHost
    {
        private IServiceProvider _serviceProvider;
        public SchindlerReportClientHost(ILogger<SchindlerReportClientHost> logger,
            IServiceProvider serviceProvider)
            : base(logger, serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override ISchindlerClientHandlerBase GetClientHandler()
        {
            return _serviceProvider.GetRequiredService<ISchindlerReportClientHandler>();
        }
    }
}
