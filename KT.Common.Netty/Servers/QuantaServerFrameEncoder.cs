using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using KT.Common.Core.Utils;
using KT.Common.Netty.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Netty.Servers
{
    public class QuantaServerFrameEncoder : MessageToMessageEncoder<QuantaNettyHeader>
    {
        private readonly ILogger<QuantaServerFrameEncoder> _logger;
        public QuantaServerFrameEncoder(ILogger<QuantaServerFrameEncoder> logger)
        {
            _logger = logger;
        }

        protected override void Encode(IChannelHandlerContext context, QuantaNettyHeader message, List<object> output)
        {
            message.Header = QuantaNettyHelper.DataHeader;
            message.DataLength = message.Datas.Length;

            var byteBuffer = message.WriteToLocalBuffer();

            var bytes = new byte[byteBuffer.WriterIndex];
            byteBuffer.GetBytes(0, bytes);
            _logger.LogInformation($"{context.Channel.RemoteAddress} 发送数据：{bytes.ToCommaPrintString()} ");


            output.Add(byteBuffer);
        }
    }
}
