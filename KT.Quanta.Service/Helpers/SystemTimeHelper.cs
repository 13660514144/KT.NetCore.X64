using KT.Proxy.BackendApi.Apis;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Helpers
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
        private readonly PushUrlHelper _pushUrlHelper;

        public SystemTimeHelper(ILogger<SystemTimeHelper> logger,
            OpenApi openApi,
            PushUrlHelper pushUrlHelper)
        {
            _logger = logger;
            _openApi = openApi;
            _pushUrlHelper = pushUrlHelper;
        }

        private async void SystemTimeSyncCallbackAsync(object state)
        {
            var result = await KT.Common.Core.Helpers.SystemTimeHelper.SetSystemTimeAsync(async () =>
            {
                return await _openApi.GetSystemTime(_pushUrlHelper.PushUrl);
            });
            if (!result)
            {
                //_logger.LogWarning($"同步系统时间失败！");
            }
        }

        public Task StartAsync()
        {
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
