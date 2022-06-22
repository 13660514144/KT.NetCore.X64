using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Common.Netty.Servers
{
    /// <summary>
    /// SocketTcp服务
    /// </summary>
    public abstract class QuantaServerHostBase : IQuantaServerHostBase
    {
        private IEventLoopGroup _bossGroup;
        private IEventLoopGroup _workerGroup;
        private ServerBootstrap _bootstrap;
        private IChannel _boundChannel;

        private readonly ILogger<QuantaServerHostBase> _logger;

        public QuantaServerHostBase(ILogger<QuantaServerHostBase> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 启动服务监听
        /// </summary>
        /// <param name="port">端口</param> 
        public async void RunAsync(int port)
        {
            _bossGroup = new MultithreadEventLoopGroup(1);
            _workerGroup = new MultithreadEventLoopGroup(2);
            _bootstrap = new ServerBootstrap();

            _bootstrap.Group(_bossGroup, _workerGroup)
                .Channel<TcpServerSocketChannel>()
                .Option(ChannelOption.SoBacklog, 1024)
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new LoggingHandler("SRV-LSTN"))
                .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;

                    pipeline.AddLast(new LoggingHandler("SRV-CONN", DotNetty.Handlers.Logging.LogLevel.DEBUG));
                    pipeline.AddLast("framing-enc", GetFrameEncoder());
                    pipeline.AddLast("framing-dec", GetFrameDecoder());

                    //解析数据
                    var quantaServerHandler = GetChannelHandler();
                    pipeline.AddLast("echo", quantaServerHandler);
                }));

            _boundChannel = await _bootstrap.BindAsync(port);

            _logger.LogInformation($"{_boundChannel.LocalAddress} 启动TCP服务：localPort:{port} ");
        }

        /// <summary>
        /// 获取编码器
        /// </summary>
        /// <returns></returns>
        public abstract QuantaServerFrameEncoder GetFrameEncoder();

        /// <summary>
        /// 获取解码器
        /// </summary>
        /// <returns></returns>
        public abstract QuantaServerFrameDecoder GetFrameDecoder();

        /// <summary>
        /// 处理类
        /// </summary>
        /// <returns></returns>
        public abstract IChannelHandler GetChannelHandler();

        /// <summary>
        /// 关闭程序
        /// </summary>
        /// <param name="bossGroup"></param>
        /// <param name="workerGroup"></param>
        /// <returns></returns>
        public async Task Shutdown()
        {
            //关闭通道
            await _boundChannel.CloseAsync();

            //优雅关闭事件循环组
            await Task.WhenAll(
                _bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                _workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
        }
    }
}
