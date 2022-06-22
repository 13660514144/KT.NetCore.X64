using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.WpfApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Service.Network
{
    public class NettyServerHost : INettyServerHost
    {
        private IEventLoopGroup bossGroup;
        private IEventLoopGroup workerGroup;
        private ServerBootstrap bootstrap;
        private IChannel boundChannel;

        public NettyServerHost()
        {
        }

        public async Task RunAsync(int port)
        {
            bossGroup = new MultithreadEventLoopGroup(1);
            workerGroup = new MultithreadEventLoopGroup(2);
            bootstrap = new ServerBootstrap();

            bootstrap
                .Group(bossGroup, workerGroup)
                .Channel<TcpServerSocketChannel>()
                .Option(ChannelOption.SoBacklog, 100)
                .Handler(new LoggingHandler("SRV-LSTN"))
                .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;

                    pipeline.AddLast(new LoggingHandler("SRV-CONN"));
                    pipeline.AddLast("framing-enc", new LengthFieldPrepender(2));
                    pipeline.AddLast("framing-dec", new LengthFieldBasedFrameDecoder(ushort.MaxValue, 0, 2, 0, 2));

                    var nettyServerHandler = ContainerHelper.Resolve<NettyServerHandler>();
                    pipeline.AddLast("echo", nettyServerHandler);
                }));

            boundChannel = await bootstrap.BindAsync(port);
        }

        public async Task CloseAsync()
        {
            await boundChannel.CloseAsync();

            await Task.WhenAll(
                bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
        }

    }
}
