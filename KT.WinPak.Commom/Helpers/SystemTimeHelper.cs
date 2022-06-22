using KT.Proxy.BackendApi.Apis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KT.WinPak.Commom.Helpers
{
    /// <summary>
    /// 系统时间同步
    /// </summary>
    public class SystemTimeHelper : IAsyncDisposable
    {
        //系统时间同步
        private Timer _systemTimeSyncTimer;

        private readonly ILogger<SystemTimeHelper> _logger;
        private readonly OpenApi _openApi;
        private Func<string> _urlAction;

        public SystemTimeHelper(ILogger<SystemTimeHelper> logger, OpenApi openApi)
        {
            _logger = logger;
            _openApi = openApi;
        }

        private async void SystemTimeSyncCallbackAsync(object state)
        {
            var result = await KT.Common.Core.Helpers.SystemTimeHelper.SetSystemTimeAsync(async () =>
            {
                if (_urlAction == null)
                {
                    _logger.LogError($"同步系统时间获取地址委托为空！");
                    return null;
                }
                var url = _urlAction.Invoke();
                if (!string.IsNullOrEmpty(url))
                {
                    _logger.LogError($"同步系统时间获取地址为空！");
                    return null;
                }
                return await _openApi.GetSystemTime(url);
            });
            if (!result)
            {
                _logger.LogWarning($"同步系统时间失败！");
            }
        }

        public Task StartAsync(Func<string> urlAction)
        {
            _urlAction = urlAction;
            //时间同步
            _systemTimeSyncTimer = new Timer(SystemTimeSyncCallbackAsync, null, 10 * 1000, 60 * 1000);

            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_systemTimeSyncTimer != null)
            {
                await _systemTimeSyncTimer.DisposeAsync();
            }
        }
    }
}
