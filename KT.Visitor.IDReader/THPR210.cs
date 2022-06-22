using IDevices;
using KT.Common.Core.Utils;
using KT.Proxy.BackendApi.Enums;
using KT.Visitor.IdReader.Common;
using KT.Visitor.IdReader.SDK;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using static KT.Visitor.IdReader.SDK.THPR210SDK;

namespace KT.Visitor.IdReader
{
    /// <summary>
    /// 文通
    /// </summary>
    public class THPR210 : IDevice
    {
        private ILogger<THPR210> _logger;
        public THPR210(ILogger<THPR210> logger)
        {
            _logger = logger;
        }

        public ReaderTypeEnum ReaderType { get; } = ReaderTypeEnum.THPR210;
        public Action<object> ResultCallBack { get; set; }

        private THPR210SDK _idCard;

        public int Port { get; set; }
        public bool IsScanImage { get; set; }

        public bool Authenticate()
        {
            return true;
        }

        public bool InitComm()
        {
            if (_idCard == null)
            {
                string dllBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReferenceFiles\\IdReaderSdks\\THPR210x64");
                //_idCard = new THPR210SDK(_logger, "58795668242476138509", dllBasePath);
                _idCard = new THPR210SDK(_logger, "58795668242476138509", dllBasePath);
            }
            if (_idCard.Result == null)
            {
                _idCard.Result = new THPR210SDK.RecognResult(_logger);
            }
            StartSignalLamp();
            return true;
        }

        public void StartSignalLamp()
        {
            var closeSignalLamp = IDCardApi.SetIOStatus(5, true);
            if (closeSignalLamp != 0)
            {
                _logger.LogError($"开启信号灯失败：{closeSignalLamp} ");
            }
        }

        public Person ReadContent()
        {
            _logger.LogDebug($"ReaderType.Value={ReaderType.Value}");
            //阅读ID卡
            try
            {
                _idCard.ReadCard();
            }
            catch (IdReaderException ex)
            {
                _logger.LogDebug("THPR210读取证件失败：{0} ", ex.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("THPR210读取证件错误：{0} ", e);
            }

            //未读到证件，继续扫描
            if (string.IsNullOrEmpty(_idCard.Result.IDCode))
            {
                //扫描证件
                _idCard.AutoClassAndRecognize();
            }

            _logger.LogDebug($"{ReaderType.Value} - THPR210扫描原始数据：{JsonConvert.SerializeObject(_idCard.Result, JsonUtil.JsonPrintSettings)} ");

            if (string.IsNullOrEmpty(_idCard.Result.IDCode))
            {
                throw IdReaderException.Run($"{ReaderType.Value} - THPR210扫描失败：未获取到身份证件信息！");
            }

            Person person = new Person();
            person.Name = _idCard.Result.Name;
            person.CardType = CertificateTypeEnum.GetValueByCode(_idCard.Result.CardType);
            person.IdCode = _idCard.Result.IDCode;
            person.Gender = _idCard.Result.Gender;
            person.Nation = _idCard.Result.PeopleChineseName;
            person.Birthday = _idCard.Result.Birthday;
            person.Address = _idCard.Result.Address;
            person.Agency = _idCard.Result.Authority;
            person.ExpireStart = _idCard.Result.IssueDay;
            person.ExpireEnd = _idCard.Result.ExpityDay;
            person.Portrait = _idCard.Result.HeadImg(person.CardType);

            return person;
        }

        public Person ScanContent(string operateIdType)
        {
            return ReadContent();
        }

        public bool CloseComm()
        {
            var closeSignalLamp = IDCardApi.SetIOStatus(5, false);
            if (closeSignalLamp != 0)
            {
                _logger.LogError($"关闭信号灯失败：{closeSignalLamp} ");
            }
            if (_idCard != null)
            {
                _idCard.Result = new RecognResult(_logger);
            }
            //_idCard.FreeKenle();
            return true;
        }

    }
}
