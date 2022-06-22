using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Coders
{
    /// <summary>
    /// 编码
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
    public class MitsubishiElipFrameEncoder : MessageToMessageEncoder<IByteBuffer>
    {
        private readonly ILogger<MitsubishiElipFrameEncoder> _logger;
        public MitsubishiElipFrameEncoder(ILogger<MitsubishiElipFrameEncoder> logger)
        {
            _logger = logger;
        }

        protected override void Encode(IChannelHandlerContext context, IByteBuffer message, List<object> output)
        {
            var header = new MitsubishiElipCommunicationHeader();
            header.ReadFromBuffer(message);

            //数据要能被4整除
            var dataLength = header.Assistant.Datas.Count();
            var dataLengthRemainder = (dataLength + 1) % 4;
            if (dataLengthRemainder != 0)
            {
                //数据长度
                var paddingLength = 4 - dataLengthRemainder;
                header.DataLength = (byte)(dataLength + 1 + paddingLength);
                _logger.LogInformation($"MitsubishiElipFrameEncoder数据增加长度：source:{dataLength} padding:{paddingLength} end:{ header.DataLength} ");

                //增加空byte  
                for (int i = 0; i < paddingLength; i++)
                {
                    header.Assistant.Datas.Add(0);
                }
            }

            //计算数据长度（真实数据+命令号）
            header.DataLength = (byte)(header.Assistant.Datas.Count + 1);

            //直接将对象传到下级继续处理
            var buffer = header.WriteToLocalBuffer();

            var bytes = new byte[buffer.WriterIndex];
            buffer.GetBytes(0, bytes);
            _logger.LogInformation($"{context.Channel.RemoteAddress} 发送数据：{bytes.ToCommaPrintString()} ");

            output.Add(buffer);
        }
    }
}
