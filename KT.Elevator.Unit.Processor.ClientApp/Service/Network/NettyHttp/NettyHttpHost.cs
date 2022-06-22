using DotNetty.Codecs.Http;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Network.NettyServer
{
    /// <summary>
    /// NettyHttp服务器
    /// </summary>
    public class NettyHttpHost : INettyHttpHost
    {
        private IChannel _bootstrapChannel;
        private IEventLoopGroup _group;
        private IEventLoopGroup _workGroup;
        private ServerBootstrap _bootstrap;

        private ILogger _logger;
        public NettyHttpHost(ILogger logger)
        {
            _logger = logger;
        }

        public async Task RunAsync(int port)
        {
            Console.WriteLine(
                $"\n{RuntimeInformation.OSArchitecture} {RuntimeInformation.OSDescription}"
                + $"\n{RuntimeInformation.ProcessArchitecture} {RuntimeInformation.FrameworkDescription}"
                + $"\nProcessor Count : {Environment.ProcessorCount}\n");

            Console.WriteLine($"Server garbage collection: {GCSettings.IsServerGC}");
            Console.WriteLine($"Current latency mode for garbage collection: {GCSettings.LatencyMode}");

            _group = new MultithreadEventLoopGroup(1);
            _workGroup = new MultithreadEventLoopGroup(2);
            _bootstrap = new ServerBootstrap();

            _bootstrap.Group(_group, _workGroup)
                .Channel<TcpServerSocketChannel>()
                .Option(ChannelOption.SoBacklog, 8192)
                .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast("encoder", new HttpResponseEncoder());
                    pipeline.AddLast("decoder", new HttpRequestDecoder(4096, 8192, 8192, false));
                    pipeline.AddLast("handler", new HttpServerHandler());
                }));

            _bootstrapChannel = await _bootstrap.BindAsync(IPAddress.IPv6Any, port);

            _logger.LogInformation($"Httpd started. Listening on {_bootstrapChannel.LocalAddress} ");
        }

        /// <summary>
        /// 关闭服务
        /// </summary> 
        public async Task CloseAsync()
        {
            await _bootstrapChannel.CloseAsync();
            await _group.ShutdownGracefullyAsync();
        }

    }
}
