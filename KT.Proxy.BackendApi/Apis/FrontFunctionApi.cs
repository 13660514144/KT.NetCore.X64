using KT.Common.Core.Helpers;
using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpModel;
using KT.Proxy.BackendApi.Helpers;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public class FrontFunctionApi : BackendApiBase, IFunctionApi
    {
        private ILogger<FrontFunctionApi> _logger;
        private IMemoryCache _memoryCache;

        public FrontFunctionApi(ILogger<FrontFunctionApi> logger,
            IMemoryCache memoryCache) : base(logger)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<FileInfoModel> GetQrAsync(string content)
        {
            content = StringUtil.UrlEncode(content);
            var result = await GetFileAsync($"/front/qr/create?content={content}");
            return result;
        }

        public async Task SendNotifyAsync(NotifyModel model)
        {
            await PostAsync("/front/base/send/emergencyNotice", model);
        }

        /// <summary>
        /// 访客配置
        /// </summary>
        /// <returns></returns>
        public async Task<VisitorConfigParms> GetConfigParmsAsync()
        {
            var result = await _memoryCache.GetOrCreateAsync(CacheKeys.VisitorConfigParms, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120);
                var parms = await PostAsync<VisitorConfigParms>("/front/setting/info");
                return parms;
            });

            await DebugHelper.DebugRunAsync(() =>
            {
                //result.OpenVisitorCheck = true;

                return Task.CompletedTask;
            });

            return result;
        }

        public async Task HeartbeatAsync()
        {
            await PostAsync($"/front/heartbeat");
        }

        /// <summary>
        /// 访客配置
        /// 访客配置启动时直接调用，出错不抛出异常
        /// </summary>
        /// <returns></returns>
        public async Task<VisitorSettingModel> GetSystemSetting()
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
            {
                return null;
            }
            try
            {
                var result = await _memoryCache.GetOrCreateAsync(CacheKeys.VisitorSetting, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                    var setting = await PostAsync<VisitorSettingModel>("/front/setting/system", isHasBaseHeader: false);
                    return setting;
                });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("获取配置信息错误：ex:{0} ", ex);
                return null;
            }
        }

        public async Task<FileInfoModel> GetPictureAsync(string url)
        {
            var result = await GetFileAsync(url);
            return result;
        }

        public async Task<long> GetSystemTime()
        {
            return await PostAsync<long>("/system/time");
        }
    }
}
