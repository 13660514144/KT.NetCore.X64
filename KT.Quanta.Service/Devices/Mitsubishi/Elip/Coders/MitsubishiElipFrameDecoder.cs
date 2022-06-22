using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Coders
{
    /// <summary> 
    /// 解码
    /// 
    /// +------------+-----------+-----------+------------+-----------------+
    /// | byte       | byte      |byte       | byte       |                 |
    /// | DataLength | Version   |Reserve1   | Reserve2   | Assistant Data  |
    /// |            |           |           |            |                 |
    /// +------------+-----------+-----------+------------+-----------------+
    /// length = 4 + Assistant Data Length
    /// 
    /// Assistant Data
    /// +-------------+-----------+
    /// |  byte       |           |
    /// |  Command    | Real Data |
    /// |             |           |
    /// +-------------+-----------+
    /// 
    /// </summary>
    public class MitsubishiElipFrameDecoder : MessageToMessageDecoder<IByteBuffer>
    {
        private readonly ILogger<MitsubishiElipFrameDecoder> _logger;
        public MitsubishiElipFrameDecoder(ILogger<MitsubishiElipFrameDecoder> logger)
        {
            _logger = logger;
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            //数据过长丢弃，防攻击
            if (MitsubishiElipHelper.DiscardMin < input.ReadableBytes)
            {
                _logger.LogError($"{context.Channel.RemoteAddress} MitsubishiElipFrameDecoder丢弃数据：{input.ReadableBytes} ");
                input.SkipBytes(input.ReadableBytes);
                return;
            }

            //判断数据是否完整,不完整等待数据接收
            var dataLength = input.GetByte(0);
            var length = dataLength + 4;
            if (length > input.ReadableBytes)
            {
                _logger.LogWarning($"{context.Channel.RemoteAddress} MitsubishiElipFrameDecoder数据不完整：need:{length} readable:{input.ReadableBytes} ");
                //不能等待，否则出错数据错位
                input.SkipBytes(input.ReadableBytes);
                return;
            }
            //数据长度过长丢弃数据
            if (length > MitsubishiElipHelper.DiscardMin)
            {
                _logger.LogError($"{context.Channel.RemoteAddress} 丢弃数据：数据长度过长：{length} ");

                input.SkipBytes(input.ReadableBytes);
                return;
            }

            //获取数据
            var bytes = new byte[length];
            input.ReadBytes(bytes);

            _logger.LogInformation($"{context.Channel.RemoteAddress} MitsubishiElipFrameDecoder接收数据：bytes:{bytes.ToCommaPrintString()} ");

            //转换成对象
            var result = new MitsubishiElipCommunicationHeader();
            result.ReadFromBytes(bytes);

            //输出
            output.Add(result);
        }
    }
}
