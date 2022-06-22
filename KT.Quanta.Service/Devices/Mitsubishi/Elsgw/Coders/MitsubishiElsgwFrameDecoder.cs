using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Coders
{
    public class MitsubishiElsgwFrameDecoder : IMitsubishiElsgwFrameDecoder
    {
        private readonly ILogger<MitsubishiElsgwFrameDecoder> _logger;
        public MitsubishiElsgwFrameDecoder(ILogger<MitsubishiElsgwFrameDecoder> logger)
        {
            _logger = logger;
        }

        public MitsubishiElsgwTransmissionHeader Decode(DatagramPacket input)
        {
            //数据过长丢弃，防攻击
            if (MitsubishiElsgwHelper.DiscardMin < input.Content.ReadableBytes)
            {
                _logger.LogError($"MitsubishiFrameDecoder丢弃数据：{input.Content.ReadableBytes} ");
                input.Content.SkipBytes(input.Content.ReadableBytes);
                return null;
            }

            //判断表头是否正确，不正确去掉一个字符继续
            var header = input.Content.GetUnsignedShort(0);
            if (header != MitsubishiElsgwHelper.Header)
            {
                _logger.LogWarning($"MitsubishiFrameDecoder表头不正确：{header} ");
                input.Content.SkipBytes(1);
                return Decode(input);
            }

            //判断数据是否完整,不完整等待数据接收
            var dataLength = input.Content.GetUnsignedShort(2);
            var length = dataLength + 12;
            if (length > input.Content.ReadableBytes)
            {
                _logger.LogWarning($"MitsubishiFrameDecoder数据不完整：need:{length} readable:{input.Content.ReadableBytes} ");
                return null;
            }
            //数据长度过长丢弃数据
            if (length > MitsubishiElsgwHelper.DiscardMin)
            {
                _logger.LogError($"{input.Sender.AddressFamily} 丢弃数据：数据长度过长：{length} ");
                input.Content.SkipBytes(input.Content.ReadableBytes);
                return null;
            }

            //获取数据
            var bytes = new byte[length];
            input.Content.ReadBytes(bytes);

            _logger.LogInformation($"MitsubishiFrameDecoder接收数据：bytes:{bytes.ToCommaPrintString()} ");

            //转换成对象
            var result = new MitsubishiElsgwTransmissionHeader();
            result.ReadFromBytes(bytes);

            //输出
            return result;
        }
    }
}
