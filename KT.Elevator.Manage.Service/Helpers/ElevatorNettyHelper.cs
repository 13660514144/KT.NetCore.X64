using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Manage.Service.Handlers;
using log4net.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Helpers
{
    /// <summary>
    /// 电梯服务数据收发器
    /// </summary>
    public class ElevatorNettyHelper
    {
        /// <summary>
        /// 电梯服务
        /// </summary>
        public ElevatorServerModel ElevatorServer { get; set; }

        private MultithreadEventLoopGroup _eventLoopGroup { get; set; }
        private IPEndPoint _targetIPEndPoint { get; set; }
        private IChannel _clientChannel { get; set; }

        private ILogger<ElevatorNettyHelper> _logger;
        public ElevatorNettyHelper(ILogger<ElevatorNettyHelper> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 启动客户端连接
        /// </summary>
        /// <param name="elevatorType">电梯类型</param>
        /// <param name="ip">ip地址</param>
        /// <param name="port">端口</param>
        /// <returns></returns>
        public async Task RunClientAsync(ElevatorTypeEnum elevatorType, string ip, int port)
        {
            _targetIPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _eventLoopGroup = new MultithreadEventLoopGroup();

            try
            {
                var bootstrap = new Bootstrap();
                bootstrap
                    .Group(_eventLoopGroup)
                    .Channel<TcpSocketChannel>()
                    .Option(ChannelOption.TcpNodelay, true)
                    .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;

                        pipeline.AddLast(new LoggingHandler());
                        pipeline.AddLast("framing-enc", new LengthFieldPrepender(2));
                        pipeline.AddLast("framing-dec", new LengthFieldBasedFrameDecoder(ushort.MaxValue, 0, 2, 0, 2));

                        pipeline.AddLast("echo", new ElevatorNettyHandler(elevatorType));
                    }));

                _clientChannel = await bootstrap.ConnectAsync(_targetIPEndPoint);

                //Console.ReadLine();

                //await clientChannel.CloseAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("启动电梯客户端连接失败：{0} ", ex);
            }
        }

        public async Task CloseAsync()
        {
            //关闭连接
            await _clientChannel.CloseAsync();
            //关闭事件循环组
            await _eventLoopGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
        }
    }
}
