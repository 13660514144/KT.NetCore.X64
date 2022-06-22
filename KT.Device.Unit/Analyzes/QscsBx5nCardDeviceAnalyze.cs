using KT.Common.Core.Utils;
using KT.Device.Unit.Analyzes;
using KT.Device.Unit.CardReaders.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Device.Unit.CardReaders.Datas
{
    public class QscsBx5nCardDeviceAnalyze : IQscsBx5nCardDeviceAnalyze
    {
        private readonly ILogger _logger;
        private readonly QrCodeAnalyzeProvider _qrCodeAnalyzeProvider;

        public QscsBx5nCardDeviceAnalyze(ILogger logger,
            QrCodeAnalyzeProvider qrCodeAnalyzeProvider)
        {
            _logger = logger;
            _qrCodeAnalyzeProvider = qrCodeAnalyzeProvider;
        }

        private List<byte> _bufferBytes = new List<byte>();

        public CardReceiveModel Analyze(string protName, byte[] datas)
        {
            _logger.LogInformation($"bx5n--串口接收数据1：portName:{protName} datas:{datas.ToCommaPrintString()} ");
            //队列的形式读取
            if (datas != null && datas.Count() > 0)
            {
                datas.ToList().ForEach(x => _bufferBytes.Add(x));
            }
            //_logger.LogInformation($"串口接收总数据：portName:{protName} bytes:{_bufferBytes.ToCommaPrintString()} ");

            if (_bufferBytes.Count() <= 0)
            {
                _logger.LogInformation($"bx5n--串口接收总数据为空：portName:{protName} ");
                return null;
            }

            //起始符号为$$,结束符##
           // else if (_bufferBytes[0] == 36 && _bufferBytes[1] == 36)
              else if (_bufferBytes[0] == 36)
                    {
                var icCodeByteLength = GetQrCodeByteLength();
                if (icCodeByteLength <= 0)
                {
                    _logger.LogInformation($"串口接收总数据：不存在结束标识！ length:{_bufferBytes.Count()} ");

                    // 长度最大值为128,超出长度重新识别
                    if (_bufferBytes.Count() > 20)
                    {
                        _logger.LogInformation($"串口接收总数据：长度过长2 length:{_bufferBytes.Count()} neet:{20} ");
                        _bufferBytes.Clear();
                    }

                    return null;
                }

                //获取
                byte[] resultDataBytes = GetDataBytes(icCodeByteLength);

                //去掉起始结束标识
                var icCardBytes = new byte[icCodeByteLength - 4];
                for (int i = 0; i < icCardBytes.Length; i++)
                {
                    icCardBytes[i] = resultDataBytes[i + 2];
                }

                var value = Encoding.ASCII.GetString(icCardBytes);
                _logger.LogInformation($"串口接收IC卡数据：data:{value} ");

                //卡号去掉左边0
                var result = new CardReceiveModel();
                result.CardNumber = value.TrimStart('0');
                result.IsCheckDate = false;
                result.AccessType = "IC_CARD";

                _logger.LogInformation($"串口接收IC卡数据3：data:{JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
                return result;
            }
            //起始符号为**,结束符##
            else if (_bufferBytes[0] == 42 && _bufferBytes[1] == 42)
            {
                var qrCodeByteLength = GetQrCodeByteLength();

                if (qrCodeByteLength <= 0)
                {
                    _logger.LogInformation($"串口接收总数据：二维码不存在结束标识！ length:{_bufferBytes.Count()} ");

                    // 长度最大值为128,超出长度重新识别
                    if (_bufferBytes.Count() > 128)
                    {
                        _logger.LogInformation($"串口接收总数据：长度过长2 length:{_bufferBytes.Count()} neet:{128} ");
                        _bufferBytes.Clear();
                    }

                    return null;
                }

                //获取二维码有所数据
                byte[] resultDataBytes = GetDataBytes(qrCodeByteLength);
                //去掉起始结束标识
                var qrCodeBytes = new byte[qrCodeByteLength - 4];
                for (int i = 0; i < qrCodeBytes.Length; i++)
                {
                    qrCodeBytes[i] = resultDataBytes[i + 2];
                }

                var value = Encoding.UTF8.GetString(qrCodeBytes);
                _logger.LogInformation($"串口接收数据二维码数据：data:{value} ");

                var result = _qrCodeAnalyzeProvider.DecryptQrCode(value);

                _logger.LogInformation($"串口接收数据3：data:{JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
                return result;
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
        }

        private int GetQrCodeByteLength()
        {
            int qrCodeByteLength = -1;
            for (int i = 2; i < (_bufferBytes.Count() - 1); i++)
            {
                if (_bufferBytes[i] == 35 && _bufferBytes[i + 1] == 35)
                {
                    //i + 后1字符 + 1为长度
                    qrCodeByteLength = i + 1 + 1;
                    break;
                }
            }

            return qrCodeByteLength;
        }

        private byte[] GetDataBytes(int totalDataLength)
        {
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
