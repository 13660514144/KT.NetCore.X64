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
    public class QuantaClientFrameEncoder : MessageToMessageEncoder<QuantaNettyHeader>
    {
        private readonly ILogger<QuantaClientFrameEncoder> _logger;
        public QuantaClientFrameEncoder(ILogger<QuantaClientFrameEncoder> logger)
        {
            _logger = logger;
        }

        protected override void Encode(IChannelHandlerContext context, QuantaNettyHeader message, List<object> output)
        {
            message.Header = QuantaNettyHelper.DataHeader;
            message.DataLength = message.Datas.Length;

            var byteBuffer = message.WriteToLocalBuffer();

            //记录日记
            var bytes = new byte[byteBuffer.WriterIndex];
            byteBuffer.GetBytes(0, bytes);
            _logger.LogInformation($"{context.Channel.RemoteAddress} 发送数据：{bytes.ToCommaPrintString()} ");

            output.Add(byteBuffer);
        }
    }
}
