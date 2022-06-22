using KT.Common.Core.Utils;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HelperTools;
using ContralServer.CfgFileRead;

namespace KT.Quanta.Service.Handlers
{
    public class PushRecordHandler
    {
        private IServiceProvider _serviceProvider;
        public OpenApi _openApi;
        public PushUrlHelper _pushUrlHelper;
        private ILogger<PushRecordHandler> _logger;
        private AppSettings _appSettings;

        public PushRecordHandler(IServiceProvider serviceProvider,
           OpenApi openApi,
           PushUrlHelper pushUrlHelper,
           ILogger<PushRecordHandler> logger,
           IOptions<AppSettings> appSettings)
        {
            _serviceProvider = serviceProvider;
            _openApi = openApi;
            _pushUrlHelper = pushUrlHelper;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<bool> StartPushAsync(PassRecordModel passRecord)
        {
            var pushResult = await PushAsync(passRecord);
            if (!pushResult)
            {
                //上传错误，存储本地
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IPassRecordService>();
                    await service.AddIfNoExistsAsync(passRecord);
                }
                //上传错误，更改当前Job记录，防止出错一条卡死所有数据
                JobSchedulerHelper.FirstRecord = passRecord;
                return false;
            }

            return true;
        }

        private async Task<bool> PushAsync(PassRecordModel passRecord)
        {
            _logger.LogError($" api 上传封装==>PushAsync{JsonConvert.SerializeObject(passRecord)}");
            var pushPassRecord = new PushPassRecordModel();
            //人脸通行时的抓拍图片
            pushPassRecord.File = passRecord.FaceImage;
            //设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
            pushPassRecord.EquipmentId = passRecord.DeviceId;
            //扩展字段，在人脸摄像头时，该字段是人脸服务器的token
            pushPassRecord.Extra = passRecord.Extra;
            //出入方向       
            pushPassRecord.WayType = passRecord.WayType;
            //备注，用于填写第三方特别信息
            pushPassRecord.Remark = passRecord.Remark;
            //设备类型
            pushPassRecord.EquipmentType = passRecord.DeviceType;
            //通行类型
            pushPassRecord.AccessType = passRecord.AccessType;
            //通行码，IC卡、二维码、人脸ID
            pushPassRecord.AccessToken = string.IsNullOrEmpty(passRecord.PassRightSign) ? 0.ToString() : passRecord.PassRightSign;
            //通行时间，2019-11-06 15:20:45 
            if (!string.IsNullOrEmpty(passRecord.PassLocalTime))
            {
                pushPassRecord.AccessDate = passRecord.PassLocalTime;
            }
            else
            {
                pushPassRecord.AccessDate = passRecord.PassTime.ToZoneDateTimeByMillis()?.ToSecondString();
            }
            pushPassRecord.Temperature = passRecord.Temperature;
            pushPassRecord.Mask = passRecord.IsMask;
            //_logger.LogError($"最后封装派梯：{JsonConvert.SerializeObject(pushPassRecord)}");
            try
            {
                await _openApi.PushRecord(_pushUrlHelper.PushUrl, pushPassRecord);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("上传数据错误：{0} ", ex);
                return false;
            }
        }
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            //long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            long t = (time.Ticks - 621356256000000000) / 10000;
            return t;
        }
        /// <summary>
        /// 上传本地错误记录
        /// </summary>
        /// <returns></returns>
        public async Task PushErrorAsync()
        {
            long ts = ConvertDateTimeToInt(DateTime.Now)-600000;//获取当前时间戳  山粗600秒前记录
            IDalMySql dal;
            string Dbconn = AppConfigurtaionServices.Configuration["ConnectionStrings:MysqlConnection"].ToString().Trim();
            dal = DalFactory.CreateMysqlDal("mysql", Dbconn);
            string SQL = $@"delete from pass_record where PassTime<={ts}";
            long r = dal.ExecuteMySql(SQL);

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IPassRecordService>();
                var records = await service.GetAllAsync();             
                if (records != null && records.FirstOrDefault() != null)
                {

                    foreach (var item in records)
                    {
                        var result = await PushAsync(item);
                        if (result || item.ErrorTimes > _appSettings.ErrorDestroyRetimes)
                        {
                            await service.DeleteAsync(item.Id);
                        }
                        else
                        {
                            item.ErrorTimes++;
                            await service.AddOrEditAsync(item);
                        }
                    }
                }
            }
        }
    }
}
