using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using KT.Common.Core.Utils;
using KT.Common.Netty.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Netty.Clients
{
    /// <summary>
    /// head 80
    /// </summary>
    public class QuantaClientFrameDecoder : ByteToMessageDecoder
    {
        private readonly ILogger<QuantaClientFrameDecoder> _logger;
        public QuantaClientFrameDecoder(ILogger<QuantaClientFrameDecoder> logger)
        {
            _logger = logger;
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            //数据过多，防攻击
            if (input.ReadableBytes > QuantaNettyHelper.ReceiveMaxLength)
            {
                _logger.LogWarning($"{context.Channel.RemoteAddress} 丢弃数据：{input.ReadableBytes} ");

                input.SkipBytes(input.ReadableBytes);
                return;
            }

            //较验数据头长度
            if (input.ReadableBytes <= QuantaNettyHelper.HeaderLength)
            {
                _logger.LogWarning($"{context.Channel.RemoteAddress} 数据头不完整：" +
                    $"need:{QuantaNettyHelper.HeaderLength} " +
                    $"readable:{input.ReadableBytes} ");

                return;
            }

            //获取数据长度
            var dataLength = input.GetInt(75);
            var length = dataLength + QuantaNettyHelper.HeaderLength;

            //较验数据长度
            if (input.ReadableBytes < length)
            {
                _logger.LogWarning($"{context.Channel.RemoteAddress} 数据不完整：" +
                    $"need:{length} " +
                    $"readable:{input.ReadableBytes} ");

                return;
            }

            //较验表头是否正确
            var header = input.GetString(0, 32, QuantaNettyHelper.Encoding);
            if (header != QuantaNettyHelper.DataHeader)
            {
                _logger.LogWarning($"{context.Channel.RemoteAddress} 数据表头不正确：need:{QuantaNettyHelper.DataHeader} receive:{header} ");

                input.SkipBytes(1);
                return;
            }

            //获取数据
            var byteBuffer = input.ReadBytes(length);

            //记录日记
            var bytes = new byte[length];
            byteBuffer.GetBytes(0, bytes);
            _logger.LogInformation($"接收数据：{bytes.ToCommaPrintString()} ");

            //返回结果
            var result = new QuantaNettyHeader();
            result.ReadFromBuffer(byteBuffer);

            output.Add(result);
        }
    }
}
