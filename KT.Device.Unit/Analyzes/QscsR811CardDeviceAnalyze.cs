using KT.Common.Core.Utils;
using KT.Device.Unit.CardReaders.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace KT.Device.Unit.CardReaders.Datas
{
    public class QscsR811CardDeviceAnalyze : IQscsR811CardDeviceAnalyze
    {
        private string _data = string.Empty;

        private readonly ILogger _logger;

        public QscsR811CardDeviceAnalyze(ILogger logger)
        {
            _logger = logger;
        }

        public CardReceiveModel Analyze(string protName, byte[] datas)
        {
            _logger.LogInformation("R811串口设备接收数据1：protName:{0} datas:{1} ", protName, datas.ToCommaPrintString());

            // ASCII:us-ascii   
            var asciiRresult = Encoding.ASCII.GetString(datas);
            var string16 = StringUtil.Get16String(datas);
            _logger.LogInformation($"R811串口设备接收数据2-0：protName:{protName} ascii:{asciiRresult} 16code:{string16} ");

            _logger.LogInformation("R811串口设备接收数据2：protName:{0} data:{1} value:{2} ", protName, _data, asciiRresult);
            _data = _data + asciiRresult;
            _logger.LogInformation("R811串口设备接收数据3：protName:{0} data:{1} value:{2} ", protName, _data, asciiRresult);
            while (true)
            {
                _logger.LogInformation("R811串口设备接收数据4：protName:{0} data:{1} value:{2} ", protName, _data, asciiRresult);
                if (_data == null || _data.Length == 0)
                {
                    return null;
                }
                _logger.LogInformation("R811串口设备接收数据5：protName:{0} data:{1} value:{2} ", protName, _data, asciiRresult);
                if (_data.StartsWith(""))
                {
                    _logger.LogInformation("R811串口设备接收数据6：protName:{0} data:{1} value:{2} ", protName, _data, asciiRresult);
                    if (_data.Length > 11)
                    {
                        _logger.LogInformation("R811串口设备接收数据7：protName:{0} data:{1} value:{2} ", protName, _data, asciiRresult);
                        break;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    _logger.LogInformation("R811串口设备接收数据8：protName:{0} data:{1} value:{2} ", protName, _data, asciiRresult);
                    _data = _data[1..];
                }
            }

            // 获取有效位 
            asciiRresult = _data.Substring(1, 10);
            if (_data.Length > 11)
            {
                _data = _data[11..];
            }
            else
            {
                _data = string.Empty;
            }

            _logger.LogInformation("R811串口设备接收数据：protName:{0} data:{1} ", protName, asciiRresult);
            // 进制转换
            asciiRresult = Convert.ToInt64(asciiRresult, 16).ToString();
            _logger.LogInformation("R811串口设备接收数据：protName:{0} data:{1} ", protName, asciiRresult);

            var cardData = new CardReceiveModel();
            cardData.AccessType = "IC_CARD";
            cardData.CardNumber = asciiRresult;
            cardData.IsCheckDate = false;

            return cardData;
        }
    }
}
