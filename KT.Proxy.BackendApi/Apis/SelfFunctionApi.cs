using KT.Common.Core.Helpers;
using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpModel;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public class SelfFunctionApi : BackendApiBase, IFunctionApi
    {
        private ILogger<SelfFunctionApi> _logger;

        public SelfFunctionApi(ILogger<SelfFunctionApi> logger) : base(logger)
        {
            _logger = logger;
        }

        public async Task<FileInfoModel> GetQrAsync(string content)
        {
            content = StringUtil.UrlEncode(content);
            var result = await GetFileAsync($"/self/qr/create?content={content}");
            return result;
        }

        /// <summary>
        /// 访客配置
        /// </summary>
        /// <returns></returns>
        public async Task<VisitorConfigParms> GetConfigParmsAsync()
        {
            var result = await PostAsync<VisitorConfigParms>("/self/setting/info");

            await DebugHelper.DebugRunAsync(() =>
            {
                //result.OpenVisitorCheck = true;

                return Task.CompletedTask;
            });

            return result;
        }

        public async Task HeartbeatAsync()
        {
            await PostAsync($"/self/heartbeat");
        }

        public Task SendNotifyAsync(NotifyModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<VisitorSettingModel> GetSystemSetting()
        {
            if (string.IsNullOrEmpty(this.BaseUrl))
            {
                return null;
            }
            try
            {
                var result = await PostAsync<VisitorSettingModel>("/self/setting/system");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("获取配置信息错误：ex:{0} ", ex);
            }
            return null;
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
