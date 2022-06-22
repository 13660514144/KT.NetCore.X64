using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Coders;
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
    public class MitsubishiElsgwUdpClientHostBase : IMitsubishiElsgwClientHostBase
    {
        //private IPAddress _ipAddress;
        //private int _port;
        private IChannel _clientChannel;
        private MultithreadEventLoopGroup _multithreadEventLoopGroup;
        private Bootstrap _bootstrap;

        private EndPoint _remoteAddress;

        private readonly IServiceProvider _serviceProvider;
        private readonly IMitsubishiElsgwFrameEncoder _mitsubishiElsgwFrameEncoder;

        private readonly ILogger<MitsubishiElsgwUdpClientHostBase> _logger;
        public MitsubishiElsgwUdpClientHostBase(ILogger<MitsubishiElsgwUdpClientHostBase> logger,
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
        /// <param name="ipAddress">ip地址</param>
        /// <param name="port">端口</param>
        public async Task InitAsync(string ipAddress, int port)
        {
            _remoteAddress = new IPEndPoint(IPAddress.Parse(ipAddress), port);

            _multithreadEventLoopGroup = new MultithreadEventLoopGroup(2);
            _bootstrap = new Bootstrap();

            _bootstrap.Group(_multithreadEventLoopGroup)
                .Channel<SocketDatagramChannel>()
                .Option(ChannelOption.SoBroadcast, true)
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    //pipeline.AddLast(new LoggingHandler());

                    ////编码
                    //var assistantEncoder = _serviceProvider.GetRequiredService<MitsubishiElsgwAssistantEncoder>();
                    //var frameEncoder = _serviceProvider.GetRequiredService<MitsubishiElsgwFrameEncoder>();
                    //pipeline.AddLast("assistant-encoder", assistantEncoder);
                    //pipeline.AddLast("frame-encoder", frameEncoder);

                    ////解码
                    //var frameDecoder = _serviceProvider.GetRequiredService<MitsubishiElsgwFrameDecoder>();
                    //pipeline.AddLast("frame-decoder", frameDecoder);

                    //数据解析
                    var _mitsubishiClientHandler = _serviceProvider.GetRequiredService<IMitsubishiElsgwClientHandler>();
                    pipeline.AddLast("echo", _mitsubishiClientHandler);
                }));
            //IPAddress.Any,IPEndPoint.MinPortIPAddress.Parse("192.168.0.183"),

            _clientChannel = await _bootstrap.BindAsync(52000);

            _logger.LogInformation($"{_remoteAddress.AddressFamily} MitsubishiElsgw初始化Socket客户端成功：local:{52000} ");
        }

        /// <summary>
        /// 连接
        /// </summary>
        public Task ConnectAsync()
        {
            _logger.LogInformation($"{_remoteAddress.AddressFamily} MitsubishiElsgw开启Socket客户端UDP不需要连接！");

            return Task.CompletedTask;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public async Task CloseAsync()
        {
            await _clientChannel.CloseAsync();
            await _multithreadEventLoopGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            _logger.LogInformation($"{_remoteAddress.AddressFamily} MitsubishiElsgw关闭Socket客户端成功！");
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="bytes">消息数据</param>
        public async Task SendAsync(MitsubishiElsgwTransmissionHeader header)
        {
            header.Assistant.DataLength = (byte)header.Assistant.Datas.Count();
            header.DataLength = (byte)(header.Assistant.DataLength + 2);

            var byteBuffer = header.WriteToLocalBuffer();

            _logger.LogInformation($"{_remoteAddress.AddressFamily} MitsubishiElsgw发送数据：data:{JsonConvert.SerializeObject(header, JsonUtil.JsonPrintSettings)} ");

            //解码器解析，解码器失灵
            byteBuffer = _mitsubishiElsgwFrameEncoder.HeaderEncode(_mitsubishiElsgwFrameEncoder.AssistantEncode(byteBuffer));

            await _clientChannel.WriteAndFlushAsync(new DatagramPacket(byteBuffer, _remoteAddress));
        }

        public Task<bool> CheckAndLinkAsync()
        {
            return Task.FromResult(true);
        }

        public Task HeartbeatAsync()
        {
            //var heartbeat = new MitsubishiElsgwHeartbeatModel();

            //var header = new MitsubishiElsgwTransmissionHeader();
            //header.Assistant.CommandNumber = (byte)MitsubishiElsgwCommandEnum.Heartbeat.Code;
            //header.Assistant.Datas = heartbeat.GetBytes().ToList();
            //header.Assistant.DataLength = (byte)header.Assistant.Datas.Count();
            //header.DataLength = (byte)(header.Assistant.DataLength + 2);

            //var byteBuffer = header.WriteToLocalBuffer();

            //_logger.LogInformation($"发送数据-心跳：address:{_remoteAddress.AddressFamily} " +
            //    $"data:{JsonConvert.SerializeObject(header, JsonUtil.JsonPrintSettings)} " +
            //    $"bytes:{header.Assistant.Datas.ToCommaPrintString()}");

            ////解码器解析，解码器失灵
            //byteBuffer = HeaderEncode(AssistantEncode(byteBuffer));

            //await _clientChannel.WriteAndFlushAsync(new DatagramPacket(byteBuffer, _remoteAddress));

            return Task.CompletedTask;
        }
    }
}
