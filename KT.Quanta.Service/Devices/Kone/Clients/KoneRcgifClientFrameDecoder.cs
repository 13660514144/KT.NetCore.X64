using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using KT.Common.Core.Utils;
using KT.Common.Netty.Common;
using KT.Quanta.Service.Devices.Kone.Helpers;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Devices.Kone.Requests;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Clients
{
    public class KoneRcgifClientFrameDecoder : ByteToMessageDecoder
    {
        private readonly ILogger<KoneRcgifClientFrameDecoder> _logger;
        private readonly KoneSettings _koneSettings;

        public KoneRcgifClientFrameDecoder(ILogger<KoneRcgifClientFrameDecoder> logger,
            IOptions<KoneSettings> koneSettings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<KoneRcgifClientFrameDecoder>));
            _koneSettings = koneSettings?.Value ?? throw new ArgumentNullException(nameof(IOptions<KoneSettings>));
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            var readableBytes = new byte[input.ReadableBytes];
            input.GetBytes(0, readableBytes);
            _logger.LogInformation($"{context.Channel.RemoteAddress} 接收总数据：{readableBytes.ToCommaPrintString()} ");

            //数据过多，防攻击
            if (input.ReadableBytes > _koneSettings.Rcgif.DiscardMinimumLength)
            {
                _logger.LogError($"{context.Channel.RemoteAddress} 丢弃数据：{input.ReadableBytes} ");

                input.SkipBytes(input.ReadableBytes);
                return;
            }

            //较验数据头长度
            if (input.ReadableBytes <= KoneRcgifHelper.HeaderLength)
            {
                _logger.LogWarning($"{context.Channel.RemoteAddress} 数据头不完整：" +
                    $"need:{QuantaNettyHelper.HeaderLength} " +
                    $"readable:{input.ReadableBytes} ");

                return;
            }

            //获取数据长度
            int length;
            var messageByteOrder = input.GetByte(0);
            if (messageByteOrder == KoneRcgifMessageByteOrderEnum.BYTE_ORDER_BIG_ENDIAN.Code)
            {
                length = input.GetInt(1);
            }
            else if (messageByteOrder == KoneRcgifMessageByteOrderEnum.BYTE_ORDER_LITTLE_ENDIAN.Code)
            {
                length = input.GetIntLE(1);
            }
            else
            {
                _logger.LogError($"{context.Channel.RemoteAddress} 数据表头不正确：need:0x80 or 0x81 receive:{Convert.ToString((int)messageByteOrder, 16)} ");
                input.SkipBytes(input.ReadableBytes);
                return;
            }

            //较验数据长度
            if (input.ReadableBytes < length)
            {
                _logger.LogWarning($"{context.Channel.RemoteAddress} 数据不完整：" +
                    $"need:{length} " +
                    $"readable:{input.ReadableBytes} ");

                return;
            }
            //数据长度过长丢弃数据
            if (length > _koneSettings.Rcgif.DiscardMinimumLength)
            {
                _logger.LogError($"{context.Channel.RemoteAddress} 丢弃数据：数据长度过长：{length} ");

                input.SkipBytes(input.ReadableBytes);
                return;
            }

            //获取数据
            var byteBuffer = input.ReadBytes(length);
            var bytes = new byte[length];
            byteBuffer.GetBytes(0, bytes);

            _logger.LogInformation($"{context.Channel.RemoteAddress} 接收数据：{bytes.ToCommaPrintString()} ");

            //丢掉剩余数据
            if (input.ReadableBytes > 0)
            {
                var removeByteBuffer = input.ReadBytes(input.ReadableBytes);
                var removeBytes = new byte[input.ReadableBytes];
                removeByteBuffer.GetBytes(0, removeBytes);
                _logger.LogInformation($"{context.Channel.RemoteAddress} 丢掉剩余数据：{removeBytes.ToCommaPrintString()} ");
            }

            //返回结果
            var result = new KoneRcgifHeaderResponse();
            result.ReadFromBuffer(byteBuffer, messageByteOrder == KoneRcgifMessageByteOrderEnum.BYTE_ORDER_LITTLE_ENDIAN.Code);

            output.Add(result);
        }
    }
}
