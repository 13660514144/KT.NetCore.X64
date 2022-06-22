using DotNetty.Buffers;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Coders
{
    public class MitsubishiElsgwFrameEncoder : IMitsubishiElsgwFrameEncoder
    {
        private readonly ILogger<MitsubishiElsgwFrameEncoder> _logger;
        public MitsubishiElsgwFrameEncoder(ILogger<MitsubishiElsgwFrameEncoder> logger)
        {
            _logger = logger;
        }

        public MitsubishiElsgwTransmissionHeader AssistantEncode(IByteBuffer message)
        {
            var header = new MitsubishiElsgwTransmissionHeader();
            header.ReadFromBuffer(message);

            //数据要能被4整除
            var dataLength = header.Assistant.Datas.Count();
            var dataLengthRemainder = (dataLength + 2) % 4;
            if (dataLengthRemainder != 0)
            {
                //数据长度
                var paddingLength = 4 - dataLengthRemainder;
                header.Assistant.DataLength = (byte)(dataLength + paddingLength);

                _logger.LogInformation($"MitsubishiAssistantEncoder数据增加长度：source:{dataLength} padding:{paddingLength} end:{header.Assistant.DataLength} ");

                //增加空byte  
                for (int i = 0; i < paddingLength; i++)
                {
                    header.Assistant.Datas.Add(0);
                }
            }
            else
            {
                header.Assistant.DataLength = (byte)dataLength;
            }

            //直接将对象传到下级继续处理
            return header;
        }

        public IByteBuffer HeaderEncode(MitsubishiElsgwTransmissionHeader message)
        {
            message.Identifier = MitsubishiElsgwHelper.Header;
            message.DataLength = (ushort)(message.Assistant.DataLength + 2);

            var buffer = message.WriteToLocalBuffer();

            var bytes = new byte[buffer.WriterIndex];
            buffer.GetBytes(0, bytes);

            _logger.LogInformation($" MitsubishiElsgw发送数据：{bytes.ToCommaPrintString()} ");

            return buffer;
        }
    }
}
