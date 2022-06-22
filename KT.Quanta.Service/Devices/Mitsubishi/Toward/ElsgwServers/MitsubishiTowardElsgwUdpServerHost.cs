using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Coders;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Toward.ElsgwServers
{
    /// <summary>
    /// socket客户端
    /// </summary>
    public class MitsubishiTowardElsgwUdpServerHost : IMitsubishiTowardElsgwUdpServerHost
    {
        private IChannel _serverChannel;
        private MultithreadEventLoopGroup _multithreadEventLoopGroup;
        private Bootstrap _bootstrap;

        private readonly IServiceProvider _serviceProvider;
        private readonly IMitsubishiElsgwFrameEncoder _mitsubishiElsgwFrameEncoder;
        private readonly ILogger<MitsubishiTowardElsgwUdpServerHost> _logger;

        public IMitsubishiElipClientHostBase MitsubishiElipClientHost { get; set; }

        public MitsubishiTowardElsgwUdpServerHost(ILogger<MitsubishiTowardElsgwUdpServerHost> logger,
            IServiceProvider serviceProvider,
            IMitsubishiElsgwFrameEncoder mitsubishiElsgwFrameEncoder)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _mitsubishiElsgwFrameEncoder = mitsubishiElsgwFrameEncoder;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public async Task InitAsync(int port)
        {
            _multithreadEventLoopGroup = new MultithreadEventLoopGroup(2);
            _bootstrap = new Bootstrap();

            _bootstrap.Group(_multithreadEventLoopGroup)
                .Channel<SocketDatagramChannel>()
                .Option(ChannelOption.SoBroadcast, true)
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    //pipeline.AddLast(new LoggingHandler());

                    //数据解析
                    var _mitsubishiTowardElsgwServerHandler = GetChannelHandler();
                    pipeline.AddLast("echo", _mitsubishiTowardElsgwServerHandler);
                }));

            _serverChannel = await _bootstrap.BindAsync(port);

            _logger.LogInformation($"MitsubishiTowardUdpServerHost初始化Socket服务端成功！");
        }

        public IChannelHandler GetChannelHandler()
        {
            var _mitsubishiTowardElsgwServerHandler = _serviceProvider.GetRequiredService<IMitsubishiTowardElsgwServerHandler>();
            _mitsubishiTowardElsgwServerHandler.MitsubishiTowardElsgwRequestHandler = _serviceProvider.GetRequiredService<IMitsubishiTowardElsgwRequestHandler>();
            _mitsubishiTowardElsgwServerHandler.MitsubishiTowardElsgwRequestHandler.MitsubishiElipClientHost = MitsubishiElipClientHost;
            return _mitsubishiTowardElsgwServerHandler;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public async Task CloseAsync()
        {
            await _serverChannel.CloseAsync();
            await _multithreadEventLoopGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            _logger.LogInformation($"MitsubishiTowardUdpServerHost关闭Socket服务端成功！");
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="bytes">消息数据</param>
        public async Task SendAsync(MitsubishiElsgwTransmissionHeader header, EndPoint endPoint)
        {
            header.Assistant.DataLength = (byte)header.Assistant.Datas.Count();
            header.DataLength = (byte)(header.Assistant.DataLength + 2);

            var byteBuffer = header.WriteToLocalBuffer();

            _logger.LogInformation($"{endPoint.AddressFamily} MitsubishiElsgwServer发送数据：data:{JsonConvert.SerializeObject(header, JsonUtil.JsonPrintSettings)} ");

            //解码器解析，解码器失灵
            byteBuffer = _mitsubishiElsgwFrameEncoder.HeaderEncode(_mitsubishiElsgwFrameEncoder.AssistantEncode(byteBuffer));

            await _serverChannel.WriteAndFlushAsync(new DatagramPacket(byteBuffer, endPoint));
        }


    }
}
