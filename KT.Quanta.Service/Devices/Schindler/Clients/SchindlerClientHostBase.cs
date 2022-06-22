using ContralServer.CfgFileRead;
using DotNetty.Buffers;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Schindler.Models;
using KT.Quanta.Service.Dtos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler.Clients
{
    /// <summary>
    /// socket客户端
    /// </summary>
    public abstract class SchindlerClientHostBase : ISchindlerClientHostBase
    {
        private IPAddress _remoteIp;
        private int _remotePort;
        private IChannel _clientChannel;
        private MultithreadEventLoopGroup _multithreadEventLoopGroup;
        private Bootstrap _bootstrap;
        private int WaitSleep = Convert.ToInt32(AppConfigurtaionServices.Configuration["WaitSleep"].ToString());
        public CommunicateDeviceInfoModel CommunicateDeviceInfo { get; set; }

        private readonly ILogger<SchindlerClientHostBase> _logger;
        private IServiceProvider _serviceProvider;

        public SchindlerClientHostBase(ILogger<SchindlerClientHostBase> logger,
             IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ipAddress">ip地址</param>
        /// <param name="port">端口</param>
        public Task InitAsync(string ipAddress, int port)
        {
            _remoteIp = IPAddress.Parse(ipAddress);
            _remotePort = port;

            _multithreadEventLoopGroup = new MultithreadEventLoopGroup(2);
            _bootstrap = new Bootstrap();

            _bootstrap.Group(_multithreadEventLoopGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;

                    pipeline.AddLast(new LoggingHandler());

                    //表头不要过滤
                    //pipeline.AddLast("framing-enc", new LengthFieldPrepender(2));
                    //pipeline.AddLast("framing-dec", new LengthFieldBasedFrameDecoder(ushort.MaxValue, 0, 2, 0, 2));

                    var schindlerClientHandler = GetClientHandler();
                    schindlerClientHandler.CommunicateDeviceInfo = CommunicateDeviceInfo;
                    pipeline.AddLast("echo", schindlerClientHandler);
                }));

            _logger.LogInformation($"Schindler初始化Socket客户端成功：serverIp:{_remoteIp} serverPort:{_remotePort} ");

            return Task.CompletedTask;
        }

        public abstract ISchindlerClientHandlerBase GetClientHandler();

        /// <summary>
        /// 连接
        /// </summary>
        public async Task ConnectAsync()
        {
            _logger.LogInformation($"{_remoteIp.AddressFamily}:{_remotePort} Quanta开启Socket客户端连接！");
            _clientChannel = await _bootstrap.ConnectAsync(new IPEndPoint(_remoteIp, _remotePort));
            _logger.LogInformation($"Schindler开启Socket客户端连接成功：serverIp:{_remoteIp} serverPort:{_remotePort} ");
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public async Task CloseAsync()
        {
            if (_clientChannel != null)
            {
                await _clientChannel.CloseAsync();
            }

            //await _multithreadEventLoopGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            _logger.LogInformation($"Schindler关闭Socket客户端成功：serverIp:{_remoteIp} serverPort:{_remotePort} ");
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息数据</param>
        public async Task SendAsync(byte[] bytes)
        {
            //Thread.Sleep(WaitSleep);
            var byteBuffer = Unpooled.Buffer(bytes.Length);
            byteBuffer.WriteBytes(bytes);
            if (_clientChannel == null || !_clientChannel.Active)
            {
                await ConnectAsync();
            }
            _logger.LogInformation($"{_clientChannel.RemoteAddress} 发送数据：data:{bytes.ToCommaPrintString()} ");
            await _clientChannel.WriteAndFlushAsync(byteBuffer);
        }

        /// <summary>
        /// 检查是否在线并连接
        /// </summary>
        /// <returns>是否在线</returns>
        public async Task<bool> CheckAndLinkAsync()
        {
            if (!(_clientChannel?.Active == true))
            {
                await ConnectAsync();
            }
            return _clientChannel.Active;
        }

        /// <summary>
        /// 心跳
        /// </summary>
        /// <returns></returns>
        public async Task HeartbeatAsync()
        {
            var data = new SchindlerDatabaseHeartbeatRequest();
            var byteBuffer = data.WriteToLocalBuffer();
            if (_clientChannel?.Active == true)
            {
                _logger.LogInformation($"发送数据-心跳：address:{_clientChannel.RemoteAddress} data:{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");
                await _clientChannel.WriteAndFlushAsync(byteBuffer);
            }
            else
            {
                _logger.LogWarning($"schindler设备离线！");
            }
        }
    }
}
