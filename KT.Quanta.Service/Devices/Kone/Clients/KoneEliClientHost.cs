using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Kone.Helpers;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Devices.Kone.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone.Clients
{
    /// <summary>
    /// socket客户端
    /// </summary>
    public class KoneEliClientHost : IKoneEliClientHost
    {
        private IPAddress _remoteIp;
        private int _remotePort;
        private IChannel _clientChannel;
        private MultithreadEventLoopGroup _multithreadEventLoopGroup;
        private Bootstrap _bootstrap;
        private long _receiveHeartbeatTime;
        private bool _isHostClosing = false;
        private readonly ILogger<KoneEliClientHost> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly KoneSettings _koneSettings;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public KoneEliClientHost(ILogger<KoneEliClientHost> logger,
             IServiceProvider serviceProvider,
            IOptions<KoneSettings> koneSettings,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<KoneEliClientHost>));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(IServiceProvider));
            _koneSettings = koneSettings?.Value ?? throw new ArgumentNullException(nameof(IOptions<KoneSettings>));
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(IServiceScopeFactory));
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

            _multithreadEventLoopGroup = new MultithreadEventLoopGroup(_koneSettings.Eli.EventLoopGroupCount);
            _bootstrap = new Bootstrap();

            _bootstrap.Group(_multithreadEventLoopGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;

                    pipeline.AddLast(new LoggingHandler());

                    //编码器
                    var koneEliClientFrameEncoder = _serviceProvider.GetRequiredService<KoneEliClientFrameEncoder>();
                    pipeline.AddLast("framing-enc", koneEliClientFrameEncoder);

                    //解码器
                    var koneEliClientFrameDecoder = _serviceProvider.GetRequiredService<KoneEliClientFrameDecoder>();
                    pipeline.AddLast("framing-dec", koneEliClientFrameDecoder);

                    //处理数据
                    var koneClientHandler = _serviceProvider.GetRequiredService<IKoneEliClientHandler>();
                    koneClientHandler.SetReceiveHeartbeatTimeHandler = SetReceiveHeartbeatTime;
                    pipeline.AddLast("echo", koneClientHandler);
                }));

            _logger.LogInformation($"Kone初始化Socket客户端成功：serverIp:{_remoteIp} serverPort:{_remotePort} ");

            return Task.CompletedTask;
        }

        /// <summary>
        /// 接收到心跳设置接收心跳时间
        /// </summary>
        public void SetReceiveHeartbeatTime()
        {
            _receiveHeartbeatTime = DateTimeUtil.UtcNowMillis();
            _logger.LogInformation($"更新接收时间：serverIp:{_remoteIp} serverPort:{_remotePort} receiveTime:{_receiveHeartbeatTime} ");
        }

        /// <summary>
        /// 连接
        /// </summary>
        public async Task ConnectAsync()
        {
            _logger.LogInformation($"{_remoteIp}:{_remotePort} Kone开启Socket客户端连接！");
            _clientChannel = await _bootstrap.ConnectAsync(new IPEndPoint(_remoteIp, _remotePort));
            _logger.LogInformation($"{_remoteIp}:{_remotePort} Kone开启Socket客户端连接成功！");

            using var scope = _serviceScopeFactory.CreateScope();
            var koneService = scope.ServiceProvider.GetRequiredService<IKoneService>();
            await koneService.SetDopMaskAsync(_remoteIp.ToString(), _remotePort);

            _receiveHeartbeatTime = DateTimeUtil.UtcNowMillis();
        }



        /// <summary>
        /// 关闭
        /// </summary>
        public async Task CloseAsync()
        {
            //发送断开连接
            await DisconnectAsync();

            await Task.Delay(_koneSettings.Eli.CloseDelaySeconds * 1000);

            //断开连接
            if (_clientChannel != null)
            {
                await _clientChannel.CloseAsync();
                await _multithreadEventLoopGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            }
            _logger.LogInformation($"Kone关闭Socket客户端成功：serverIp:{_remoteIp} serverPort:{_remotePort} ");

            _receiveHeartbeatTime = 0;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息数据</param>
        public async Task SendAsync(KoneEliHeaderRequest request)
        {
            if (_clientChannel == null || !_clientChannel.Active)
            {
                await ConnectAsync();
            }
            await _clientChannel.WriteAndFlushAsync(request);
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
            if (_isHostClosing)
            {
                _logger.LogWarning($"服务在在关闭，不再发送心中包：address:{_clientChannel?.RemoteAddress}");
                return;
            }

            var data = new KoneEliHeartbeatMessageRequest();
            if (_clientChannel?.Active == true)
            {
                _logger.LogInformation($"发送数据-心跳：address:{_clientChannel.RemoteAddress} data:{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");
                await _clientChannel.WriteAndFlushAsync(data);
            }
            else if (_clientChannel != null)
            {
                _logger.LogWarning($"kone设备离线：{_clientChannel.RemoteAddress} ");
            }
            else
            {
                _logger.LogWarning($"kone设备通道为空：{_remoteIp}:{_remotePort} ");
            }

            //检测是否收到心跳，收不到断开重连 
            var receiveTimeAdded = _receiveHeartbeatTime + (60 * 1000);
            var nowMillis = DateTimeUtil.UtcNowMillis();

            if (_koneSettings.Eli.IsNoHeartbeatReconnect
                && _receiveHeartbeatTime > 0
                && receiveTimeAdded < nowMillis)
            {
                _logger.LogInformation($"接收不到心跳重新连接：receiveTime:{_receiveHeartbeatTime} receiveTimeAdded:{receiveTimeAdded} nowMillis:{nowMillis} ");

                await NoHeartbeatReconnectAsync();
            }
        }

        /// <summary>
        /// 重新连接
        /// </summary>
        public async Task NoHeartbeatReconnectAsync()
        {
            _logger.LogError($"接收不到心跳重新连接：address:{_clientChannel.RemoteAddress} ");

            if (_clientChannel == null || !_clientChannel.Active)
            {
                await ConnectAsync();
                return;
            }
            _receiveHeartbeatTime = DateTimeUtil.UtcNowMillis();

            var data = new KoneEliDisconnectMessageRequest();
            data.Reason = (ushort)KoneEliDisconnectReasonEnum.NO_HEARTBEAT.Code;

            _logger.LogInformation($"发送数据-断开连接：address:{_clientChannel.RemoteAddress} data:{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");
            await _clientChannel.WriteAndFlushAsync(data);

            try
            {
                // 延迟断开时间 
                await Task.Delay(_koneSettings.Eli.CloseDelaySeconds * 1000);

                //断开连接
                await _clientChannel.CloseAsync();

                // 延迟连接时间 
                await Task.Delay(_koneSettings.Eli.ReconnectDelaySeconds * 1000);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Kone关闭失败：{_clientChannel?.RemoteAddress} ");
            }

            await ConnectAsync();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        private async Task DisconnectAsync()
        {
            if (_clientChannel == null || !_clientChannel.Active)
            {
                _logger.LogWarning($"连接已断开：address:{_clientChannel?.RemoteAddress}");
                return;
            }

            var data = new KoneEliDisconnectMessageRequest();
            data.Reason = (ushort)KoneEliDisconnectReasonEnum.HOST_SHUTTING_DOWN.Code;
            _logger.LogInformation($"发送数据-断开连接：address:{_clientChannel.RemoteAddress} data:{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");
            await _clientChannel.WriteAndFlushAsync(data);

            _isHostClosing = true;
        }
    }
}
