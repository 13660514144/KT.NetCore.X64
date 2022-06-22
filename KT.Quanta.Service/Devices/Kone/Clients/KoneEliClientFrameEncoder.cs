using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using KT.Common.Core.Utils;
using KT.Common.Netty.Common;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Devices.Kone.Requests;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace KT.Quanta.Service.Devices.Kone.Clients
{
    public class KoneEliClientFrameEncoder : MessageToMessageEncoder<KoneEliHeaderRequest>
    {
        private readonly ILogger<KoneEliClientFrameEncoder> _logger;
        public KoneEliClientFrameEncoder(ILogger<KoneEliClientFrameEncoder> logger)
        {
            _logger = logger;
        }

        protected override void Encode(IChannelHandlerContext context, KoneEliHeaderRequest message, List<object> output)
        {
            var isLittleEndianess = message.IsLittleEndianess();
            //获取发送内容  
            var byteBuffer = message.WriteToLocalBuffer(isLittleEndianess);
            var bytes = new byte[byteBuffer.WriterIndex];
            byteBuffer.GetBytes(0, bytes);

            //修改长度再获取一遍发送内容
            message.MessageSize = (uint)bytes.Length;
            byteBuffer = message.WriteToLocalBuffer(isLittleEndianess);
            bytes = new byte[byteBuffer.WriterIndex];
            byteBuffer.GetBytes(0, bytes);

            _logger.LogInformation($"{context.Channel.RemoteAddress} 发送数据：{bytes.ToCommaPrintString()} ");

            output.Add(byteBuffer);
        }
    }
}
