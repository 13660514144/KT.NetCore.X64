using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Common.Netty.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KT.Common.Netty.Clients
{
    /// <summary>
    /// socket客户端
    /// </summary>
    public abstract class QuantaClientHostBase : IQuantaClientHostBase
    {
        private IPAddress _remoteIp;
        private int _remotePort;
        private IChannel _clientChannel;
        private MultithreadEventLoopGroup _multithreadEventLoopGroup;
        private Bootstrap _bootstrap;

        private readonly IServiceProvider _serviceProvider; 
        private readonly ILogger<QuantaClientHostBase> _logger;

        public QuantaClientHostBase(ILogger<QuantaClientHostBase> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="remoteIp">连接IP地址</param>
        /// <param name="remotePort">连接端口</param>
        public Task InitAsync(string remoteIp, int remotePort)
        {
            _remoteIp = IPAddress.Parse(remoteIp);
            _remotePort = remotePort;

            _multithreadEventLoopGroup = new MultithreadEventLoopGroup(2);
            _bootstrap = new Bootstrap();

            _bootstrap.Group(_multithreadEventLoopGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<ISocketChannel>(async channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;

                    pipeline.AddLast(new LoggingHandler());

                    var encoders = await GetFrameEncodersAsync();
                    foreach (var item in encoders)
                    {
                        pipeline.AddLast($"framing-enc-{item.GetType().Name}", item);
                    }

                    var decoders = await GetFrameDecodersAsync();
                    foreach (var item in decoders)
                    {
                        pipeline.AddLast($"framing-dec-{item.GetType().Name}", item);
                    }

                    var handlers = await GetClientHandlersAsync();
                    foreach (var item in handlers)
                    {
                        pipeline.AddLast($"echo-{item.GetType().Name}", item);
                    }
                }));

            _logger.LogInformation($"{_remoteIp}:{_remotePort} Quanta初始化Socket客户端成功！");

            return Task.CompletedTask;
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<IChannelHandler>> GetFrameEncodersAsync();

        /// <summary>
        /// 解码
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<IChannelHandler>> GetFrameDecodersAsync();

        /// <summary>
        /// 数据处理
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<IChannelHandler>> GetClientHandlersAsync();

        /// <summary>
        /// 连接
        /// </summary>
        public async Task ConnectAsync()
        {
            _logger.LogInformation($"{_remoteIp}:{_remotePort} Quanta开启Socket客户端连接！");
            _clientChannel = await _bootstrap.ConnectAsync(new IPEndPoint(_remoteIp, _remotePort));
            _logger.LogInformation($"{_remoteIp}:{_remotePort} Quanta开启Socket客户端连接成功！");
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public async Task CloseAsync()
        {
            _logger.LogInformation($"{_remoteIp}:{_remotePort} Quanta关闭Socket客户端！");
            await _clientChannel.CloseAsync();
            await _multithreadEventLoopGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            _logger.LogInformation($"{_remoteIp}:{_remotePort} Quanta关闭Socket客户端成功！");
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="bytes">消息数据</param>
        public async Task SendAsync(QuantaNettyHeader header)
        {
            _logger.LogInformation($"{_remoteIp}:{_remotePort} QuantaSocket客户端发送数据！");
            if (_clientChannel == null || !_clientChannel.Active)
            {
                await ConnectAsync();
            }

            await _clientChannel.WriteAndFlushAsync(header);
            _logger.LogInformation($"{_remoteIp}:{_remotePort} QuantaSocket客户端发送数据成功！");
        }
    }
}
