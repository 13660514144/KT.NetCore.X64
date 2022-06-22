using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Coders;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
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
    public abstract class MitsubishiElipTcpClientHostBase : IMitsubishiElipClientHostBase
    {
        private MultithreadEventLoopGroup _multithreadEventLoopGroup;
        private Bootstrap _bootstrap;

        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger<MitsubishiElipTcpClientHostBase> _logger;

        public IPAddress RemoteIp { get; private set; }
        public int RemotePort { get; private set; }
        public int LocalPort { get; private set; }
        public IChannel ClientChannel { get; private set; }

        public MitsubishiElipTcpClientHostBase(ILogger<MitsubishiElipTcpClientHostBase> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="remoteIp">ip地址</param>
        /// <param name="remotePort">端口</param>
        public Task InitAsync(string remoteIp, int remotePort)
        {
            RemoteIp = IPAddress.Parse(remoteIp);
            RemotePort = remotePort;

            _multithreadEventLoopGroup = new MultithreadEventLoopGroup(2);
            _bootstrap = new Bootstrap();

            _bootstrap.Group(_multithreadEventLoopGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast(new LoggingHandler());

                    //编码 
                    var frameEncoder = _serviceProvider.GetRequiredService<MitsubishiElipFrameEncoder>();
                    pipeline.AddLast("framing-enc", frameEncoder);

                    //解码
                    var frameDecoder = _serviceProvider.GetRequiredService<MitsubishiElipFrameDecoder>();
                    pipeline.AddLast("framing-dec", frameDecoder);

                    //数据解析
                    var mitsubishiElipClientHandler = GetChannelHandler();
                    pipeline.AddLast("echo", mitsubishiElipClientHandler);
                }));

            _logger.LogInformation($"{RemoteIp}:{RemotePort} MitsubishiElip初始化Socket客户端成功！ ");

            return Task.CompletedTask;
        }

        public abstract IMitsubishiElipClientHandlerBase GetChannelHandler();

        public Task SetLocalPortAsync(int localPort)
        {
            LocalPort = localPort;

            if (LocalPort > 0)
            {
                _bootstrap.LocalAddress(LocalPort);
                _logger.LogInformation($"{RemoteIp}:{RemotePort} MitsubishiElip设置本地端口：localPort:{LocalPort} ");
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 连接
        /// </summary>
        public async Task ConnectAsync()
        {
            _logger.LogInformation($"{RemoteIp}:{RemotePort} {LocalPort} MitsubishiElip开启Socket客户端开始连接！");
            ClientChannel = await _bootstrap.ConnectAsync(new IPEndPoint(RemoteIp, RemotePort));
            _logger.LogInformation($"{RemoteIp}:{RemotePort} {LocalPort} MitsubishiElip开启Socket客户端连接成功！");
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public async Task CloseAsync()
        {
            if (ClientChannel != null)
            {
                await ClientChannel.CloseAsync();
            }
            if (_multithreadEventLoopGroup != null)
            {
                await _multithreadEventLoopGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            }
            _logger.LogInformation($"{RemoteIp}:{RemotePort} MitsubishiElip关闭Socket客户端成功！");
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="bytes">消息数据</param>
        public async Task SendAsync(MitsubishiElipCommunicationHeader header)
        {
            header.DataLength = (byte)(header.Assistant.Datas.Count() + 1);

            var byteBuffer = header.WriteToLocalBuffer();

            if (ClientChannel == null || !ClientChannel.Active)
            {
                await ConnectAsync();
            }
            _logger.LogInformation($"{RemoteIp}:{RemotePort} MitsubishiElip发送数据：data:{JsonConvert.SerializeObject(header, JsonUtil.JsonPrintSettings)} ");

            await ClientChannel.WriteAndFlushAsync(byteBuffer);
        }

        public async Task HeartbeatAsync()
        {
            var header = new MitsubishiElipCommunicationHeader();
            header.Assistant.Command = (byte)MitsubishiElipCommandEnum.Heartbeat.Code;

            var heartbeat = new MitsubishiElipHeartbeatModel();
            heartbeat.WriteToLocalBuffer();

            header.Assistant.Datas = heartbeat.GetBytes().ToList();
            header.DataLength = (byte)(header.Assistant.Datas.Count() + 1);

            var byteBuffer = header.WriteToLocalBuffer();

            if (ClientChannel?.Active == true)
            {
                _logger.LogInformation($"{RemoteIp}:{RemotePort} MitsubishiElip发送数据-心跳：data:{JsonConvert.SerializeObject(header, JsonUtil.JsonPrintSettings)} ");

                await ClientChannel.WriteAndFlushAsync(byteBuffer);
            }
            else
            {
                _logger.LogWarning($"{RemoteIp}:{RemotePort} MitsubishiElip设备离线！");
            }
        }
    }
}
