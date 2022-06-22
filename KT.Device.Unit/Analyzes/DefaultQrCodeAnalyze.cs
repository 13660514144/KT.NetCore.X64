using KT.Common.Core.Utils;
using KT.Device.Unit.Analyzes;
using KT.Device.Unit.CardReaders.Models;
using Microsoft.Extensions.Logging;
using System.Text;

namespace KT.Device.Unit.CardReaders.Datas
{

    public class DefaultQrCodeAnalyze : ICardDeviceAnalyze
    {
        private string _data = string.Empty;

        private readonly ILogger _logger;
        private readonly QrCodeAnalyzeProvider _qrCodeAnalyzeProvider;
        private string key = "%QUANTA@DATA$COM";

        
        public DefaultQrCodeAnalyze(ILogger logger,
            QrCodeAnalyzeProvider qrCodeAnalyzeProvider)
        {
            _logger = logger;
            _qrCodeAnalyzeProvider = qrCodeAnalyzeProvider;
        }

        public CardReceiveModel Analyze(string protName, byte[] datas)
        {
            //_logger.LogInformation("二维码串口设备接收数据1：protName:{0} datas:{1} ", protName, datas.ToCommaPrintString());
            // ASCII:us-ascii   
            string result = Encoding.ASCII.GetString(datas);
           // _logger.LogInformation("二维码串口设备接收数据2：protName:{0} data:{1} value:{2} ", protName, _data, result);
            _data = _data + result;
            //_logger.LogInformation("二维码串口设备接收数据3：protName:{0} data:{1} value:{2} ", protName, _data, result);
            while (true)
            {
                //_logger.LogInformation("二维码串口设备接收数据4：protName:{0} data:{1} value:{2} ", protName, _data, result);
                if (_data == null || _data.Length == 0)
                {
                    return null;
                }
                //_logger.LogInformation("二维码串口设备接收数据5：protName:{0} data:{1} value:{2} ", protName, _data, result);
                if (_data.StartsWith("*"))
                {
                    if (_data.StartsWith("**"))
                    {
                        //_logger.LogInformation("二维码串口设备接收数据6：protName:{0} data:{1} value:{2} ", protName, _data, result);
                        if (_data.IndexOf("##") > 0)
                        {
                            //_logger.LogInformation("二维码串口设备接收数据7：protName:{0} data:{1} value:{2} ", protName, _data, result);
                            break;
                        }
                        else
                        {
                            // 长度最大值为128,超出长度重新识别
                            if (_data.Length > 128)
                            {
                                //_logger.LogInformation($"串口接收总数据：长度过长2 length:{_data.Length} neet:{128} ");
                                _data = string.Empty;
                            }
                            return null;
                        }
                    }
                    else if (_data.Length == 1)
                    {
                        return null;
                    }
                    else
                    {
                        //去掉前两个
                        //_logger.LogInformation("二维码串口设备接收数据8：protName:{0} data:{1} value:{2} ", protName, _data, result);
                        _data = _data[2..];
                    }
                }
                else
                {
                    //去掉第一个
                   // _logger.LogInformation("二维码串口设备接收数据8：protName:{0} data:{1} value:{2} ", protName, _data, result);
                    _data = _data[1..];
                }
            }

            // 获取有效位 
            _data = _data.TrimStart('*');
            var endIndex = _data.IndexOf("##");

            result = _data.Substring(0, endIndex);
            if (_data.Length > endIndex)
            {
                _data = _data.Substring(endIndex);
            }
            else
            {
                _data = string.Empty;
            }
            _logger.LogInformation("二维码串口设备接收数据：protName:{0} data:{1} value:{2} ", protName, _data, result);
            //return _qrCodeAnalyzeProvider.DecryptQrCode(result);
            return SplitData(result);

        }
        private CardReceiveModel SplitData(string hexData)
        {
            // 进制转换
            //_logger.LogInformation("二维码串口设备接收数据11：hexData:{0} ", hexData);
            //解密
            var data = hexData.AesDecryptHex(key);
            // 解密
            //_logger.LogInformation("二维码串口设备接收数据12：data:{0} ", data);
            var values = data.Split("#");
            if (values == null || values.Length == 0)
            {
                _logger.LogError("二维码串口设备接收数据为空13!!!! ");
            }
            var result = new CardReceiveModel();
            result.AccessType = "QR_CODE";
            if (values.Length >= 4)
            {
                result.CardNumber = values[0];
                result.IsCheckDate = values[1] == "1";
                result.StartTime = ConvertUtil.ToLong(values[2]);
                result.EndTime = ConvertUtil.ToLong(values[3]);
            }
            else
            {
                result.CardNumber = values[0];
                result.IsCheckDate = false;
            }
            return result;
        }
    }

}
