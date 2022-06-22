using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Quanta.Service.Devices.Hikvision.Models;
using KT.Quanta.Service.Devices.Hikvision.Sdks;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hikvision
{
    /// <summary>
    /// 海康SDK
    /// </summary>
    public class HikvisionSdkService : IHikvisionSdkService
    {
        private CommunicateDeviceInfoModel _communicateDeviceInfo;
        private int userId = -1;
        private HikvisionTypeParameterModel _typeParameter;
        private HikvisionSdkExecuteQueue _hikvisionSdkExecuteQueue;
        private CHCNetSDKService _cHCNetSDKService;

        private ILogger<HikvisionSdkService> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        private AppSettings _appSettings;
        private bool _isDeployEvent = false;
        private int _charEncodeType;
        private readonly OpenApi _openApi;
        private readonly PushUrlHelper _pushUrlHelper;

        public HikvisionSdkService(ILogger<HikvisionSdkService> logger,
            IServiceScopeFactory serviceScopeFactory,
            IOptions<AppSettings> appSettings,
            OpenApi openApi,
            PushUrlHelper pushUrlHelper,
            HikvisionSdkExecuteQueue hikvisionSdkExecuteQueue,
            CHCNetSDKService cHCNetSDKService)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _appSettings = appSettings.Value;
            _openApi = openApi;
            _pushUrlHelper = pushUrlHelper;
            _hikvisionSdkExecuteQueue = hikvisionSdkExecuteQueue;
            _cHCNetSDKService = cHCNetSDKService;

            _hikvisionSdkExecuteQueue.ExecuteActionAsync = ExecuteQueueAsync;
            _cHCNetSDKService.SetLogger(_logger);
        }

        /// <summary>
        /// 0- 无字符编码信息(老设备)，
        /// 1- GB2312(简体中文)，
        /// 2- GBK，
        /// 3- BIG5(繁体中文)，
        /// 4- Shift_JIS(日文)，
        /// 5- EUC-KR(韩文)，
        /// 6- UTF-8，
        /// 7- ISO8859-1，
        /// 8- ISO8859-2，
        /// 9- ISO8859-3，
        /// … 依次类推，
        /// 21- ISO8859-15(西欧)
        /// </summary>
        /// <returns></returns>
        public Encoding GetEncoding()
        {
            Encoding encoding;
            if (_charEncodeType == 1)
            {
                encoding = CodePagesEncodingProvider.Instance.GetEncoding("GB2312");
                return encoding;
            }
            else if (_charEncodeType == 2)
            {
                encoding = CodePagesEncodingProvider.Instance.GetEncoding("GBK");
                return encoding;
            }
            else if (_charEncodeType == 3)
            {
                encoding = CodePagesEncodingProvider.Instance.GetEncoding("BIG5");
                return encoding;
            }
            else if (_charEncodeType == 4)
            {
                encoding = CodePagesEncodingProvider.Instance.GetEncoding("Shift-JIS");
                return encoding;
            }
            else if (_charEncodeType == 5)
            {
                encoding = CodePagesEncodingProvider.Instance.GetEncoding("EUC-KR");
                return encoding;
            }
            else if (_charEncodeType == 6)
            {
                encoding = Encoding.UTF8;
                return encoding;
            }
            //else if (_charEncodeType == 7)
            //{
            //    Encoding encoding = CodePagesEncodingProvider.Instance.GetEncoding("ISO-8859-1");
            //    return encoding;
            //}
            else
            {
                for (int i = 7; i <= 15; i++)
                {
                    if (_charEncodeType == i)
                    {
                        encoding = Encoding.GetEncoding($"ISO-8859-{i - 6}");
                        return encoding;
                    }
                }
            }
            //有海康设备HIKVISION-DS-5607Z-ZZH编码类型为0使用GB2312，修改默认方请按类型来修改
            _logger.LogWarning($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision使用默认GB2312编码方式：charEncodeType:{_charEncodeType} ");
            encoding = CodePagesEncodingProvider.Instance.GetEncoding("GB2312");
            return encoding;
        }

        private async Task ExecuteQueueAsync(HikvisionSdkExecuteQueueModel model)
        {
            if (model.DistributeType == HikvisionSdkExecuteDistributeTypeEnum.SetCard.Value)
            {
                await ExecuteSetCardAsync(model.PersonId, (HikvisionPersonCardQuery)model.Data);

                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} 海康结束下发设置卡信息：{JsonConvert.SerializeObject(model.Data, JsonUtil.JsonPrintSettings)} ");
            }
            else if (model.DistributeType == HikvisionSdkExecuteDistributeTypeEnum.DeleteCard.Value)
            {
                await ExecuteDeleteCardAsync(model.PersonId, (string)model.Data);

                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} 海康结束下发删除卡信息：{JsonConvert.SerializeObject(model.Data, JsonUtil.JsonPrintSettings)} ");
            }
            else if (model.DistributeType == HikvisionSdkExecuteDistributeTypeEnum.SetFace.Value)
            {
                await ExecuteSetFaceAsync(model.PersonId, (HikvisionSetFaceQuery)model.Data);

                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} 海康结束下发设置人脸信息：{JsonConvert.SerializeObject(model.Data, JsonUtil.JsonPrintSettings)} ");
            }
            else if (model.DistributeType == HikvisionSdkExecuteDistributeTypeEnum.DeleteFace.Value)
            {
                await ExecuteDeleteFaceAsync(model.PersonId, (HikvisionDeleteFaceQuery)model.Data);

                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} 海康结束下发删除人脸信息：{JsonConvert.SerializeObject(model.Data, JsonUtil.JsonPrintSettings)} ");
            }
            else
            {
                _logger.LogError($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} 海康找不到下发权限类型：type:{model.DistributeType}");
            }
        }

        public Task DeleteCardAsync(string personId, string cardNo)
        {
            var model = new HikvisionSdkExecuteQueueModel();
            model.DistributeType = HikvisionSdkExecuteDistributeTypeEnum.DeleteCard.Value;
            model.PersonId = personId;
            model.Data = cardNo;
            _hikvisionSdkExecuteQueue.Add(model);

            return Task.CompletedTask;
        }

        public Task DeleteFaceAsync(string personId, HikvisionDeleteFaceQuery query)
        {
            var model = new HikvisionSdkExecuteQueueModel();
            model.DistributeType = HikvisionSdkExecuteDistributeTypeEnum.DeleteFace.Value;
            model.PersonId = personId;
            model.Data = query;
            _hikvisionSdkExecuteQueue.Add(model);

            return Task.CompletedTask;
        }

        public Task SetCardAsync(string personId, HikvisionPersonCardQuery personCard)
        {
            var model = new HikvisionSdkExecuteQueueModel();
            model.DistributeType = HikvisionSdkExecuteDistributeTypeEnum.SetCard.Value;
            model.PersonId = personId;
            model.Data = personCard;
            _hikvisionSdkExecuteQueue.Add(model);

            return Task.CompletedTask;
        }

        public Task SetFaceAsync(string personId, HikvisionSetFaceQuery query)
        {
            var model = new HikvisionSdkExecuteQueueModel();
            model.DistributeType = HikvisionSdkExecuteDistributeTypeEnum.SetFace.Value;
            model.PersonId = personId;
            model.Data = query;
            _hikvisionSdkExecuteQueue.Add(model);

            return Task.CompletedTask;
        }

        private async Task ExecuteDeleteCardAsync(string personId, string cardNo)
        {
            _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision开始推送删除卡：cardNo:{cardNo} charEncodeType:{_charEncodeType} userId:{userId} ");
            var logTip = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision：cardNo:{cardNo} charEncodeType:{_charEncodeType} userId:{userId}";
            var result = await _cHCNetSDKService.ExecuteDeleteCardAsync(userId, cardNo, logTip);
            if (!result.IsSuccess)
            {
                var fail = new FailModel();
                fail.PersonId = personId;
                fail.Reason = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送删除卡失败：{result.Message} ";
                await _openApi.PushFailAsync(_pushUrlHelper.PushUrl, fail);

                throw CustomException.Run($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送删除卡失败：{logTip} {result.Message} ");
            }
            else
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送删除卡成功：cardNo:{cardNo} charEncodeType:{_charEncodeType} userId:{userId} ");
            }
        }

        private async Task ExecuteDeleteFaceAsync(string personId, HikvisionDeleteFaceQuery query)
        {
            _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision开始推送删除人脸：cardNo:{query.CardNo} cardReaderNo:{query.CardReaderNo} charEncodeType:{_charEncodeType} userId:{userId} ");
            var logTip = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision：cardNo:{query.CardNo} cardReaderNo:{query.CardReaderNo} charEncodeType:{_charEncodeType} userId:{userId}";
            var result = await _cHCNetSDKService.ExecuteDeleteFaceAsync(userId, query.CardNo, query.CardReaderNo, logTip);
            if (!result.IsSuccess)
            {
                var fail = new FailModel();
                fail.PersonId = personId;
                fail.Reason = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送删除人脸失败：{result.Message} ";
                await _openApi.PushFailAsync(_pushUrlHelper.PushUrl, fail);

                throw CustomException.Run($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送删除人脸失败：{logTip} {result.Message} ");
            }
            else
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送删除人脸成功：cardNo:{query.CardNo} charEncodeType:{_charEncodeType} userId:{userId} ");
            }
        }

        private async Task ExecuteSetCardAsync(string personId, HikvisionPersonCardQuery personCard)
        {
            _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision开始推送下载卡： cardNo:{personCard.CardNo} charEncodeType:{_charEncodeType} userId:{userId} card:{JsonConvert.SerializeObject(personCard, JsonUtil.JsonPrintSettings)} ");
            var logTip = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision：charEncodeType:{_charEncodeType} userId:{userId}";
            var result = await _cHCNetSDKService.ExecuteSetCardAsync(GetEncoding(), userId, personCard, logTip);
            if (!result.IsSuccess)
            {
                var fail = new FailModel();
                fail.PersonId = personId;
                fail.Reason = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送下载卡失败：{result.Message} ";
                await _openApi.PushFailAsync(_pushUrlHelper.PushUrl, fail);

                throw CustomException.Run($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送下载卡失败：{logTip} {result.Message} ");
            }
            else
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送下载卡成功：cardNo:{personCard.CardNo} charEncodeType:{_charEncodeType} userId:{userId} ");
            }
        }

        private async Task ExecuteSetFaceAsync(string personId, HikvisionSetFaceQuery model)
        {
            _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision开始推送下载人脸：cardNo:{model.CardNo} " +
                $"charEncodeType:{_charEncodeType} " +
                $"userId:{userId} " +
                $"model:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

            var logTip = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision：cardNo:{model.CardNo} cardReaderNo:{model.CardReaderNo} charEncodeType:{_charEncodeType} userId:{userId}";
            var result = await _cHCNetSDKService.ExecuteSetFaceAsync(userId, model.CardNo, model.FacePath, logTip, model.CardReaderNo);
            if (!result.IsSuccess)
            {
                var fail = new FailModel();
                fail.PersonId = personId;
                fail.Reason = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送下载人脸失败：{result.Message} ";
                await _openApi.PushFailAsync(_pushUrlHelper.PushUrl, fail);

                throw CustomException.Run($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送下载人脸失败：{logTip} {result.Message} ");
            }
            else
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送下载人脸成功：cardNo:{model.CardNo} charEncodeType:{_charEncodeType} userId:{userId} ");
            }
        }

        public async Task<bool> DeployEventAsync()
        {
            _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} " +
                $"Hikvision开始推送开启事件：" +
                $"charEncodeType:{_charEncodeType} " +
                $"userId:{userId} " +
                $"typeParameter:{JsonConvert.SerializeObject(_typeParameter, JsonUtil.JsonPrintSettings)} ");

            var logTip = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision： charEncodeType:{_charEncodeType} userId:{userId}";
            _isDeployEvent = await _cHCNetSDKService.ExecuteDeployEventAsync(userId, _typeParameter, logTip);
            if (!_isDeployEvent)
            {
                _logger.LogError($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送开启事件失败：charEncodeType:{_charEncodeType} userId:{userId} ");
                return _isDeployEvent;
            }
            else
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送开启事件成功：charEncodeType:{_charEncodeType} userId:{userId} ");

                return _isDeployEvent;
            }
        }

        public async Task<HikvisionPersonCardQuery> GetCardAsync(string personId, string cardNo)
        {
            _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision开始推送获取卡：cardNo:{cardNo} charEncodeType:{_charEncodeType} userId:{userId} ");
            var logTip = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision：cardNo:{cardNo} charEncodeType:{_charEncodeType} userId:{userId}";
            var result = await _cHCNetSDKService.ExecuteGetCardAsync(GetEncoding(), userId, cardNo, logTip);
            if (!result.IsSuccess)
            {
                var fail = new FailModel();
                fail.PersonId = personId;
                fail.Reason = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送获取卡失败：{result.Message} ";
                await _openApi.PushFailAsync(_pushUrlHelper.PushUrl, fail);

                _logger.LogError($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送获取卡失败：{logTip} {result.Message} ");
                return null;
            }
            else
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送获取卡成功：cardNo:{cardNo} charEncodeType:{_charEncodeType} userId:{userId} " +
                    $"data:{JsonConvert.SerializeObject(result.Data, JsonUtil.JsonPrintSettings)} ");

                var personCard = new HikvisionPersonCardQuery();
                personCard.CardNo = Encoding.Default.GetString(result.Data.byCardNo);
                personCard.CardRightPlan = result.Data.wCardRightPlan[0];
                personCard.EmployeeNo = result.Data.dwEmployeeNo;
                personCard.DoorRights = result.Data.byDoorRight;

                return personCard;
            }
        }

        public async Task<int> GetCurACSDeviceDoorNumAsync()
        {
            _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision开始推送获取门禁输出数量：charEncodeType:{_charEncodeType} userId:{userId} ");
            var logTip = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision：charEncodeType:{_charEncodeType} userId:{userId}";
            var curACSDeviceDoorNum = await _cHCNetSDKService.ExecuteGetCurACSDeviceDoorNumAsync(userId, logTip);
            if (curACSDeviceDoorNum <= 0)
            {
                throw CustomException.Run($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送获取门禁输出数量失败：charEncodeType:{_charEncodeType} userId:{userId} ");
            }
            else
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送获取门禁输出数量成功：charEncodeType:{_charEncodeType} userId:{userId} ");
            }
            return curACSDeviceDoorNum;
        }

        public async Task<string> GetFaceAsync(string personId, string cardNo, int cardReaderNo = 1)
        {
            _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision开始推送获取人脸：cardNo:{cardNo} cardReaderNo:{cardReaderNo} charEncodeType:{_charEncodeType} userId:{userId} ");
            var logTip = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision：cardNo:{cardNo} cardReaderNo:{cardReaderNo} charEncodeType:{_charEncodeType} userId:{userId}";
            var result = await _cHCNetSDKService.ExecuteGetFaceAsync(userId, cardNo, logTip, cardReaderNo);
            if (!result.IsSuccess)
            {
                var fail = new FailModel();
                fail.PersonId = personId;
                fail.Reason = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送获取人脸失败：{result.Message} ";
                await _openApi.PushFailAsync(_pushUrlHelper.PushUrl, fail);

                _logger.LogError($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送获取人脸失败：{logTip} {result.Message} ");
            }
            else
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送获取人脸成功：cardNo:{cardNo} charEncodeType:{_charEncodeType} userId:{userId} " +
                    $"data:{result.Data} ");
            }
            return result.Data;
        }

        public async Task<int> LoginAsync(string account, string password)
        {
            _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} " +
                $"Hikvision开始推送登录：account:{account} " +
                $"password:{password} " +
                $"charEncodeType:{_charEncodeType} " +
                $"userId:{userId} ");

            var logTip = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision：charEncodeType:{_charEncodeType} userId:{userId}";
            var loginResult = await _cHCNetSDKService.ExecuteLoginAsync(userId, account, password, _communicateDeviceInfo.IpAddress, _communicateDeviceInfo.Port, logTip);
            userId = loginResult.UserId;

            if (userId < 0)
            {
                _charEncodeType = -1;

                throw CustomException.Run($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} " +
                    $"Hikvision推送登录失败：charEncodeType:{_charEncodeType} " +
                    $"userId:{userId} ");
            }
            else
            {
                _charEncodeType = loginResult.DeviceInfo.byCharEncodeType;

                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} " +
                    $"Hikvision推送登录成功：charEncodeType:{_charEncodeType} " +
                    $"userId:{userId} ");
            }
            return userId;
        }

        public async Task LogoutAsync()
        {
            _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision开始推送退出登录：charEncodeType:{_charEncodeType} userId:{userId} ");

            var logTip = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision：charEncodeType:{_charEncodeType} userId:{userId}";
            var isCloseAlarm = await _cHCNetSDKService.CloseAlarmAsync(_isDeployEvent, userId, logTip);
            if (!isCloseAlarm)
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送退出布防失败：charEncodeType:{_charEncodeType} userId:{userId} ");
            }
            else
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送退出布防成功：charEncodeType:{_charEncodeType} userId:{userId} ");
            }

            var isLogout = await _cHCNetSDKService.ExecuteLogoutAsync(userId, logTip);
            if (!isLogout)
            {
                throw CustomException.Run($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送退出登录失败：charEncodeType:{_charEncodeType} userId:{userId} ");
            }
            else
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送退出登录成功：charEncodeType:{_charEncodeType} userId:{userId} ");
            }
        }

        public async Task InitAsync(CommunicateDeviceInfoModel communicateDevice, HikvisionTypeParameterModel typeParameter)
        {
            _typeParameter = typeParameter;

            _communicateDeviceInfo = communicateDevice;
            _cHCNetSDKService.ExecuteUploadPassRecordAsync = ExecuteUploadPassRecordAsync;
            _hikvisionSdkExecuteQueue.DeviceKey = $"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port}";

            var isInit = await _cHCNetSDKService.ExecuteInitAsync();
            if (!isInit)
            {
                throw CustomException.Run($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送初始化失败：charEncodeType:{_charEncodeType} userId:{userId} ");
            }
            else
            {
                _logger.LogInformation($"{_communicateDeviceInfo.IpAddress}:{_communicateDeviceInfo.Port} Hikvision推送初始化成功：charEncodeType:{_charEncodeType} userId:{userId} ");
            }
        }

        public async Task ExecuteUploadPassRecordAsync(PassRecordModel passRecord)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serivce = scope.ServiceProvider.GetRequiredService<IHikvisionResponseHandler>();
                await serivce.UploadPassRecordAsync(passRecord);
            };
        }
    }
}
