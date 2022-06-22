using KT.Common.Core.Utils;
using KT.Device.Unit.CardReaders.Models;
using KT.Quanta.Common.Enums;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace KT.Device.Unit.Analyzes
{
    public class QrCodeAnalyzeProvider
    {
        private readonly ILogger _logger;
        private readonly CardDeviceSettings _cardDeviceSettings;

        public QrCodeAnalyzeProvider(ILogger logger,
            CardDeviceSettings cardDeviceSettings)
        {
            _logger = logger;
            _cardDeviceSettings = cardDeviceSettings;
        }

        public CardReceiveModel DecryptQrCode(string value)
        {
            if (_cardDeviceSettings.QrCodeType == QrCodeTypeEnum.Xita.Value)
            {
                return DecryptXitaQrCode(value);
            }
            else if (_cardDeviceSettings.QrCodeType == QrCodeTypeEnum.Quanta.Value)
            {
                return DecryptQuantaQrCode(value);
            }
            else
            {
                throw new ArgumentException($"找不到二维码解析规则：{_cardDeviceSettings.QrCodeType} ");
            }
        }

        /// <summary>
        /// 西域二维码解析规则
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private CardReceiveModel DecryptXitaQrCode(string value)
        {
            var cardReceive = new CardReceiveModel();

            //String qr = "**" + encode;
            var base64 = value.TrimStart('*');
            //var encode = Convert.ToBase64String(bytes);
            var bytes = Convert.FromBase64String(base64);
            //var bytes = DesUtil.Encrypt3Des(concat, "Radio888ATTeBDsPKmGYM4Ud"); 
            bytes = DesUtil.Decrypt3Des(bytes, _cardDeviceSettings.QrCodeKey);

            //int expires =28800 + 3600 ;//3600*24*7;
            //byte[] bs4 = BitConverter.GetBytes(expires); 
            uint expireSpan = BitConverter.ToUInt32(bytes, 13) - 28800;

            //int seconds = (int)(DateTimeUtil.UtcNowSeconds() );
            //byte[] bs3 = BitConverter.GetBytes(seconds); 
            var startTime = BitConverter.ToUInt32(bytes, 9);

            //过期时间
            var endTime = startTime + expireSpan;

            //byte[] bs2 = BitConverter.GetBytes(cardNo); 
            //bs2 = ArrayByteUtil.MergeArray(new byte[] { 0x00, 0x00, 0x00, 0x00 }, bs2); 
            var cardNo = BitConverter.ToUInt32(bytes, 5);

            cardReceive.CardNumber = cardNo.ToString();
            cardReceive.IsCheckDate = true;
            cardReceive.StartTime = startTime;
            cardReceive.EndTime = endTime;

            return cardReceive;
        }

        /// <summary>
        /// 康塔二维码解析规则
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private CardReceiveModel DecryptQuantaQrCode(string value)
        {
            _logger.LogInformation($"Quanta二维码解析数据1：{value} ");

            //去掉头尾帧
            value = value.TrimStartString("**").TrimEndString("##");

            _logger.LogInformation($"Quanta二维码解析数据2：{value} ");

            //解密
            var data = value.AesDecryptHex(_cardDeviceSettings.QrCodeKey);

            _logger.LogInformation($"Quanta二维码解析数据3：{data} ");

            var values = data.Split("#");
            if (values == null || values.Length == 0)
            {
                _logger.LogError($"Quanta二维码解析数据4：二维码数据分片为空！ ");
                return null;
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

            _logger.LogError($"Quanta二维码解析数据5：{JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");

            return result;
        }
    }
}
