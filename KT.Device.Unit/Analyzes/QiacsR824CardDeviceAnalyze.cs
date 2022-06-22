using KT.Common.Core.Utils;
using KT.Device.Unit.Analyzes;
using KT.Device.Unit.CardReaders.Models;
using KT.Quanta.Common.Enums;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Device.Unit.CardReaders.Datas
{
    public class QiacsR824CardDeviceAnalyze : IQiacsR824CardDeviceAnalyze
    {
        private readonly ILogger _logger;
        private readonly QrCodeAnalyzeProvider _qrCodeAnalyzeProvider;
        private readonly CardDeviceSettings _cardDeviceSettings;

        public QiacsR824CardDeviceAnalyze(ILogger logger,
            QrCodeAnalyzeProvider qrCodeAnalyzeProvider,
            CardDeviceSettings cardDeviceSettings)
        {
            _logger = logger;
            _qrCodeAnalyzeProvider = qrCodeAnalyzeProvider;
            _cardDeviceSettings = cardDeviceSettings;
        }

        private List<byte> _bufferBytes = new List<byte>();

        public CardReceiveModel Analyze(string protName, byte[] datas)
        {
            _logger.LogInformation($"串口接收数据1：portName:{protName} datas:{datas.ToCommaPrintString()} ");
            //队列的形式读取
            if (datas != null && datas.Count() > 0)
            {
                datas.ToList().ForEach(x => _bufferBytes.Add(x));
            }
            _logger.LogInformation($"串口接收总数据：portName:{protName} bytes:{_bufferBytes.ToCommaPrintString()} ");

            if (_bufferBytes.Count() == 0)
            {
                _logger.LogInformation($"串口接收总数据为空：portName:{protName} ");
                return null;
            }

            if (_bufferBytes[0] == 2)
            {
                if (_bufferBytes.Count() < 5)
                {
                    _logger.LogInformation($"串口接收总数据：长度不足1 length:{_bufferBytes.Count()} neet:{5} ");
                    return null;
                }

                var dataLength = BitConverter.ToUInt16(new byte[2] { _bufferBytes[3], _bufferBytes[4] });
                var totalDataLength = 7 + dataLength;
                if (_bufferBytes.Count() < totalDataLength)
                {
                    _logger.LogInformation($"串口接收总数据：长度异常 length:{_bufferBytes.Count()} neet:{dataLength} ");
                    // 长度最大值为128,超出长度重新识别 
                    if (_bufferBytes.Count() > 128)
                    {
                        _bufferBytes.Clear();
                        return null;
                    }
                    else if (dataLength > 128)
                    {
                        _bufferBytes.RemoveAt(0);
                        return Analyze(protName, null);
                    }
                }

                byte[] resultDataBytes = GetDataBytes(totalDataLength);

                var result = new CardReceiveModel();
                //数据高低位设置
                if (_cardDeviceSettings.R824IcCardBigEndian)
                {
                    var cardNumberBytes = new byte[4];
                    cardNumberBytes[3] = resultDataBytes[5];
                    cardNumberBytes[2] = resultDataBytes[6];
                    cardNumberBytes[1] = resultDataBytes[7];
                    cardNumberBytes[0] = resultDataBytes[8];
                    result.CardNumber = BitConverter.ToUInt32(cardNumberBytes).ToString();
                }
                else
                {
                    result.CardNumber = BitConverter.ToUInt32(resultDataBytes, 5).ToString();
                }
                result.IsCheckDate = false;
                result.AccessType = "IC_CARD";

                _logger.LogInformation($"串口接收数据3：data:{JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
                return result;
            }
            else if (_bufferBytes[0] == 42 && _bufferBytes[1] == 42)
            {
                if (_cardDeviceSettings.QrCodeType == QrCodeTypeEnum.Xita.Value)
                {
                    if (_bufferBytes.Count() < 46)
                    {
                        _logger.LogInformation($"串口接收总数据：长度不足3 length:{_bufferBytes.Count()} neet:{46}");
                        return null;
                    }

                    byte[] resultDataBytes = GetDataBytes(46);

                    var value = Encoding.UTF8.GetString(resultDataBytes);
                    _logger.LogInformation($"串口接收数据2：data:{value} ");

                    var result = _qrCodeAnalyzeProvider.DecryptQrCode(value);
                    result.AccessType = "QR_CODE";

                    _logger.LogInformation($"串口接收数据3：data:{JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
                    return result;
                }
                else if (_cardDeviceSettings.QrCodeType == QrCodeTypeEnum.Quanta.Value)
                {
                    int qrCodeByteLength = -1;
                    for (int i = 2; i < (_bufferBytes.Count() - 1); i++)
                    {
                        if (_bufferBytes[i] == 35 && _bufferBytes[i + 1] == 35)
                        {
                            //i+后1字符 + 1为长度
                            qrCodeByteLength = i + 1 + 1;
                            break;
                        }
                    }

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
                    throw new ArgumentException($"找不到二维码解析规则：{_cardDeviceSettings.QrCodeType} ");
                }
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
