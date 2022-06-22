using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Manage.Service.Services;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Handlers
{
    public class PushRecordHandler
    {
        private IServiceProvider _serviceProvider;
        public PushApi _pushApi;
        public PushUrlHelper _pushUrlHelper;
        private ILogger<PushRecordHandler> _logger;
        private AppSettings _appSettings;

        public PushRecordHandler(IServiceProvider serviceProvider,
           PushApi pushApi,
           PushUrlHelper pushUrlHelper,
           ILogger<PushRecordHandler> logger,
           IOptions<AppSettings> appSettings)
        {
            _serviceProvider = serviceProvider;
            _pushApi = pushApi;
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
            pushPassRecord.AccessDate = passRecord.PassTime.ToZoneDateTime()?.ToSecondString();
            }

            try
            {
                await _pushApi.Push(_pushUrlHelper.PushUrl, pushPassRecord);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("上传数据错误：{0} ", ex);
                return false;
            }
        }

        /// <summary>
        /// 上传本地错误记录
        /// </summary>
        /// <returns></returns>
        public async Task PushErrorAsync()
        {
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
