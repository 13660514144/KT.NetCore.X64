using KT.Common.Core.Utils;
using KT.Device.Unit.CardReaders.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KT.Device.Unit.CardReaders.Datas
{
    /// <summary>
    /// STX         1       0xa8 -  ‘起始字节’ – 标准控制字节.表示一个数据包的开始
    /// SEQ[]       1       随机码 预留地址位用于处理设备地址超过255.
    /// DADD        1       设备地址用于多台机子通信，只有地址匹配才能进行数据通信，0x00与0xFF地址为广播地址。  
    /// CMD         1       命令码上位机发给下位机的命令一个字节。  
    /// DATA LENGTH 2       数据长度包括TIME/STATUS +DATA field       高字节在前，低字节在后
    /// // STATUS       1       下位机回复状态，一个字节            00表示命令正确执行其他为错误码
    /// // TIME     1       用于特定命令时间控制，超时处理，其他命令（大部分）该参数为0
    /// DATA[0-N]   2000    上位机发送时作为命令参数，下位机发送时作为返回数据，长度可变。长度最大值为512，超出范围时不做处理直接回复/显示命令过长等待下一条命令。   
    /// BCC         1       异或校验位，对数据进行校验但不包含STX和ETX
    /// ETX         1       0xa9 -  ‘起终止字节’ – 标准控制字节.表示一个数据包的结束
    /// 
    /// 224047586  
    /// A8 00 00 00 00 05 00 0D 5A B1 E2 01 A9 
    /// 00 01 02 03 04 05 06 07 08 09 10 11 12
    /// </summary>
    public class QiacsR992CardDeviceAnalyze : IQiacsR992CardDeviceAnalyze
    {
        private List<byte> _bufferBytes = new List<byte>();

        private readonly ILogger _logger;
        public QiacsR992CardDeviceAnalyze(ILogger logger)
        {
            _logger = logger;
        }

        public CardReceiveModel Analyze(string protName, byte[] datas)
        {
             //_logger.LogInformation($"串口接收数据1：portName:{protName} datas:{datas.ToCommaPrintString()} ");
             //队列的形式读取
             if (datas != null && datas.Count() > 0)
             {
                 datas.ToList().ForEach(x => _bufferBytes.Add(x));
             }
             //_logger.LogInformation($"串口接收总数据：portName:{protName} bytes:{_bufferBytes.ToCommaPrintString()} ");

             if (_bufferBytes.Count() <= 0)
             {
                 //_logger.LogInformation($"串口接收总数据为空：portName:{protName} ");
                 return null;
             }

             var result = new CardReceiveModel();
             // 0xa0 = 168
             if (_bufferBytes[0] == 168)
             {
                 //数据长度为6 + Data Length + 标识符2
                 if (_bufferBytes.Count() < 6)
                 {
                     //_logger.LogInformation($"串口接收总数据：头长度不足1 length:{_bufferBytes.Count()} need:{8} ");
                     return null;
                 }

                 //数据长度为6 + Data Length + 标识符2
                 var dataLength = BitConverter.ToUInt16(new byte[2] { _bufferBytes[5], _bufferBytes[4] });
                 var totalDataLength = 6 + dataLength + 2;
                 if (_bufferBytes.Count() < totalDataLength)
                 {
                     //_logger.LogInformation($"串口接收总数据：长度异常 length:{_bufferBytes.Count()} neet:{dataLength} ");
                     // 长度最大值为20,超出长度重新识别 
                     if (_bufferBytes.Count() > 20)
                     {
                         _bufferBytes.Clear();
                         return null;
                     }
                     else if (dataLength > 20)
                     {
                         _bufferBytes.RemoveAt(0);
                         return Analyze(protName, null);
                     }
                 }

                 //获取
                 byte[] totalDataBytes = GetTotalDataBytes(totalDataLength);
                 //获取ic卡数据
                 var icCardBytes = new byte[4]
                 {
                     totalDataBytes[dataLength+6 -1],
                     totalDataBytes[dataLength+6 -2],
                     totalDataBytes[dataLength+6 -3],
                     totalDataBytes[dataLength+6 -4],
                 };

                 var value = BitConverter.ToUInt32(icCardBytes);
                 _logger.LogInformation($"串口接收真实数据：data:{value} ");

                 //卡号去掉左边0
                 result.CardNumber = value.ToString();
                 result.IsCheckDate = false;
                 result.AccessType = "IC_CARD";
             }
             else
             {
                 if (_bufferBytes.Count() > 1)
                 {
                     _bufferBytes.RemoveAt(0);
                     return Analyze(protName, null);
                 }
                 return null;
             }

             //_logger.LogInformation($"串口接收数据3：data:{JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
             return result;            
        }

        private byte[] GetTotalDataBytes(int totalDataLength)
        {
            /*var resultDataBytes = new byte[totalDataLength];
            for (int i = totalDataLength - 1; i >= 0; i--)
            {
                resultDataBytes[i] = _bufferBytes[i];
                _bufferBytes.RemoveAt(i);
            }
            _logger.LogInformation($"串口接收解析前数据数据：data:{resultDataBytes.ToCommaPrintString()} ");

            return resultDataBytes;*/
            var resultDataBytes = new byte[totalDataLength];
            for (int i = totalDataLength - 1; i >= 0; i--)
            {
                resultDataBytes[i] = _bufferBytes[i];
                _bufferBytes.RemoveAt(i);
            }
            _logger.LogInformation($"串口接收解析前数据数据：data:{resultDataBytes.ToCommaPrintString()} ");

            return resultDataBytes;
        }
    }
}
