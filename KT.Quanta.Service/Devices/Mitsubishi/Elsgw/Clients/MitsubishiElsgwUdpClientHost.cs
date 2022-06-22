using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Coders;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Clients
{
    /// <summary>
    /// socket客户端
    /// </summary>
    public class MitsubishiElsgwUdpClientHost : MitsubishiElsgwUdpClientHostBase, IMitsubishiElsgwClientHost
    {
        public MitsubishiElsgwUdpClientHost(ILogger<MitsubishiElsgwUdpClientHost> logger,
            IServiceProvider serviceProvider,
            IMitsubishiElsgwFrameEncoder mitsubishiElsgwFrameEncoder)
            : base(logger, serviceProvider, mitsubishiElsgwFrameEncoder)
        {
        }
    }
}
